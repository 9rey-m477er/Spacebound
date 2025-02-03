using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour, IDataPersistence
{
    [SerializeField] private CharacterStatSheet John;
    [SerializeField] private CharacterStatSheet Bob;
    [SerializeField] private CharacterStatSheet Thozos;
    [SerializeField] private CharacterStatSheet Janet;
    [SerializeField] private CharacterStatSheet Stephven;
    public int partyEXP;
    public int expToNext;
    public List<CharacterStatSheet> battleOrder;

    public void LoadData(GameData data)
    {
        //Set John's stats
        John.characterLevel = data.partyLevel;
        John.characterHP = data.jHP;
        John.bashMultiplier = data.jBash;
        John.slashMultiplier = data.jSlash;
        John.pokeMultiplier = data.jPoke;
        //Set Bob's stats
        Bob.characterLevel = data.partyLevel;
        Bob.characterHP = data.bHP;
        Bob.bashMultiplier = data.bBash;
        Bob.slashMultiplier = data.bSlash;
        Bob.pokeMultiplier = data.bPoke;
        //Set Thozos' stats
        Thozos.characterLevel = data.partyLevel;
        Thozos.characterHP = data.tHP;
        Thozos.bashMultiplier = data.tBash;
        Thozos.slashMultiplier = data.tSlash;
        Thozos.pokeMultiplier = data.tPoke;
        //Set Janet's stats
        Janet.characterLevel = data.partyLevel;
        Janet.characterHP = data.nHP;
        Janet.bashMultiplier= data.nBash;
        Janet.slashMultiplier = data.nSlash;
        Janet.pokeMultiplier = data.nPoke;
        //Set Stephven's statts
        Stephven.characterLevel = data.partyLevel;
        Stephven.characterHP = data.sHP;
        Stephven.bashMultiplier = data.sBash;
        Stephven.slashMultiplier = data.sSlash;
        Stephven.pokeMultiplier = data.sPoke;
        //Set General Information
        expToNext = data.expToNext;

    }

    public void SaveData(ref GameData data)
    {
        //Save John's stats
        data.partyLevel = John.characterLevel;
        data.jHP = John.characterHP;
        data.jBash = John.bashMultiplier;
        data.jSlash = John.slashMultiplier;
        data.jPoke = John.pokeMultiplier;
        //Save Bob's stats
        data.partyLevel = Bob.characterLevel;
        data.bHP = Bob.characterHP;
        data.bBash = Bob.bashMultiplier;
        data.bSlash = Bob.slashMultiplier;
        data.bPoke = Bob.pokeMultiplier;
        //Save Thozos' stats
        data.partyLevel = Thozos.characterLevel;
        data.tHP = Thozos.characterHP;
        data.tBash = Thozos.bashMultiplier;
        data.tSlash = Thozos.slashMultiplier;
        data.tPoke = Thozos.pokeMultiplier;
        //Save Janet's stats
        data.partyLevel = Janet.characterLevel;
        data.nHP = Janet.characterHP;
        data.nBash = Janet.bashMultiplier;
        data.nSlash = Janet.slashMultiplier;
        data.nPoke = Janet.pokeMultiplier;
        //Save Stephven's statts
        data.partyLevel = Stephven.characterLevel;
        data.sHP = Stephven.characterHP;
        data.sBash = Stephven.bashMultiplier;
        data.sSlash = Stephven.slashMultiplier;
        data.sPoke = Stephven.pokeMultiplier;
        //Set General Information
        data.expToNext = expToNext;
    }

    public void partyLevelUp()
    {
        //level up system goes here.
        partyEXP = 0;
        expToNext = 0; //Replace with whatever formula we use.
    }
}
