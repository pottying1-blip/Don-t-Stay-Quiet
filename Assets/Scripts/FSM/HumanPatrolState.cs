using UnityEngine;

public class HumanPatrolState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("The human is working");
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
