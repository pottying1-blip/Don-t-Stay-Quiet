using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New NPC", menuName = "NPCs")]
public class NPCData : ScriptableObject
{
    public Sprite portrait;
    public NPCTypes nPCTypes;
    public string nPCName;
    public int moveSpeed;
    public string[] dialogueOpts;
}


public enum NPCTypes
{
    Scientist,
    Creature,
    Soldier,
    Civillian
}
