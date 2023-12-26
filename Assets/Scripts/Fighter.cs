using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Fighter : MonoBehaviour
{
    public int maxHealth = 75;
    public int currentHealth;
    public int currentBlock = 0;
    public int poisionNumber = 0;
    public HealthBarUI healthBarUI;
    public int[] a;
    public bool isPlayer;

    [Header("Manager")]
    public BattleManager BattleManager;
    public GameManager gameManager;

    [Header("Buff")]
    public Buff vulnerable;
    public Buff heal;
    public Buff weak;
    public Buff poision;
    public Buff erage;
    public Buff strength;
    public Buff dexterity;
    public Buff frail;
    public Buff stun;
    public GameObject buffPrefab;
    public Transform buffParent;




    /*[Header("Buffs")]*/

    private void Awake()
    {
        BattleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();
        currentHealth = maxHealth;
        healthBarUI.healthBar.maxValue = maxHealth;
        healthBarUI.DisplayHealth(currentHealth);
        healthBarUI.DisplayPoision(0, currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (currentBlock > 0)
        {
            amount = BlockDamage(amount);
        }
        currentHealth -= amount;

        UpdateHealth(currentHealth);
        if (erage.buffValue > 0)
        {
            AddBuff(Buff.Type.strength, erage.buffValue);
        }
        if(currentHealth <= 0)
        {
            if (isPlayer == true)
            {
                BattleManager.EndFight(false);
            }
            else 
            {
                BattleManager.EndFight(true);
            }
            Destroy(gameObject);
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        UpdateHealth(currentHealth);
    }
    public void TakePoisionDamage(int amount)
    {
        currentHealth -= poisionNumber;
        UpdateHealth(currentHealth);
    }

    public int BlockDamage(int amount)
    {
        if (currentBlock >= amount)
        {
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
        int total = amount + dexterity.buffValue;
        if (isPlayer && frail.buffValue > 0)
        {
            float round = total * 0.75f;
            currentBlock += (int)round;
        }
        else
        {
            currentBlock += total;
        }



        healthBarUI.DisplayBlock(currentBlock);
    }
    private void Dead()
    {
        this.gameObject.SetActive(false);
    }

    public void AddBuff(Buff.Type _typebuff, int amount)
    {
        if (_typebuff == Buff.Type.vulnerable)
        {
            if (vulnerable.buffValue <= 0)
            {
                vulnerable.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            vulnerable.AddBuffValue(amount);
            vulnerable.buffUI.DisplayBuff(vulnerable);
        }
        else if (_typebuff == Buff.Type.heal)
        {
            if (heal.buffValue <= 0)
            {
                heal.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            heal.AddBuffValue(amount);
            heal.buffUI.DisplayBuff(heal);
        }
        else if (_typebuff == Buff.Type.poision)
        {
            if (heal.buffValue <= 0)
            {
                poision.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            poision.AddBuffValue(amount);
            poision.buffUI.DisplayBuff(poision);
            healthBarUI.DisplayPoision(poision.buffValue, currentHealth);
        }
        else if (_typebuff == Buff.Type.weak)
        {
            if (heal.buffValue <= 0)
            {
                weak.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            weak.AddBuffValue(amount);
            weak.buffUI.DisplayBuff(weak);
        }
        else if (_typebuff == Buff.Type.strength)
        {
            if (strength.buffValue <= 0)
            {
                strength.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            strength.AddBuffValue(amount);
            strength.buffUI.DisplayBuff(strength);
        }
        else if (_typebuff == Buff.Type.dexterity)
        {
            if (dexterity.buffValue <= 0)
            {
                dexterity.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            dexterity.AddBuffValue(amount);
            dexterity.buffUI.DisplayBuff(dexterity);
        }
        else if (_typebuff == Buff.Type.stun)
        {
            if (stun.buffValue <= 0)
            {
                stun.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            stun.AddBuffValue(amount);
            stun.buffUI.DisplayBuff(stun);
        }
        else if (_typebuff == Buff.Type.enrage)
        {
            if (erage.buffValue <= 0)
            {
                erage.buffUI = Instantiate(buffPrefab, buffParent).GetComponent<BuffUI>();
            }
            erage.AddBuffValue(amount);
            erage.buffUI.DisplayBuff(erage);
        }

    }

    public void UpdateAtTurnStart()
    {
        if (vulnerable.buffValue > 0)
        {
            vulnerable.buffValue -= 1;
            vulnerable.buffUI.DisplayBuff(vulnerable);
            if (vulnerable.buffValue <= 0)
            {
                Destroy(vulnerable.buffUI.gameObject);
            }

        }
        if (weak.buffValue > 0)
        {
            weak.buffValue -= 1;
            weak.buffUI.DisplayBuff(weak);
            if (weak.buffValue <= 0)
            {
                Destroy(weak.buffUI.gameObject);
            }

        }
        if (frail.buffValue > 0)
        {
            frail.buffValue -= 1;
            frail.buffUI.DisplayBuff(vulnerable);
            if (frail.buffValue <= 0)
            {
                Destroy(frail.buffUI.gameObject);
            }

        }
        if (heal.buffValue > 0)
        {
            Heal(heal.buffValue);
            heal.buffValue -= 1;
            heal.buffUI.DisplayBuff(vulnerable);
            if (heal.buffValue <= 0)
            {
                Destroy(heal.buffUI.gameObject);
            }

        }
    }
    public void ResetBuff()
    {
        if (vulnerable.buffValue > 0)
        {
            vulnerable.buffValue = 0;
            Destroy(vulnerable.buffUI.gameObject);
        }
        else if (weak.buffValue > 0)
        {
            weak.buffValue = 0;
            Destroy(weak.buffUI.gameObject);
        }
        else if (strength.buffValue > 0)
        {
            strength.buffValue = 0;
            Destroy(strength.buffUI.gameObject);
        }
        else if (heal.buffValue > 0)
        {
            heal.buffValue=0;
            Destroy(heal.buffUI.gameObject);
        }
        else if (frail.buffValue > 0)
        {
            frail.buffValue=0;
            Destroy(frail.buffUI.gameObject);
        }
        else if(dexterity.buffValue > 0)
        {
            dexterity.buffValue = 0;
            Destroy(dexterity.buffUI.gameObject);
        }
        else if(poision.buffValue > 0)
        {
            poision.buffValue = 0;
            healthBarUI.DisplayPoision(0, currentHealth);
            Destroy(dexterity.buffUI.gameObject);
        }

        

        currentBlock = 0;
        healthBarUI.DisplayBlock(0);
    }



}
