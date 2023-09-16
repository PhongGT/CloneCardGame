using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    public PlayerManager player;
    public PlayerManager target;
    Card card;
    Buff buff;
    BattleMangager battleMangager;

    private void Awake()
    {
        battleMangager = FindObjectOfType<BattleMangager>();
    }

    public void ActionCard(Card _card, PlayerManager _target)
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

            default:
                Debug.Log("Smell Fishy Here!!");
                break;
        }


    }
    public void Attack()
    {
        int totalDamage = card.GetCardEffect();

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
