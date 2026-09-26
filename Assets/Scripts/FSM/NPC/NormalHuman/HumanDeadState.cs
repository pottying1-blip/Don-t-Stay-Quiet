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
                humanState.animator.SetBool("isDeadByPierced", true);
                humanState.isDead = true;
                break;
            case DeathCause.Explosion:
                humanState.animator.SetBool("isDeadByExplosion", true);
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
