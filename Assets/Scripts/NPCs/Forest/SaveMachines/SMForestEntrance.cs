using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMForestEntrance : NPC, ITalkable, ISaveable
{

    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    public DataPersistenceManager dataPersistenceManager;
    public int machineID;
    public override void Interact()
    {
        Talk(dialogueText);
        SaveGame();
    }

    public void SaveGame()
    {
        Debug.Log("Saving Game");
        dataPersistenceManager.SaveGame();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
