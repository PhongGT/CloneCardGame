using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayCard : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent;
    public List<GameObject> listCards;

    GameManager gameManager;

    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Display(List<Card> cards)
    {
        if(listCards!= null)
        {
            Destroy();
        }
        foreach (Card c in cards)
        {
            var currentCard = Instantiate(cardPrefab, cardParent);
            currentCard.GetComponent<LoadReward>().LoadCard(c);
            listCards.Add(currentCard);
        }
        gameObject.SetActive(false);
    }
    public void Destroy()
    {
        if (listCards != null)
        {
            foreach (var item in listCards)
            {
                Destroy(item);
            }
        }
    }

    public void Show()
    {
        if(gameObject.activeSelf)
            this.gameObject.SetActive(false);
        else 
        {   
            this.gameObject.SetActive(true);
        }    
    }
    public void Return()
    {
        this.gameObject.SetActive(false);
    }

    public void DisplayDrawPiple()
    {
        Destroy();
        Display(battleManager.drawPile);
    }
    public void DisplayDiscardPiple()
    {
        Destroy();
        Display(battleManager.discardPile);
    }
}
