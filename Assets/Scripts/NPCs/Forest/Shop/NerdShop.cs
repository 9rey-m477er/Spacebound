using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdShop : NPC, IShopable, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private Shop shop;

    public override void Interact()
    {
        Talk(dialogueText);
        if (dialogueController.paragraphs.Count == 0)
        {
            dialogueController.EndConversation();
            openShop(shop);
        }
    }

    public void openShop(Shop shop)
    {
        shopManager.OpenShop(shop);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}

