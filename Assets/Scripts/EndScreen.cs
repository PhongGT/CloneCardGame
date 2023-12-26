using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public GameObject relicReward;
    public GameObject goldReward;
    public GameObject cardRewardButton;
    public List<LoadReward> cardRewards;
    GameManager gameManager;
    // Update is called once per frame
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void HandleCardReward()
    {
        gameManager.cardLibrary.Shuffle();
        for(int i = 0; i < 3 ; i++)
        {
            cardRewards[i].gameObject.SetActive(true);
            cardRewards[i].LoadCard(gameManager.cardLibrary[i]);
        }
    }
    public void AddRandomRelic()
    {
        //RandomRelic
    }
    public void SelectedCard(int cardPos)
    {
        gameManager.playerDeck.Add(gameManager.cardLibrary[cardPos]);
        foreach (LoadReward card in cardRewards)
        {
            card.gameObject.SetActive(false);
        }
    }
}
