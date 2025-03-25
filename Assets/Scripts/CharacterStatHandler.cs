using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour, IDataPersistence
{
    public CharacterStatSheet John;
    public CharacterStatSheet Bob;
    public CharacterStatSheet Thozos;
    public CharacterStatSheet Janet;
    public CharacterStatSheet Stephven;
    public int partyEXP;
    public int expToNext;
    public int partyLevel;
    public int levelMax = 30;
    public bool devTools;
    public List<CharacterStatSheet> battleOrder;

    //Load Stats from Data
    public void LoadData(GameData data)
    {
        //Set Party Level
        partyLevel = data.partyLevel;
        //Set John's stats
        John.characterLevel = partyLevel;
        John.characterHP = data.jHP;
        John.currentHP = data.jCHP;
        John.bashMultiplier = data.jBash;
        John.slashMultiplier = data.jSlash;
        John.pokeMultiplier = data.jPoke;
        //Set Bob's stats
        Bob.characterLevel = partyLevel;
        Bob.characterHP = data.bHP;
        Bob.currentHP = data.bCHP;
        Bob.bashMultiplier = data.bBash;
        Bob.slashMultiplier = data.bSlash;
        Bob.pokeMultiplier = data.bPoke;
        //Set Thozos' stats
        Thozos.characterLevel = partyLevel;
        Thozos.characterHP = data.tHP;
        Thozos.currentHP = data.tCHP;
        Thozos.bashMultiplier = data.tBash;
        Thozos.slashMultiplier = data.tSlash;
        Thozos.pokeMultiplier = data.tPoke;
        //Set Janet's stats
        Janet.characterLevel = partyLevel;
        Janet.characterHP = data.nHP;
        Janet.currentHP = data.nCHP;
        Janet.bashMultiplier= data.nBash;
        Janet.slashMultiplier = data.nSlash;
        Janet.pokeMultiplier = data.nPoke;
        //Set Stephven's statts
        Stephven.characterLevel = partyLevel;
        Stephven.characterHP = data.sHP;
        Stephven.currentHP = data.sCHP;
        Stephven.bashMultiplier = data.sBash;
        Stephven.slashMultiplier = data.sSlash;
        Stephven.pokeMultiplier = data.sPoke;
        //Set General Information
        partyEXP = data.partyExp;
        expToNext = data.expToNext;

    }

    //Save Stats to Data
    public void SaveData(ref GameData data)
    {
        data.partyLevel = partyLevel;
        //Save John's stats
        data.jHP = John.characterHP;
        data.jCHP = John.currentHP;
        data.jBash = John.bashMultiplier;
        data.jSlash = John.slashMultiplier;
        data.jPoke = John.pokeMultiplier;
        //Save Bob's stats
        data.bHP = Bob.characterHP;
        data.bCHP = Bob.currentHP;
        data.bBash = Bob.bashMultiplier;
        data.bSlash = Bob.slashMultiplier;
        data.bPoke = Bob.pokeMultiplier;
        //Save Thozos' stats
        data.tHP = Thozos.characterHP;
        data.tCHP = Thozos.currentHP;
        data.tBash = Thozos.bashMultiplier;
        data.tSlash = Thozos.slashMultiplier;
        data.tPoke = Thozos.pokeMultiplier;
        //Save Janet's stats
        data.nHP = Janet.characterHP;
        data.nCHP = Janet.currentHP;
        data.nBash = Janet.bashMultiplier;
        data.nSlash = Janet.slashMultiplier;
        data.nPoke = Janet.pokeMultiplier;
        //Save Stephven's statts
        data.sHP = Stephven.characterHP;
        data.sCHP = Stephven.currentHP;
        data.sBash = Stephven.bashMultiplier;
        data.sSlash = Stephven.slashMultiplier;
        data.sPoke = Stephven.pokeMultiplier;
        //Set General Information
        data.partyExp = partyEXP;
        data.expToNext = expToNext;
    }

    //Add EXP to partyEXP
    public void addEXP(int exp)
    {
        Debug.Log("Adding " +  exp + " exp!");
        partyEXP += exp;
        if (partyEXP >= expToNext && partyLevel < levelMax)
        {
            partyLevelUp();
            Debug.Log("Next Level Reached");
        }
        else if (partyLevel >= levelMax)
        {
            Debug.Log("Max Level Reached");
        }
    }

    public void healCharacters()
    {
        John.currentHP = John.characterHP;
        Bob.currentHP = Bob.characterHP;
        Thozos.currentHP = Thozos.characterHP;
        Janet.currentHP = Janet.characterHP;
        Stephven.currentHP = Stephven.characterHP;
    }

    //Level Up Party (+0.1 for = (4), +0.13 for + (5), +0.07 for - (3))
    public void partyLevelUp()
    {
        partyLevel++;
        partyEXP -= expToNext;
        John.characterLevel = partyLevel;
        Bob.characterLevel = partyLevel;
        Thozos.characterLevel = partyLevel;
        Janet.characterLevel = partyLevel;
        Stephven.characterLevel = partyLevel;

        //Regular Level Up
        if (partyLevel < levelMax - 1)
        {
            //LevelUpJohn
            John.characterHP += 5;
            John.currentHP = John.characterHP;
            John.bashMultiplier += 0.1f;
            John.slashMultiplier += 0.1f;
            John.pokeMultiplier += 0.1f;
            //LevelUpBob
            Bob.characterHP += 5;
            Bob.currentHP = Bob.characterHP;
            Bob.bashMultiplier += 0.13f;
            Bob.slashMultiplier += 0.1f;
            Bob.pokeMultiplier += 0.07f;
            //LevelUpThozos
            Thozos.characterHP += 5;
            Thozos.currentHP = Thozos.characterHP;
            Thozos.bashMultiplier += 0.07f;
            Thozos.slashMultiplier += 0.07f;
            Thozos.pokeMultiplier += 0.07f;
            //LevelUpJanet
            Janet.characterHP += 5;
            Janet.currentHP = Janet.characterHP;
            Janet.bashMultiplier += 0.1f;
            Janet.slashMultiplier += 0.07f;
            Janet.pokeMultiplier += 0.13f;
            //LevelUpStephven
            Stephven.characterHP += 5;
            Stephven.currentHP = Stephven.characterHP;
            Stephven.bashMultiplier += 0.07f;
            Stephven.slashMultiplier += 0.13f;
            Stephven.pokeMultiplier += 0.1f;
        }
        else
        {
            //LevelUpJohn
            John.characterHP += 10;
            John.bashMultiplier += 0.2f;
            John.slashMultiplier += 0.2f;
            John.pokeMultiplier += 0.2f;
            //LevelUpBob
            Bob.characterHP += 10;
            Bob.bashMultiplier += 0.26f;
            Bob.slashMultiplier += 0.2f;
            Bob.pokeMultiplier += 0.14f;
            //LevelUpThozos
            Thozos.characterHP += 10;
            Thozos.bashMultiplier += 0.14f;
            Thozos.slashMultiplier += 0.14f;
            Thozos.pokeMultiplier += 0.14f;
            //LevelUpJanet
            Janet.characterHP += 10;
            Janet.bashMultiplier += 0.2f;
            Janet.slashMultiplier += 0.14f;
            Janet.pokeMultiplier += 0.26f;
            //LevelUpStephven
            Stephven.characterHP += 10;
            Stephven.bashMultiplier += 0.14f;
            Stephven.slashMultiplier += 0.26f;
            Stephven.pokeMultiplier += 0.2f;
        }


        Debug.Log("Level " + partyLevel + " reached.");

        expToNext += 200;
        if (partyEXP >= expToNext)
        {
            partyLevelUp();
        }
    }

    //Dev Tool for debugging.
    public void resetCharacterStats()
    {
        //Set John's stats
        John.characterLevel = 1;
        John.characterHP = 30;
        John.currentHP = 30;
        John.bashMultiplier = 1.0f;
        John.slashMultiplier = 1.0f;
        John.pokeMultiplier = 1.0f;
        //Set Bob's stats
        Bob.characterLevel = 1;
        Bob.characterHP = 40;
        Bob.currentHP = 40;
        Bob.bashMultiplier = 1.1f;
        Bob.slashMultiplier = 1.0f;
        Bob.pokeMultiplier = 0.9f;
        //Set Thozos' stats
        Thozos.characterLevel = 1;
        Thozos.characterHP = 20;
        Thozos.currentHP = 20;
        Thozos.bashMultiplier = 0.9f;
        Thozos.slashMultiplier = 0.9f;
        Thozos.pokeMultiplier = 0.9f;
        //Set Janet's stats
        Janet.characterLevel = 1;
        Janet.characterHP = 35;
        Janet.currentHP = 35;
        Janet.bashMultiplier = 1.0f;
        Janet.slashMultiplier = 0.9f;
        Janet.pokeMultiplier = 1.1f;
        //Set Stephven's statts
        Stephven.characterLevel = 1;
        Stephven.characterHP = 25;
        Stephven.currentHP = 25;
        Stephven.bashMultiplier = 0.9f;
        Stephven.slashMultiplier = 1.1f;
        Stephven.pokeMultiplier = 1.0f;
        //Set EXP
        partyEXP = 0;
        partyLevel = 1;
        expToNext = 100;
    }

    private void Update()
    {
        if (devTools)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                resetCharacterStats();
                Debug.Log("Character Stats reset to Level 1");
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                addEXP(10);
                Debug.Log("Added 10 EXP");
            }
            if ((Input.GetKeyDown(KeyCode.Alpha2)))
            {
                addEXP(expToNext);
                Debug.Log("Forced Level Up");
            }
        }
    }
}
