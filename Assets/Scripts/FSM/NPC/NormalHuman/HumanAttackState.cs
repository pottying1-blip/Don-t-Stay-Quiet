
using Unity.VisualScripting;
using UnityEngine;

public class HumanAttackState : HumanBaseState
{
    
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("QUAI VAT!");
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        float rotationSpeed = 50f;
        float distance = Vector2.Distance(humanState.transform.position, humanState.playerController.transform.position);
        Vector2 direction = (Vector2)humanState.playerController.transform.position - (Vector2)humanState.transform.position;
        float rotateAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion entityRotation = Quaternion.Euler(0,0,rotateAngle);

        if (!humanState.playerController.isInvisible && distance < humanState.soldierAttackDis 
        && humanState.playerController.currentState != humanState.playerController.disguisedState)
        {
            humanState.transform.rotation = Quaternion.RotateTowards(humanState.transform.rotation,
            entityRotation, rotationSpeed*Time.deltaTime);
            Debug.Log("BAN QUAI VAT!");
        }
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

}
