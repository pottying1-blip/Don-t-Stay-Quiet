using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class HumanPatrolState : HumanBaseState
{

    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("Patrolling");
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        if (!humanState.isDead)
        {
            humanState.patrolElapsedTime += Time.deltaTime;

            float distance = UnityEngine.Vector2.Distance(humanState.transform.position, 
            humanState.playerController.transform.position);

            if (humanState.playerController.isInvisible == false && distance < humanState.scareDistance
            && humanState.playerController.currentState != humanState.playerController.disguisedState)
            {
                humanState.SwitchState(humanState.humanScareState);
            }
            else 
            {
                HandlePatrol(humanState);
            }

            if (humanState.gameManager.hasAlert && humanState.nPCData.nPCTypes == NPCTypes.Soldier)
            {
                humanState.SwitchState(humanState.humanAlertState);
            }

            if (humanState.playerController.isInvisible == false && distance < humanState.scareDistance 
            && humanState.playerController.currentState == humanState.playerController.disguisedState && !humanState.playerController.isTalking)
            {
                humanState.SwitchState(humanState.humanTalkingState);
            }

            CheckNoiseInvestigation(humanState);
        }
        else {humanState.Die();}
    }

    void CheckNoiseInvestigation(HumanStateManager humanState)
    {
        Collider2D[] surrounds = Physics2D.OverlapCircleAll(humanState.transform.position, humanState.awarenessRange);
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

    void HandlePatrol(HumanStateManager humanState)
    {
        if (!humanState.nPCData.canPatrol) return;
        float pingPongValue = Mathf.PingPong(humanState.patrolElapsedTime*humanState.patrolSpeed, 1f);
        humanState.transform.position = UnityEngine.Vector2.Lerp(humanState.posA, humanState.posB, pingPongValue);
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
