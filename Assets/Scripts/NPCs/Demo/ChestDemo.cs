using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDemo : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;

    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        Debug.Log("Dialogue Text is: " + dialogueText.name);
        dialogueController.displayNextParagraph(dialogueText);
    }
}
