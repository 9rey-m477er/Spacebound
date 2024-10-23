using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushDemo : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;

    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
