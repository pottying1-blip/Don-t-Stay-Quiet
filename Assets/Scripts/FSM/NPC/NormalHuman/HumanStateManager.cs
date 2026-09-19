using System;
using UnityEngine;

public class HumanStateManager : MonoBehaviour
{
    HumanBaseState humanCurrentState;
    public HumanPatrolState humanPatrolState = new HumanPatrolState();
    public HumanInvestState humanInvestState = new HumanInvestState();
    public HumanScareState humanScareState = new HumanScareState();
    public HumanDeadState humanDeadState = new HumanDeadState();
    public HumanTalkingState humanTalkingState = new HumanTalkingState();
    public HumanAlertState humanAlertState = new HumanAlertState();
    public HumanAttackState humanAttackState = new HumanAttackState();
    public UnityEngine.Vector2 posA = new UnityEngine.Vector2(2.5f, 2f);
    public UnityEngine.Vector2 posB = new UnityEngine.Vector2(2.5f, -1f);
    public float patrolSpeed = 0.1f;
    public PlayerController playerController;
    public float scareDistance = 2f;
    public AudioSource humanSoundSource;
    public AudioClip gaspSound;
    public AudioClip walkingSound;
    public AudioClip fleshSound;
    public Animator animator;
    public bool isDead = false;
    public bool isTalking = false;
    public bool isMakingNoises = false;
    public float awarenessRange;
    public Vector2 investPos;
    public float suspicionLevel;
    public NPCData nPCData;
    public UIManager uIManager;
    public float patrolElapsedTime;
    public bool hasTalked = false;
    public float soldierAttackDis = 4f;
    void Start()
    {
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
}
