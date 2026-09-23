using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class HumanAlertState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("Started alerting!");
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        Transform closestAlert = FindClosestAlertButton(humanState.transform.position, humanState);
        if (closestAlert != null && !humanState.isDead)
        {
            if (!humanState.gameManager.hasAlert && humanState.nPCData.nPCTypes == NPCTypes.Scientist)
            {humanState.SetDestination(closestAlert);}

            else if (humanState.gameManager.hasAlert && humanState.nPCData.nPCTypes == NPCTypes.Scientist)
            {
                humanState.StartCoroutine(HorrifiedShaking(humanState));
            }   

            if (!humanState.gameManager.hasAlert && humanState.nPCData.nPCTypes == NPCTypes.Scientist 
            && !humanState.navMeshAgent.pathPending 
            && humanState.navMeshAgent.remainingDistance <= humanState.navMeshAgent.stoppingDistance )
            {
                humanState.gameManager.WarningStart = true;
                humanState.gameManager.hasAlert = true;
                humanState.emergentPlace = closestAlert;
                humanState.gameManager.currentAlert = closestAlert;
            }
        }


        if (humanState.nPCData.nPCTypes == NPCTypes.Soldier && humanState.gameManager.hasAlert)
        {
            float distance = UnityEngine.Vector2.Distance(humanState.transform.position, humanState.playerController.transform.position);
            float currentDistance = Vector2.Distance(humanState.transform.position, humanState.gameManager.currentAlert.transform.position);
            humanState.SetDestination(humanState.gameManager.currentAlert);
            
            if (currentDistance < 3f)
            {
                humanState.SwitchState(humanState.humanAttackState);
            }
        }
    }

    IEnumerator HorrifiedShaking(HumanStateManager humanState)
    {
        Vector2 shakingIntensity = Random.insideUnitCircle * 0.015f;
        humanState.transform.position = (Vector2)humanState.transform.position + shakingIntensity;
        yield return new WaitForSeconds(0.2f);
        humanState.transform.position = (Vector2)humanState.transform.position;

        if (humanState.gameManager.falseAlarm)
        {
            humanState.isReturningToPos = true;
            if (humanState.nPCData.canPatrol)
            {
                humanState.SetDestination(humanState.posA);
            } 
            else
            {humanState.SetDestination(humanState.stationaryPatrolPos);}
        }

        if (humanState.isReturningToPos 
        && !humanState.navMeshAgent.pathPending 
        && humanState.navMeshAgent.remainingDistance <= 0.01f)
        {
            humanState.isReturningToPos = false;
            humanState.SwitchState(humanState.humanPatrolState);
        }
    }

    public Transform FindClosestAlertButton(Vector2 selfPos, HumanStateManager humanState)
    {
        Transform closestButton = null;
        float closestDistance = float.MaxValue;

        foreach (Transform alertButton in humanState.gameManager.allAlertButtons)
        {
            if (alertButton.TryGetComponent<AlertReturn>(out var alertReturn) && alertReturn.isResolved) continue;
            float distance = Vector2.Distance(selfPos, alertButton.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestButton = alertButton;
            }
        }
        return closestButton;
    }
    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
