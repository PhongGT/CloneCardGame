using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<EnemyAction> enemyActions;
    public List<EnemyAction> turns = new List<EnemyAction>();
    public int turnNumber;
    public bool shuffleActions;
    public Fighter thisEnemy;

    [Header("Intent_Action")]
    public Image image;
    public TMP_Text intentAmount;

    [Header("Specifics")]
    public int goldDrop;
    public bool isBoss;
    public BattleManager BattleManager;

    Fighter player;

    //Animator

    private void Start()
    {
        BattleManager = FindObjectOfType<BattleManager>();
        player = BattleManager.player;
        thisEnemy = GetComponent<Fighter>();
        //animator = GetComponent<Animator>();

        if (shuffleActions)
            GenerateTurns();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadEnemy()
    {
        BattleManager = FindObjectOfType<BattleManager>();
        player = BattleManager.player;
        thisEnemy = GetComponent<Fighter>();
    }
    public void GenerateTurns()
    {
        foreach (EnemyAction ea in enemyActions)
        {
            for (int i = 0; i < ea.chance; i++)
            {
                turns.Add(ea);
            }
        }
    }
    public void TakeTurn()
    {
        //Animator
        switch (turns[turnNumber].intentType)
        {
            case EnemyAction.IntentType.Attack:
                StartCoroutine(Attack());
                break;
            case EnemyAction.IntentType.Block:
                Block();
                break;
            case EnemyAction.IntentType.StrategicBuff:
                ApplyBuffToSelf();
                break;
            case EnemyAction.IntentType.StrategicDebuff:
                ApplyBuffToPlayer();
                break;
            case EnemyAction.IntentType.AttackDebuff:
                StartCoroutine(Attack());
                ApplyBuffToPlayer();
                break;
            case EnemyAction.IntentType.AddCurseCard:
                //New Feature
                break;

            case EnemyAction.IntentType.SpawnMob:
                //New Feature
                break;
            default:
                Debug.Log("Bug in Enemy!!!");
                break;

        }
    }
    public void ApplyBuffToSelf()
    {
        foreach (Buff.Type type in turns[turnNumber].buffTypes)
        {
            thisEnemy.AddBuff(type, turns[turnNumber].amount);
        }
    }
    public void ApplyBuffToFriend()
    {
        //Maybe we have this
    }
    public void ApplyBuffToPlayer()
    {
        foreach (Buff.Type type in turns[turnNumber].buffTypes)
        {
            player.AddBuff(type, turns[turnNumber].amount);
        }

    }
    public void Block()
    {
        thisEnemy.AddBlock(turns[turnNumber].amount);
    }
    public IEnumerator Attack()
    {
        // Animation


        //Attack
        int totalDamage = turns[turnNumber].amount + thisEnemy.strength.buffValue;
        if (player.vulnerable.buffValue > 0)
        {
            float round = totalDamage * 1.5f;
            totalDamage = (int)round;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < turns[turnNumber].number_of_hit; i++)
        {
            player.TakeDamage(totalDamage);
            yield return new WaitForSeconds(0.5f);
        }
        WarpUpTurn();
    }

    public void WarpUpTurn()
    {
        // Manage Special turn for each enemy
    }
    public void DisplayIntentAttack()
    {
        //Need more Icon for this 

    }
}
