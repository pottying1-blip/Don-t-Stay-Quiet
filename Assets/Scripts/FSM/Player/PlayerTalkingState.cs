using UnityEngine;

public class PlayerTalkingState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Has enter talking!");
        player.rb2d.linearVelocity = Vector2.zero;
    }

    public override void UpdateState(PlayerController player)
    {
        player.rb2d.linearVelocity = Vector2.zero;
        if (!player.isTalking)
        {
            player.SwitchState(player.disguisedState);
        }
    }

    public override void PhysicsUpdate(PlayerController player)
    {
        
    }

    public override void OnCollisionEnter(PlayerController player)
    {
        
    }
}
