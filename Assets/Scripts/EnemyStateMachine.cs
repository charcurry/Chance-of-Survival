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

    public int rangedAttackDistance;
    public int attackDistance;
    public float attackTimerDuration;
    [SerializeField] private float attackTimer;

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
                if (ranged)
                {
                    if (Vector2.Distance(transform.position, target) < rangedAttackDistance)
                    {
                        enemyState = EnemyStates.attack;
                    }
                }
                break;
            case EnemyStates.attack:
                attackTimer -= Time.deltaTime;
                if (attackTimer <= 0f)
                {
                    if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
                    {
                        enemyState = EnemyStates.chase;
                        attackTimer = attackTimerDuration;
                    }
                    else
                    {
                        attackTimer = attackTimerDuration;
                    }
                }
                break;
            case EnemyStates.retreat:
                break;
        }
    }
}
