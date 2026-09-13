using UnityEngine;

public class HumanTalkingState : HumanBaseState
{
    
    public override void EnterState(HumanStateManager humanState)
    {
        humanState.isTalking = true;
        DialogueEntry dialogueEntry = humanState.nPCData.dialogueEntries[Random.Range(0, humanState.nPCData.dialogueEntries.Length)];
        humanState.uIManager.ShowDialogue(dialogueEntry, humanState);
    }

    public override void UpdateState(HumanStateManager humanState)
    {
        
    }

    public override void OnCollisionEnter(HumanStateManager humanState)
    {
        
    }

}
