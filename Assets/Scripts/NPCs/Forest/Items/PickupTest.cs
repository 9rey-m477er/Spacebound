using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTest : NPC, ITalkable, ICollectable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    private OmniDirectionalMovement john;

    public override void Interact()
    {
        Talk(dialogueText);
        Collect("rock", 1);
    }

    public void Collect(string item, int amount)
    {
        throw new System.NotImplementedException();
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
