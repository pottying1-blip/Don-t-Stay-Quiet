using System.Numerics;
using UnityEngine;

public class HumanPatrolState : HumanBaseState
{

    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("The human is working");
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        float pingPongValue = Mathf.PingPong(Time.deltaTime*humanState.patrolSpeed, 1f);
        humanState.transform.position = UnityEngine.Vector2.Lerp(humanState.posA, humanState.posB, pingPongValue);
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
