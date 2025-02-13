using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spacebound/Enemies/New EnemyStatSheet")]

public class EnemyStatSheet : ScriptableObject
{
    public string enemyName = " ";
    public Sprite sprite = null;
    public Sprite attackSprite = null;
    public float bashMultiplier = 0;
    public float slashMultiplier = 0;
    public float pokeMultiplier = 0;
    public float health = 0;
    public float attackStrength = 0;
    public int baseExpValue = 0;
    public List<EnemyAttack> enemyAttacks = new List<EnemyAttack>();
}
