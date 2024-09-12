using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Vector2 target;
    private PlayerMovement player;
    public bool ranged;
    public bool sniper;
    public bool uzi;
    public bool pistol;

    public int sniperAttackDistance;
    public int pistolAttackDistance;
    public float attackTimerDuration;
    [SerializeField] private float attackTimer;

    public int sniperRetreatDistance;

    public enum EnemyStates
    {
        chase,
        attack,
        retreat
    }

    public EnemyStates enemyState;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyState = EnemyStates.chase;
        attackTimer = attackTimerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyStates.chase:
                target = player.transform.position;
                navMeshAgent.SetDestination(target);
                if (uzi)
                {
                    //uzi will not have an "attack state", it will attack while chasing.
                    //attack here
                }
                if (sniper || pistol)
                {
                    if (pistol)
                    {
                        if (Vector2.Distance(transform.position, target) < pistolAttackDistance)
                        {
                            enemyState = EnemyStates.attack;
                        }
                    }
                    if (sniper)
                    {
                        if (Vector2.Distance(transform.position, target) < sniperAttackDistance)
                        {
                            enemyState = EnemyStates.attack;
                        }
                    }
                }
                if (sniper)
                {
                    if (Vector2.Distance(transform.position, target) < sniperRetreatDistance)
                    {
                        enemyState = EnemyStates.retreat;
                    }
                }
                break;
            case EnemyStates.attack:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    if (pistol)
                    {
                        //attack
                        attackTimer = attackTimerDuration;
                    }
                    else if (sniper)
                    {
                        //attack
                        attackTimer = attackTimerDuration;
                    }
                }
                if (pistol)
                {
                    if (Vector2.Distance(transform.position, player.transform.position) > pistolAttackDistance)
                    {
                        enemyState = EnemyStates.chase;
                        attackTimer = attackTimerDuration;
                    }
                }
                if (sniper)
                {
                    if (Vector2.Distance(transform.position, player.transform.position) > sniperAttackDistance)
                    {
                        enemyState = EnemyStates.chase;
                        attackTimer = attackTimerDuration;
                    }
                    if (Vector2.Distance(transform.position, target) < sniperRetreatDistance)
                    {
                        enemyState = EnemyStates.retreat;
                        attackTimer = attackTimerDuration;
                    }
                }
                break;
            // Sniper ONLY
            case EnemyStates.retreat:
                target = transform.position - player.transform.position;
                navMeshAgent.SetDestination(target);
                if (Vector2.Distance(transform.position, player.transform.position) > sniperRetreatDistance)
                {
                    enemyState = EnemyStates.chase;
                }
                break;
        }
    }
}

//sniper will chase the player and attack when it gets close enough, but run away when you get too close.
//pistol will chase the player and stand there and attack when it gets close enough, and will not retreat. It will reposition when you get out of range.
//uzi will chase the player and attack while chasing the player. It does not care if you are too close.
//melee will chase the player and will do contact damage to the player. It does not retreat or "attack".