using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


public class Fighter : MonoBehaviour
{
    public int maxHealth = 75;
    public int currentHealth;
    public int currentBlock = 0;
    public int poisionNumber = 0;
    public HealthBarUI healthBarUI;
    public int[] a ;
    public bool isPlayer;

    [Header("Manager")]
    public BattleMangager battleMangager;
    public GameManager gameManager;

    [Header("Buff")]
    public List<Buff> buffs = new List<Buff>();
    public Buff vulnerable;
    public Buff burn;
    public Buff heal;
    public Buff weak;
    public Buff poision;
    public Buff erage;
    public Buff strenghth;
    public Buff dexterity;
    public GameObject buffPrefab;
    public Transform buffParent;




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

    public void UpdateAtTurnEnd()
    {
        

    }
    public void UpdateAtTurnStart()
    {
        for (int i = buffs.Count - 1; i >= 0; i--)
        {
            if (!buffs[i].isPermernent)
            {
                Buff item = buffs[i];
                item.AddBuffValue(-1);
                if (item.buffValue == 0)
                {
                    buffs.RemoveAt(i);
                }
            }
 
        }
    }

    public void AddBuff(Buff.Type _typebuff, int amount)
    {
        int index = buffs.FindIndex(p => p.type == _typebuff);
        if (index == -1)
        {
            buffs[index].AddBuffValue(amount);
        }
        else
        {
            switch (_typebuff)
            {
                case Buff.Type.vulnerable:
                    {
                        buffs.Add(vulnerable);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    }
                case Buff.Type.weak:
                    {
                        buffs.Add(weak);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                case Buff.Type.heal:
                    {
                        buffs.Add(heal);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                case Buff.Type.strength:
                    {
                        buffs.Add(strenghth);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                case Buff.Type.dexterity:
                    {
                        buffs.Add(dexterity);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                case Buff.Type.poision:
                    {
                        buffs.Add(poision);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                case Buff.Type.burn:
                    {
                        buffs.Add(burn);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                case Buff.Type.enrage:
                    {
                        buffs.Add(erage);
                        buffs.Last().AddBuffValue(amount);
                        return;
                    };
                default: break;
            }
        }
        foreach (Buff buff in buffs)
        {
            buff.buffUI.DisplayBuff(buff);
        }


        
    }
    public int BuffIndex(Buff.Type _typebuff)
    {
        return buffs.FindIndex(item => item.type == _typebuff);
        
    }

    
}
