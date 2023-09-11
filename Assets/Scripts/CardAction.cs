using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAction : MonoBehaviour
{
    public PlayerManager player;
    public PlayerManager target;
    Card card;
    BattleMangager battleMangager;

    private void Awake()
    {
        battleMangager = FindObjectOfType<BattleMangager>();
    }

    public void ActionCard(Card _card, PlayerManager _target)
    {
        card = _card;
        target = _target;


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

    public void AddBuff()
    {

    }

    public void AddDebuff()
    {

    }
}
