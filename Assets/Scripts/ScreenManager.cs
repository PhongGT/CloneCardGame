using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public GameObject titleScene;
    public GameObject battleScene;

    public GameObject gameOverScene;

    public GameObject restScene;
    
    GameManager gameManager;
    BattleManager battleManager;
    public RewardScreen rewardScreen;
    private void Awake() {
        gameManager = GetComponent<GameManager>();
        battleManager = FindObjectOfType<BattleManager>();
        
    }
    // Update is called once per frame
    public void StartButton()
    {
        titleScene.SetActive(false);
        gameManager.LoadPlayerStats();
        StartCoroutine(LoadBattle());
    }
    public void RestartButton()
    {
        gameOverScene.SetActive(false);
        gameManager.floorNumber = 1;
        battleManager.player.currentHealth = battleManager.player.maxHealth;
        foreach (GameObject item in battleManager.enemyObjs)
        {
            Destroy(item);
        }
        gameManager.LoadPlayerStats();
        StartCoroutine(LoadBattle());
    }
    public void SkipButton()
    {
        rewardScreen.gameObject.SetActive(false);
        StartCoroutine(LoadBattle());
    }
    public IEnumerator LoadBattle()
    {
        
        if(gameManager.floorNumber % 5 == 0)
        {
            battleManager.StartBossFight();
        }
        else
        {
            battleManager.StartMobFight();
        }
        yield return new WaitForSeconds(1f);
    }
}
