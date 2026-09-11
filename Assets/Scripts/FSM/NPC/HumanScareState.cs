using System.Collections;
using UnityEngine;

public class HumanScareState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("He is scared!");
        humanState.humanSoundSource.PlayOneShot(humanState.gaspSound);
        humanState.animator.SetBool("isScared", true);
        humanState.isMakingNoises = true;
        
        humanState.StartCoroutine(StopNoiseMaking(humanState));
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

    IEnumerator StopNoiseMaking(HumanStateManager humanState)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        humanState.isMakingNoises = false;
    }

}
