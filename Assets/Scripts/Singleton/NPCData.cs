using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New NPC", menuName = "NPCs")]
public class NPCData : ScriptableObject
{
    public Sprite portrait;
    public NPCTypes nPCTypes;
    public string nPCName;
    public float moveSpeed;
    public DialogueEntry[] dialogueEntries;
}

[System.Serializable]
public struct DialogueEntry
{
    public string question;
    public DialogueChoice goodAnswer;
    public DialogueChoice badAnswer;
}

[System.Serializable]
public struct DialogueChoice
{
    public string choiceText;
    public float suspicionAmount;
    public float alertAmount;
}


public enum NPCTypes
{
    Scientist,
    Creature,
    Soldier,
    Civillian
}
