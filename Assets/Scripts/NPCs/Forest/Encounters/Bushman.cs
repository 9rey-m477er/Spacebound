using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bushman : NPC, ITalkable, IBattleable, IDataPersistence
{

    [SerializeField] private string id;

    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private EnemyStatSheet enemy;
    [SerializeField] private AudioClip encounterIntro, encounterMusic;
    [SerializeField] private GameObject bossSystem;
    [SerializeField] private BossBattleUIScript bossScript;
    private bool battleStarted = false;
    private bool defeated = false;

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

    public void Battle(EnemyStatSheet enemy)
    {
        battleStarted = true;
        bossSystem.SetActive(true);
        bossScript.StartScriptedBattle(enemy);
        this.gameObject.SetActive(false);
    }

    public void LoadData(GameData data)
    {
        data.bossesDefeated.TryGetValue(id, out defeated);
        if (defeated)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        Debug.Log("Saving Bushman");
        if (data.bossesDefeated.ContainsKey(id))
        {
            data.bossesDefeated.Remove(id);
        }
        data.bossesDefeated.Add(id, defeated);
    }
}
