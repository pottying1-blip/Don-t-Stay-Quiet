using UnityEngine;

public class HumanDeadState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        humanState.humanSoundSource.PlayOneShot(humanState.fleshSound);
        humanState.animator.SetBool("isScared", false);
        humanState.animator.SetBool("isDead", true);
        
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
