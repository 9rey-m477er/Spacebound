using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGlacierE1 : NPC, ITalkable, ISaveable
{

    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    public DataPersistenceManager dataPersistenceManager;
    public override void Interact()
    {
        Talk(dialogueText);
        SaveGame();
    }

    public void SaveGame()
    {
        dataPersistenceManager.SaveGame();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
