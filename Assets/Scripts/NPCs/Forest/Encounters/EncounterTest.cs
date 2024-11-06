using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTest : NPC, ITalkable, IBattleable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private GameObject enemy;
    private OmniDirectionalMovement john;

    public void Start()
    {
        john = GameObject.FindGameObjectWithTag("Player").GetComponent<OmniDirectionalMovement>();
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

    public void Battle(GameObject enemyPrefab)
    {
        john.StartScriptedBattle(enemyPrefab);
    }
}
