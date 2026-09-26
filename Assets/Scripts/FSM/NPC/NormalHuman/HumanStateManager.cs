using System;
using UnityEngine;
using UnityEngine.AI;

public class HumanStateManager : MonoBehaviour
{
    HumanBaseState humanCurrentState;
    public HumanPatrolState humanPatrolState = new HumanPatrolState();
    public HumanInvestState humanInvestState = new HumanInvestState();
    public HumanScareState humanScareState = new HumanScareState();
    public HumanDeadState humanDeadState = new HumanDeadState();
    public HumanTalkingState humanTalkingState = new HumanTalkingState();
    public HumanAlertState humanAlertState = new HumanAlertState();
    public DeathCause deathCause = DeathCause.Pierced;
    public HumanAttackState humanAttackState = new HumanAttackState();
    public UnityEngine.Vector2 posA = new UnityEngine.Vector2(2.5f, 2f);
    public UnityEngine.Vector2 posB = new UnityEngine.Vector2(2.5f, -1f);
    public Vector2 stationaryPatrolPos;
    public float patrolSpeed = 0.1f;
    public PlayerController playerController;
    public float scareDistance = 2f;
    public AudioSource humanSoundSource;
    public AudioClip gaspSound;
    public AudioClip walkingSound;
    public AudioClip fleshSound;
    public AudioClip gunSound;
    public Animator animator;
    public bool isDead = false;
    public bool isTalking = false;
    public bool isMakingNoises = false;
    public bool hasTalked = false;
    public bool isReturningToPos = false;
    public bool isPursuing = false;
    public bool isShaking = false;
    public float awarenessRange;
    public Vector2 investPos;
    public float suspicionLevel;
    public NPCData nPCData;
    public UIManager uIManager;
    public float patrolElapsedTime;
    public float soldierAttackDis = 6f;
    public float lastSpawnTime = 0f;
    public float wholeMapRadius = 50f;
    public float returnToPatrolTime = 10f;
    public float onHoldAttackTimer = 0f;
    public GameManager gameManager;
    public NavMeshAgent navMeshAgent;
    public Transform emergentPlace;
    public Vector2 humanPosition;
    void Start()
    {
        humanPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        humanCurrentState = humanPatrolState;
        humanCurrentState.EnterState(this);
        awarenessRange = nPCData.awarenessRadius;
    }

    void Update()
    {
        humanCurrentState.UpdateState(this);
    }

    public void SwitchState(HumanBaseState state)
    {
        humanCurrentState = state;
        state.EnterState(this);
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        SwitchState(humanDeadState);
    }

    public void SetDestination(Transform targetPos)
    {
        navMeshAgent.SetDestination(targetPos.position);
    }

    public void SetDestination(Vector2 targetPos)
    {
        navMeshAgent.SetDestination(targetPos);
    }
}
