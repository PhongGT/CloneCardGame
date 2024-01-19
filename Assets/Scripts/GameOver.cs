using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
	public TMP_Text amount;
    GameManager gameManager;
    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnEnable()
    {
        amount.text="Floors Climbed: "+(gameManager.floorNumber-1).ToString();
    }
}

