using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class HumanTalkingState : HumanBaseState
{
    
    public override void EnterState(HumanStateManager humanState)
    {
        humanState.isTalking = true;
        if (humanState.isTalking && !humanState.hasTalked)
        {
            DialogueEntry dialogueEntry = humanState.nPCData.dialogueEntries[Random.Range(0, humanState.nPCData.dialogueEntries.Length)];
            humanState.uIManager.ShowDialogue(dialogueEntry, humanState);

            humanState.StartCoroutine(ReturnToWork(humanState));
            humanState.StartCoroutine(ConversationInterval(humanState));
        } 
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        if (!humanState.isTalking && humanState.hasTalked)
        {
            humanState.SwitchState(humanState.humanPatrolState);
        }
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

    IEnumerator ReturnToWork(HumanStateManager humanState)
    {
        yield return new WaitForSecondsRealtime(1f);
        humanState.isTalking = false;
        humanState.hasTalked = true;
    }

    IEnumerator ConversationInterval(HumanStateManager humanState)
    {
        yield return new WaitForSecondsRealtime(15f);
        humanState.hasTalked = false;
    }

}
