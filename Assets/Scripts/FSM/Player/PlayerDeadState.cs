using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {

    }

    public override void UpdateState(PlayerController player)
    {
        
    }

    public override void PhysicsUpdate(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollisionEnter(PlayerController player)
    {
        
    }
}
