using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Card> playerDeck = new List<Card>();
    public List<Card> playerStarterDeck = new List<Card>();
    public List<Card> cardLibrary = new List<Card>();
    public List<Relic> relics = new List<Relic>();
    public List<Relic> relicsLibrary = new List<Relic>();
    public Relic startRelic;
    public int floorNumber = 0;
    public int goldAmount;
    public DisplayCard displayCard;

    public PlayerStatsBar playerStatsBar;

    private void Awake()
    {
        playerStatsBar = FindObjectOfType<PlayerStatsBar>();
        
    }
    public void LoadPlayerStats()
    {
        playerDeck.Clear();
        playerDeck = new List<Card>(playerStarterDeck);
        relics.Clear();
        relics.Add(startRelic); 
        playerStatsBar.playerStatsBarObject.SetActive(true);
        UpdateGold(0);
        UpdateFloor();
        playerStatsBar.DisplayRelics();
        displayCard.Display(playerDeck);
    }
    public void UpdateFloor()
    {
        		floorNumber+=1;

		switch (floorNumber)
        {
            case 1:
                playerStatsBar.floorText.text = floorNumber+"st Floor";
                break;
            case 2:
                playerStatsBar.floorText.text = floorNumber+"nd Floor";
                break;
            case 3:
                playerStatsBar.floorText.text = floorNumber+"rd Floor";
                break;
            default:
                playerStatsBar.floorText.text = floorNumber+"th Floor";
                break;
        }
    }

    public bool HasRelic(string relicName)
    {
        bool nameExists = relics.Exists( r => r.relicName == relicName);
        return nameExists;
    }
    public void UpdateGold(int amount)
    {
        goldAmount += amount;
        playerStatsBar.moneyText.text = goldAmount.ToString();
    }

    public void DisplayHealth(int healthAmount, int maxHealth)
    {
        playerStatsBar.healthDisplayText.text = $"{healthAmount} / {maxHealth}";
    }

}
