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

    }
}
