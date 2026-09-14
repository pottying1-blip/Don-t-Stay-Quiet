using UnityEngine;

public class PlayerDisguiseState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.isPossessed = true;
    }

    public override void UpdateState(PlayerController player)
    {
        if (player.isTalking)
        {
            player.SwitchState(player.talkingState);
        }
    }

    public override void PhysicsUpdate(PlayerController player)
    {
        player.MovementInput();
    }

    public override void OnCollisionEnter(PlayerController player)
    {
        
    }
}
