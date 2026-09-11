using UnityEngine;

public class PlayerDisguiseState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        player.isPossessed = true;
    }

    public override void UpdateState(PlayerController player)
    {
        
    }

    public override void OnCollisionEnter(PlayerController player)
    {
        
    }
}
