using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FRoomChest : NPC, ITalkable, ICollectable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private EncounterSaver encounterSaver;
    private OmniDirectionalMovement john;
    public Item item;

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
        encounterSaver.cleared = true;
        this.gameObject.SetActive(false);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}
