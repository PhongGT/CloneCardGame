using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStatsBar : MonoBehaviour
{
    public TMP_Text healthDisplayText;
    public TMP_Text moneyText;
    public TMP_Text floorText;
	public Transform relicParent;
	public GameObject relicPrefab;
	public GameObject playerStatsBarObject;
    
    GameManager gameManager;
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void DisplayRelics()
    {
        foreach(Transform r in relicParent)
        Destroy(r.gameObject);
        foreach(Relic r in gameManager.relics)
        {
            //This code not optimal need a little bit change
            GameObject a = Instantiate(relicPrefab, relicParent);
            a.GetComponent<RelicUI>().LoadRelic(r);
        }
    }
}
