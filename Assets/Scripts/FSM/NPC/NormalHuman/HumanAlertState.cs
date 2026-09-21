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
            humanState.SetDestination(closestAlert);
            
            if (humanState.transform.position == closestAlert.transform.position)
            {
                humanState.gameManager.WarningStart = true;
            }
        }
    }

    public Transform FindClosestAlertButton(Vector2 selfPos, HumanStateManager humanState)
    {
        Transform closestButton = null;
        float closestDistance = float.MaxValue;

        foreach (Transform alertButton in humanState.gameManager.allAlertButtons)
        {
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
