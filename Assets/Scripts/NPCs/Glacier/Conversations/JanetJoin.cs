using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanetJoin : NPC, ITalkable, ITeamable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private OmniDirectionalMovement john;
    [SerializeField] private EncounterSaver encounterSaver;

    public override void Interact()
    {
        Talk(dialogueText);
        if (dialogueController.paragraphs.Count == 0)
        {
            dialogueController.EndConversation();
            joinTeam(2);
        }
    }

    public void joinTeam(int flag)
    {
        john.setTeammateActive(flag);
        encounterSaver.cleared = true;
        this.gameObject.SetActive(false);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }
}