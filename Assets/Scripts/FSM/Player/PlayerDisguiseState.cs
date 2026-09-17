using System.Collections;
using UnityEngine;

public class PlayerDisguiseState : PlayerBaseState
{
    public override void EnterState(PlayerController player)
    {
        
    }

    public override void UpdateState(PlayerController player)
    {
        if (player.isTalking)
        {
            player.SwitchState(player.talkingState);
        }
        if (!player.isPossessed) return;
        HandleDecaying(player);
    }

    public void HandleDecaying(PlayerController player)
    {
        player.possessCount += Time.deltaTime;
        if (player.possessCount >= player.possessThreshold
         || Input.GetKeyDown(KeyCode.G))
        {
            player.isPossessed = false;
            player.possessCount = 0;
            player.spriteRenderer.sprite = player.currentBaseForm;
            player.SwitchState(player.normalState);
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
