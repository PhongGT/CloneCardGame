using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Card : ScriptableObject
{
    public string cardTitile;
    public bool isUpgrade;
    public Sprite cardIcon;

    public CardType cardType;
    public enum CardType {Attack, Skill, Power}
    // Maybe add more Class is needed;
/*  public enum CardClass { Berserker, Druid}
    public CardClass cardClass;*/

    public enum CardTargetType { self, enemy}
    public CardData cardCost; //Cost the bai
    public CardData cardEffect; // gia tri def, atk
    public CardData buffAmount; // gia tri buff/debuff
    public CardDescription cardDescription;

    public int GetCardValue()
    {
        if(!isUpgrade)
        {
            return cardCost.baseData;
        }
        return cardCost.upgradeData;
    }

    public int GetCardEffect()
    {
        if(!isUpgrade)
        {
            return cardEffect.baseData;
        }
        return cardEffect.upgradeData;
    }
    public int GetBuffAmount()
    {
        if (!isUpgrade)
        {
            return buffAmount.baseData;
        }
        return buffAmount.upgradeData;
    }

    public string GetCardDecription()
    {
        if(!isUpgrade)
        {
            return cardDescription.baseData;
        }
        return cardDescription.upgradeData;
    }


    [System.Serializable]
    public struct CardData
    {
        public int baseData;
        public int upgradeData;
    }

    [System.Serializable]
    public struct CardDescription
    {
        public string baseData;
        public string upgradeData;
    }

   





}
