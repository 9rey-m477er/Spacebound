using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTest : NPC, ITalkable, ICollectable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private InventoryManager inventoryManager;
    private OmniDirectionalMovement john;
    public Item item;
    private bool collected;

    public override void Interact()
    {
        Talk(dialogueText);
        if (dialogueController.paragraphs.Count == 0)
        {
            dialogueController.EndConversation();
            Collect(item, 1);
        }
    }

    public void Collect(Item item, int amount)
    {
        inventoryManager.addItem(item, amount);
        this.gameObject.SetActive(false);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
