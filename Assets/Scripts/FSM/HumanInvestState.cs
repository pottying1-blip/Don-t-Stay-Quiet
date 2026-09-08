using UnityEngine;

public class HumanInvestState : HumanBaseState
{
    public override void EnterState(HumanStateManager humanState)
    {
        Debug.Log("He is investigating the noise!");
        
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        Vector2 humanPos = humanState.transform.position;
        float speed = 2*Time.deltaTime;
        humanState.transform.position = Vector2.MoveTowards(humanPos, humanState.investPos, speed);
        if ((Vector2)humanState.transform.position == humanState.investPos)
        {
            Debug.Log("He has came to the position!");
        }

        float distance = Vector2.Distance(humanState.transform.position, 
        humanState.playerController.transform.position);
        if (humanState.playerController.isInvisible == false && distance < humanState.scareDistance)
        {
            humanState.SwitchState(humanState.humanScareState);
        }
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }
}
