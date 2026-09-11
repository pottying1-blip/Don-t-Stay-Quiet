using UnityEngine;

public class PlayerNormalState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {

    }

    public override void UpdateState(PlayerController player)
    {
        player.KillNPC();
        if (player.isPossessed)
        {
            player.SwitchState(player.disguisedState);
        }
    }

    public override void OnCollisionEnter(PlayerController player)
    {
        
    }
}
