using System;
using UnityEngine;

public class HumanStateManager : MonoBehaviour
{
    HumanBaseState humanCurrentState;
    public HumanPatrolState humanPatrolState = new HumanPatrolState();
    public HumanInvestState humanInvestState = new HumanInvestState();
    public HumanScareState humanScareState = new HumanScareState();
    public HumanDeadState humanDeadState = new HumanDeadState();
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
    public bool isDead;
    public bool isMakingNoises = false;

    void Start()
    {
        humanCurrentState = humanPatrolState;
        humanCurrentState.EnterState(this);
        
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
