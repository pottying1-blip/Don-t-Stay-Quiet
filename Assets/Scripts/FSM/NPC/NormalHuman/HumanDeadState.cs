using UnityEngine;

public class HumanDeadState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        humanState.humanSoundSource.PlayOneShot(humanState.fleshSound);
        humanState.animator.SetBool("isScared", false);

        switch (humanState.deathCause)
        {
            case DeathCause.Pierced:
                humanState.animator.SetBool("isDead", true);
                humanState.isDead = true;
                break;
            case DeathCause.Explosion:
                humanState.isDead = true;
                break;
        }
        
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
