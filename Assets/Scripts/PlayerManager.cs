using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int maxHealth = 75;
    public int currentHealth;
    public int currentBlock = 0;
    public int poisionNumber = 0;
    public HealthBarUI healthBarUI;
    public int[] a ;

    [Header("Manager")]
    public BattleMangager battleMangager;
    public GameManager gameManager;
    

    /*[Header("Buffs")]*/




    

    private void Awake()
    {
        battleMangager = FindObjectOfType<BattleMangager>();
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        healthBarUI.healthBar.maxValue = maxHealth;
        healthBarUI.DisplayHealth(currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (currentBlock > 0) {
            amount = BlockDamage(amount);
        }
        currentHealth -= amount;
        
        UpdateHealth(currentHealth);
    }
    public void TakePoisionDamage(int amount)
    {
        currentHealth -= poisionNumber;
        UpdateHealth(currentHealth);
    }

    public int BlockDamage( int amount)
    {
        if(currentBlock >= amount) {
            currentBlock -= amount;
            amount = 0;
        }
        else
        {
            amount -= currentBlock;
            currentBlock = 0;
        }
        healthBarUI.DisplayBlock(currentBlock);
        return amount;
    }
    public void UpdateHealth(int newAmount)
    {
        currentHealth = newAmount;
        healthBarUI.DisplayHealth(newAmount);
        //check nhan vat 
    }
    public void AddBlock(int amount)
    {
        currentBlock += amount;
        healthBarUI.DisplayBlock(currentBlock);
    }
    private void Dead()
    {
        this.gameObject.SetActive(false);
    }

    public void UpdateEndTurn()
    {


    }


}
