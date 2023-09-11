using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMangager : MonoBehaviour
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

    [Header("BattleStats")]
    public int maxEnergy;
    public int energy;
    public int drawAmount;
    public Turn turn;
    public enum Turn {Player, Enemy }
    


    public GameManager gameManager;


    public void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        Hand = GameObject.FindGameObjectWithTag("HandUI");
    }
    public void Start()
    {
        BeginBattle();
        DrawCard(5);
    }

    public void BeginBattle()
    {
        //Turn nguoi choi dau tien
        turn = Turn.Player;
        deck = gameManager.playerDeck;
        drawPile = deck;
        
    }
    public void DrawCard(int amountDraw)
    {
        int cardsDraw = 0;
        while (cardsDraw < amountDraw)
        {
            if(drawPile.Count < 1)
            {
                ShuffleCard();
            }
            AddCardToHand(ref cardsInHand, drawPile[0]);
            drawPile.Remove(drawPile[0]);
            cardsDraw++;
        }

    }
    public void ShuffleCard()
    {
        discardPile.Shuffle();
        drawPile = discardPile;
        discardPile = new List<Card>();
    }
    public void DisplayCard(Card card)
    {
        CardUIManager cardUIManager = cardsInHandOBJ[cardsInHand.Count - 1];
        cardUIManager.LoadCard(card);
        cardUIManager.gameObject.SetActive(true);
    }
    public void DiscardCard(Card card)
    {
        discardPile.Add(card);
    }

    public void AddCardToHand(ref List<Card> cards, Card card)
    {
        cards.Add(card);
        DisplayCard(card);
        spreadAngle = maxSpreadAngle;
        /*SpreadCards();*/
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
