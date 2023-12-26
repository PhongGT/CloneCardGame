using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyAction 

{
	public IntentType intentType;
    //StrategicBuff/Debuff only do 1 per game
    //
    public enum IntentType{Attack,Block,StrategicBuff,StrategicDebuff,AttackDebuff,AddCurseCard, SpawnMob}
    public int amount;
    public int number_of_hit;
    public int debuffAmount;
    public List<Buff.Type> buffTypes;
    public int chance;
    public Sprite icon;
}
