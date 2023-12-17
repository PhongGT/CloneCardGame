using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTarget : MonoBehaviour
{
    BattleManager BattleManager;
    Fighter enemyFighter;
    private void Awake()
    {
        BattleManager = FindObjectOfType<BattleManager>();
        enemyFighter = GetComponent<Fighter>();
    }

    public void SelectTarget()
    {
        Debug.Log("Click target");
        if(enemyFighter == null)
        {
            BattleManager = FindObjectOfType<BattleManager>();
            enemyFighter = GetComponent<Fighter>();
        }
            
        if(BattleManager.selectedCard != null ) {

            
        }BattleManager.cardTarget = enemyFighter;
    }
    public void DeSelectedTarget()
    {
        BattleManager.cardTarget = null;
        //Deselected Target

    }
}
