using UnityEngine;

public class HumanAttackState : HumanBaseState
{
    
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("TAN CONG QUAI VAT");
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        float distance = Vector2.Distance(humanState.transform.position, humanState.playerController.transform.position);
        
        if (!humanState.playerController.isInvisible && distance < humanState.soldierAttackDis 
        && humanState.playerController.currentState != humanState.playerController.disguisedState)
        {
            Debug.Log("BAN QUAI VAT!");
        }
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

}
