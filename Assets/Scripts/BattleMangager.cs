using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("CardUI")]
    public List<Card> deck;
    public List<Card> drawPile = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<CardUIManager> cardsInHandOBJ = new List<CardUIManager>();
    public CardUIManager selectedCard;
    public float cardSpacing = 125f;
    public float maxSpreadAngle = 30f;
    private float spreadAngle;
    public GameObject Hand;
    [Header("UI")]
    public Button endTurn;
    public TMP_Text drawPileCount;
    public TMP_Text discardPileCount;
    public TMP_Text energyText;
    public Transform enemyParent;
    public PTrigger shuffleParticle;

    public PTrigger playcardParticle;
    public Animator holyLight;


    [Header("BattleStats")]
    public int maxEnergy;
    public int energy;
    public int drawAmount;
    public Turn turn;
    public enum Turn { Player, Enemy }

    [Header("Enemy")]

    public List<Enemy> enemies = new List<Enemy>();
    public List<Fighter> enemyFighters = new List<Fighter>();
    public List<GameObject> enemyObjs = new List<GameObject>();

    public GameObject[] posibleEnemies;
    public GameObject[] possibleElites;
    public GameObject[] possibleBosses;
    public bool isElite;
    public Fighter cardTarget;
    public Fighter player;
    public GameManager gameManager;
    public RewardScreen rewardScreen;
    public GameObject gameOver;
    CardAction cardAction;


    public void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        cardAction = GetComponent<CardAction>();
        Hand = GameObject.FindGameObjectWithTag("HandUI");
    }
    public void StartBossFight()
    {
        isElite = true;
        BeginBattle(possibleBosses);
    }
    public void StartMobFight()
    {
        isElite = false;
        BeginBattle(posibleEnemies);
    }

    public void BeginBattle(GameObject[] prefabs)
    {
        //Turn nguoi choi dau tien
        GameObject newEnemy = Instantiate(prefabs[Random.Range(0,prefabs.Length)], enemyParent);
        enemyObjs.Add(newEnemy);
        newEnemy.SetActive(true);
        if(rewardScreen!=null)
        {
            rewardScreen.gameObject.SetActive(false);
        }
        enemies.Clear();
        Enemy e = FindObjectOfType<Enemy>();
        enemies.Add(e);
        enemyFighters.Add(e.GetComponent<Fighter>());
        

        player.UpdateHealth();
        turn = Turn.Player;
        deck = new List<Card>(gameManager.playerDeck);
        drawPile = deck;
        drawPile.Shuffle();
        discardPile.Clear();
        cardsInHand.Clear();
        StartCoroutine(DrawCard(5));
        UpdateDiscardPipleCount();
        energy = maxEnergy;
        UpdateEnergyText();

        StartRelic();

    }
    public void StartRelic()
    {
        if (gameManager.HasRelic("Lantern"))
        {
            energy += 1;
        }
        if (gameManager.HasRelic("WhetStone"))
        {
            player.AddBuff(Buff.Type.strength, 1);
        }
    }
    public IEnumerator DrawCard(int amountDraw)
    {
        int cardsDraw = 0;
        while (cardsDraw < amountDraw)
        {
            if (drawPile.Count < 1)
            {
                StartCoroutine(ShuffleCard());
                yield return new WaitForSeconds(2f);
            }
            AddCardToHand(ref cardsInHand, drawPile[0]);
            drawPile.Remove(drawPile[0]);
            cardsDraw++;
            UpdateDrawPipleCount();
        }

    }
    public IEnumerator ShuffleCard()
    {
        shuffleParticle.TriggerOn();
        discardPile.Shuffle();
        drawPile = discardPile;
        discardPile = new List<Card>();
        UpdateDiscardPipleCount();
        yield return new WaitForSeconds(1.5f);
        
        
    }
    public void DisplayCard(Card card)
    {
        if(cardsInHand.Count > 9)
        {
            DiscardCard(card);
            return;
        }
        CardUIManager cardUIManager = cardsInHandOBJ[cardsInHand.Count - 1];
        cardUIManager.LoadCard(card);
        cardUIManager.gameObject.SetActive(true);
    }
    public void DiscardCard(Card card)
    {
        if (card.cardType == Card.CardType.Power)
        {
            UpdateDiscardPipleCount();
        }
        else
        {
            discardPile.Add(card);
            UpdateDiscardPipleCount();
        }

    }

    public void AddCardToHand(ref List<Card> cards, Card card)
    {
        cards.Add(card);
        DisplayCard(card);
        // spreadAngle = maxSpreadAngle;
        /*SpreadCards();*/
    }
    public IEnumerator PlayCard(CardUIManager cardUI)
    {
        Debug.Log("In play card");
        //Play
        Debug.Log(cardTarget);
        Debug.Log(cardUI._card.cardTitile);
        cardAction.PerformAction(cardUI._card, cardTarget);

        //Energy
        energy -= cardUI._card.GetCardValue();
        UpdateEnergyText();

        //Discard
        selectedCard = null;
        cardUI.gameObject.SetActive(false);
        cardsInHand.Remove(cardUI._card);
        DiscardCard(cardUI._card);
        playcardParticle.TriggerOn();
        yield return new WaitForSeconds(0.5f);
        UpdateDiscardPipleCount();

    }
    public void ChangeTurn()
    {
        if (turn == Turn.Player)
        {
            turn = Turn.Enemy;
            endTurn.enabled = false;
            foreach (Card item in cardsInHand)
            {
                DiscardCard(item);
            }
            foreach (CardUIManager cardUIManager in cardsInHandOBJ)
            {
                if (cardUIManager.gameObject.activeSelf)
                {
                    // Discard effect
                }
                cardUIManager.gameObject.SetActive(false);
                cardsInHand.Clear();
            }
            foreach (Enemy enemy in enemies)
            {
                //Reset block value 
                enemy.thisEnemy.currentBlock = 0;
                enemy.thisEnemy.healthBarUI.DisplayBlock(0);
            }
            
            StartCoroutine(HandleEnemyTurn());
            player.UpdateAtTurnStart();
        }
        else
        {
            // foreach (Enemy enemy in enemies)
            // {
            //     enemy.DisplayIntentAttack();
            // }
            turn = Turn.Player;
            //Reset Block maybe change in future
            player.currentBlock = 0;
            player.healthBarUI.DisplayBlock(0);
            energy = maxEnergy;
            UpdateEnergyText();

            endTurn.enabled = true;
            StartCoroutine(DrawCard(drawAmount));

        }
    }
    public IEnumerator HandleEnemyTurn()
    {
        Debug.Log("EnemyTurn");
        foreach (Enemy e in enemies)
        {
            e.midTurn = true;
            e.TakeTurn();
            while (e.midTurn)
                yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.5f);
        ChangeTurn();

    }
    public void UpdateEnergyText()
    {
        energyText.text = energy.ToString() + "/" + maxEnergy.ToString();
    }
    public void UpdateDiscardPipleCount()
    {
        discardPileCount.text = discardPile.Count.ToString();
    }
    public void UpdateDrawPipleCount()
    {
        drawPileCount.text = drawPile.Count.ToString();
    }
    public void EndFight(bool win)
    {
        if (!win)
        {
            //gameOver
            gameOver.SetActive(true);
        }
        else
        {
            if (gameManager.HasRelic("HolyLight"))
            {
                player.Heal(6);
                holyLight.SetTrigger("Buff");
            }
            player.ResetBuff();
            HandleEndScreen();
            gameManager.UpdateFloor();
        }



        //Update gameManager
    }
    public void HandleEndScreen()
    {
        rewardScreen.gameObject.SetActive(true);
        rewardScreen.goldReward.SetActive(true);
        rewardScreen.goldText.text = enemies[0].goldDrop.ToString() + "Gold";
        rewardScreen.cardReward.SetActive(true);

        if (isElite)
        {
            rewardScreen.relicReward.SetActive(true);
        }

    }


    /*    public void SpreadCards() // must fix here
        {
            float angleStep = spreadAngle / (cardsInHand.Count - 1);

            Vector3 startPos = Hand.transform.position - 
                Hand.transform.right * (cardSpacing * (cardsInHand.Count - 1)) / 2f;
            if( cardsInHand.Count %2 == 0 ) {
                for (int i = 0; i < cardsInHand.Count/2; i++)
                {
                    Vector3 cardPos = startPos + Hand.transform.right * (cardSpacing * i);
                    float z = (-spreadAngle / 2f + angleStep * i);

                }

            }
            else
            {

            }
            for (int i = 0; i < cardsInHand.Count/2; i++)      {

                Vector3 cardPos = startPos + Hand.transform.right * (cardSpacing *i);
                float z = (-spreadAngle/2f + angleStep * i);

                Debug.Log(z);
                Debug.Log("Pos: "+cardPos);
                Quaternion cardRotation = Quaternion.Euler(0f, 0f, z);

            }
        }*/
}
