using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerScript : MonoBehaviour
{
    public float health = 0;
    public float startingHealth = 0;
    public float slashMultiplier = 0;
    public float bashMultiplier = 0;
    public float pokeMultiplier = 0;
    public Sprite characterSprite = null;
    public Sprite attackSprite = null;
    public Sprite downedSprite = null;
    public Sprite hurtSprite = null;
    public string characterName = string.Empty;
}
