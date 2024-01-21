using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<EnemyAction> enemyActions;
    public List<EnemyAction> turns = new List<EnemyAction>();
    public int turnNumber;
    public bool shuffleActions;
    public Fighter thisEnemy;

    [Header("Intent_Action")]
    public IntentUI intentUI;

    [Header("Specifics")]
    public int goldDrop;
    public bool isBoss;

    public bool flyingEye;
    public bool demonSlime;
    public bool mushroom;
    public GameObject targetObject;
    public BattleManager BattleManager;
    public bool midTurn;

    public Animator animator;
    public Fighter player;

    //Animator
    private void Awake()
    {
        thisEnemy = GetComponent<Fighter>();
        BattleManager = FindObjectOfType<BattleManager>();
        intentUI = GetComponentInChildren<IntentUI>();
        player = BattleManager.player;
        turnNumber = 0;
    }
    private void Start()
    {
        BattleManager = FindObjectOfType<BattleManager>();
        player = BattleManager.player;


        //animator = GetComponent<Animator>();

        if (shuffleActions)
            GenerateTurns();

        DisplayIntentAttack();
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
        if (shuffleActions)
            GenerateTurns();
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
        turns.Shuffle();
    }
    public void TakeTurn()
    {
        Debug.Log("Enemy Take Turn!");
        intentUI.IntentFade();
        if (thisEnemy.stun.buffValue > 0)
        {
            return;
        }
        //Animator
        switch (turns[turnNumber].intentType)
        {
            case EnemyAction.IntentType.Attack:
                StartCoroutine(Attack());
                WarpUpTurn();
                break;
            case EnemyAction.IntentType.Block:
                Block();
                WarpUpTurn();
                break;
            case EnemyAction.IntentType.StrategicBuff:
                ApplyBuffToSelf();
                WarpUpTurn();
                break;
            case EnemyAction.IntentType.StrategicDebuff:
                ApplyBuffToPlayer();
                WarpUpTurn();
                break;
            case EnemyAction.IntentType.AttackDebuff:

                StartCoroutine(Attack()); ApplyBuffToPlayer();
                WarpUpTurn();
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
            player.AddBuff(type, turns[turnNumber].debuffAmount);
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
        float round = 0;
        int totalDamage = turns[turnNumber].amount + thisEnemy.strength.buffValue;
        if (player.vulnerable.buffValue > 0)
        {
            round = totalDamage * 1.5f;
            totalDamage = (int)round;
        }
        if (thisEnemy.weak.buffValue > 0)
        {
            round = totalDamage * 0.75f;
            totalDamage = (int)round;
        }
        animator.Play("Attack");
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < turns[turnNumber].number_of_hit; i++)
        {
            Debug.Log("Enemy hit: " + totalDamage);
            player.TakeDamage(totalDamage);
        }

        animator.Play("Idle");

    }

    public void WarpUpTurn()
    {
        // Manage Special turn for each enemy and Endturn manage
        turnNumber++;
        if (turnNumber == turns.Count)
        {
            turnNumber = 0;
        }
        if (demonSlime)
        {
            if(turnNumber == 0 || turnNumber == 4)
            {
                turnNumber = 1;
            }
            
        }
        if (demonSlime && thisEnemy.currentHealth <= (thisEnemy.maxHealth / 2))
        {
            turnNumber = 5;
        }
        if (mushroom && turnNumber == 0)
        {
            turnNumber = 1;
        }
        DisplayIntentAttack();
        thisEnemy.UpdateAtTurnStart();
        
        midTurn = false;
    }
    public void DisplayIntentAttack()
    {
        Debug.Log("DisplayIntent");
        //Need more Icon for this 
        if (turns.Count == 0)
        {
            intentUI.DisplayIntent(turns[0].icon);
            return;
        }
        intentUI.DisplayIntent(turns[turnNumber].icon);
    }
}
