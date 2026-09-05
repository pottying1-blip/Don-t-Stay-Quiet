using UnityEngine;

public class HumanScareState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("He is scared!");
        humanState.humanSoundSource.PlayOneShot(humanState.gaspSound);
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

}
