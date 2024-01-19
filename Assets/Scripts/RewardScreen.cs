using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    public GameObject relicReward;
    public GameObject goldReward;
    public GameObject cardReward;
    public GameObject cardSelect;
    public GameObject nextFloor;
    public TMP_Text goldText;
    public List<LoadReward> cardRewards;
    public bool isLoaded;
    public Fighter player;
    BattleManager battleManager;
    GameManager gameManager;
    // Update is called once per frame
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();
    }
    public void HandleGoldReward()
    {
        gameManager.UpdateGold(battleManager.enemies[0].goldDrop);
        goldReward.SetActive(false);

    }
    public void HandleCardReward()
    {
        nextFloor.SetActive(false);
        if (isLoaded)
        {
            return;
        }
        else
        {
            gameManager.cardLibrary.Shuffle();
            cardSelect.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                cardRewards[i].gameObject.SetActive(true);
                cardRewards[i].LoadCard(gameManager.cardLibrary[i]);
            }
            isLoaded = true;
        }


    }
    public void HandleRandomRelic()
    {
        //RandomRelic
        Relic relic = gameManager.relicsLibrary[Random.Range(0, gameManager.relicsLibrary.Count)];
        gameManager.relics.Add(relic);
        gameManager.HasRelic("Berry");
        player.maxHealth +=10;
        player.Heal(10);
        player.UpdateMaxHealth();
        gameManager.playerStatsBar.DisplayRelics();
        relicReward.SetActive(false);
    }
    public void SelectedCard(int cardPos)
    {
        gameManager.playerDeck.Add(gameManager.cardLibrary[cardPos]);
        gameManager.displayCard.Display(gameManager.playerDeck);
        foreach (LoadReward card in cardRewards)
        {
            card.gameObject.SetActive(false);
        }
        cardReward.SetActive(false);
        cardSelect.SetActive(false);
        nextFloor.SetActive(true);
        isLoaded = false;

    }
    public void HandleSkipCard()
    {
        cardSelect.SetActive(false);
        nextFloor.SetActive(true);
    }
    public void OnClickCardReward()
    {
        HandleCardReward();
        cardSelect.SetActive(true);
    }
    public void SkipReward()
    {
        this.gameObject.SetActive(false);
        //Switch to choose path 
    }
}
