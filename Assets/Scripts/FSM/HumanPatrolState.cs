using System.Collections;
using System.Numerics;
using UnityEngine;

public class HumanPatrolState : HumanBaseState
{

    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("The human is working");
    }

    public override void UpdateState(HumanStateManager humanState)
    {   
        float distance = UnityEngine.Vector2.Distance(humanState.transform.position, 
        humanState.playerController.transform.position);

        if (humanState.playerController.isInvisible == false && distance < humanState.scareDistance)
        {
            humanState.SwitchState(humanState.humanScareState);

        } else 
        {
            float pingPongValue = Mathf.PingPong(Time.time*humanState.patrolSpeed, 1f);
            humanState.transform.position = UnityEngine.Vector2.Lerp(humanState.posA, humanState.posB, pingPongValue);
            
        }

        Collider2D[] surrounds = Physics2D.OverlapCircleAll(humanState.transform.position, humanState.awarenessRadius);
        foreach (Collider2D obj in surrounds)
        {
            
            if (obj.TryGetComponent<InteractableObject>(out var interactableObject))
            {
                if (interactableObject.IsMakingNoise())
                {
                    humanState.SwitchState(humanState.humanInvestState);
                    humanState.investPos = interactableObject.transform.position;
                    break;
                }
            }
        }
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
