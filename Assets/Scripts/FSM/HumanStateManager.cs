using System;
using UnityEngine;

public class HumanStateManager : MonoBehaviour
{
    HumanBaseState humanCurrentState;
    HumanPatrolState humanPatrolState = new HumanPatrolState();
    HumanInvestState humanInvestState = new HumanInvestState();
    HumanScareState humanScareState = new HumanScareState();

    void Start()
    {
        humanCurrentState = humanPatrolState;
        humanCurrentState.EnterState(this);
    }

    void Update()
    {
        humanCurrentState.UpdateState(this);
    }
}
