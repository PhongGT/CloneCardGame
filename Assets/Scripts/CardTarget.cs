using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTarget : MonoBehaviour
{
    BattleMangager battleMangager;
    Fighter enemyFighter;
    private void Awake()
    {
        battleMangager = FindObjectOfType<BattleMangager>();
        enemyFighter = GetComponent<Fighter>();
    }

    public void SelectTarget()
    {
        Debug.Log("Click target");
        if(enemyFighter == null)
        {
            battleMangager = FindObjectOfType<BattleMangager>();
            enemyFighter = GetComponent<Fighter>();
        }
            
        if(battleMangager.selectedCard != null ) {

            
        }battleMangager.cardTarget = enemyFighter;
    }
    public void DeSelectedTarget()
    {
        battleMangager.cardTarget = null;
        //Deselected Target

    }
}
