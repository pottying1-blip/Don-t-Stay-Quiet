using UnityEngine;

public class HumanAlertState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("Started alerting!");
    }

    public override void UpdateState(HumanStateManager humanState)
    {

    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
