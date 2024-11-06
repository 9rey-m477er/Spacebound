using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMForestEntrance : NPC, ITalkable, ISaveable
{

    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    public SaveLoadSystem saveManager;
    public int machineID;
    public override void Interact()
    {
        Talk(dialogueText);
        SaveGame(machineID);
    }

    public void SaveGame(int saveMachine)
    {
        throw new System.NotImplementedException();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
