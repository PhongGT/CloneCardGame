using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    public Fighter player;
    public Fighter target;
    Card card;
    Buff buff;
    BattleManager battleManager;

    public Animator animator;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

    public void PerformAction(Card _card, Fighter _target)
    {
        card = _card;
        target = _target;

        switch (card.cardTitile)
        {
            case "Strike":
                Attack();
                break;
            case "Block":
                Defence(); 
                break;
            case "Poision Stab":
                Attack();
                ApplyBuff(Buff.Type.poision);
                break;
            case "Armor Up":
                Defence();
                StartCoroutine(battleManager.DrawCard(1));
                break;
            case "Bash":
                Attack();
                ApplyBuff(Buff.Type.vulnerable);
                break;
            case "Quick Feet":
                AddBuff(Buff.Type.dexterity);
                break;
            case "Impervious":
                Defence();
                break;
            case "Inflame":
                AddBuff(Buff.Type.strength);
                break;
            case "Tricky Move":
                Attack();
                ApplyBuff(Buff.Type.weak);
                break;

            default:
                Debug.Log("Smell Fishy Here!!");
                break;
        }


    }
    public void Attack()
    {
        int totalDamage = card.GetCardEffect()+player.strength.buffValue;
        if(target.vulnerable.buffValue>0)
        {
            float round = totalDamage*1.5f;
            totalDamage = (int)round;
        }
        animator.SetTrigger("Attack");
        target.TakeDamage(totalDamage);
    }

    public void Defence()
    {
        int block = card.GetCardEffect();

        player.AddBlock(block);
    }

    public void AddBuff(Buff.Type _typebuff)
    {
        player.AddBuff(_typebuff, card.GetBuffAmount());


    }
    public void ApplyBuff(Buff.Type _typebuff)
    {
        target.AddBuff(_typebuff, card.GetBuffAmount());
    }

}
