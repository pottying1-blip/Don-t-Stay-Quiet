using System;
using UnityEngine;

public class HumanStateManager : MonoBehaviour
{
    HumanBaseState humanCurrentState;
    public HumanPatrolState humanPatrolState = new HumanPatrolState();
    public HumanInvestState humanInvestState = new HumanInvestState();
    public HumanScareState humanScareState = new HumanScareState();
    public UnityEngine.Vector2 posA = new UnityEngine.Vector2(2.5f, 2f);
    public UnityEngine.Vector2 posB = new UnityEngine.Vector2(2.5f, -1f);
    public float patrolSpeed = 0.5f;
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
}
