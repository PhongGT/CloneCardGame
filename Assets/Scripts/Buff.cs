using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public struct Buff
{
    public Type type;
    public enum Type {heal, vulnerable, weak, poision, enrage, strength, dexterity, frail, stun };

    public Sprite buffIcon;
    [Range(0, 999)]
    public int buffValue;
    public BuffUI buffUI;
    public bool isPermernent;
    public void AddBuffValue(int amount)
    {
        this.buffValue += amount;
    }
}



