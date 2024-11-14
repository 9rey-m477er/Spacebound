using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTest : NPC, ITalkable, IBattleable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private EnemyStatSheet enemy;
    [SerializeField] private AudioClip encounterIntro, encounterMusic;
    [SerializeField] private GameObject bossSystem;
    private BossBattleUIScript bossScript;

    public void Start()
    {
        bossScript = GameObject.FindGameObjectWithTag("BossSystem").GetComponent<BossBattleUIScript>();
    }

    public override void Interact()
    {
        Talk(dialogueText);
        Battle(enemy);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.displayNextParagraph(dialogueText);
    }

    public void Battle(EnemyStatSheet enemy)
    {
        bossSystem.SetActive(true);
        bossScript.StartScriptedBattle(enemy);
    }
}
