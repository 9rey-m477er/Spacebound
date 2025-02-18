using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spacebound/Characters/New CharacterStatSheet")]

public class CharacterStatSheet : ScriptableObject
{
    public string characterName = string.Empty;
    public Sprite characterSprite = null;
    public Sprite attackSprite = null;
    public Sprite downedSprite = null;
    public Sprite hurtSprite = null;
    public int characterLevel = 1;
    public float characterHP = 30;
    public float currentHP = 30;
    public float bashMultiplier = 1.0f;
    public float slashMultiplier = 1.0f;
    public float pokeMultiplier = 1.0f;
    //so slot for weapon
    //so slot for armour
}
