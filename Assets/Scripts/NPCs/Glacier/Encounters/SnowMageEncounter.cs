using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMageEncounter : NPC, ITalkable, IBattleable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private BossStatSheet enemy;
    [SerializeField] private AudioClip encounterIntro, encounterMusic;
    [SerializeField] private GameObject bossSystem;
    [SerializeField] private BossBattleUIScript bossScript;
    [SerializeField] private EncounterSaver encounterSaver;
    private bool battleStarted = false;

    public override void Interact()
    {
        Talk(dialogueText);
        if (!battleStarted && dialogueController.paragraphs.Count == 0)
        {
            dialogueController.EndConversation();
            Battle(enemy);
        }
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }

    public void Battle(BossStatSheet enemy)
    {
        battleStarted = true;
        bossSystem.SetActive(true);
        bossScript.StartScriptedBattle(enemy);
        encounterSaver.cleared = true;
        this.gameObject.SetActive(false);
    }
}
