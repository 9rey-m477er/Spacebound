using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spacebound/Enemies/New BossStatSheet")]

public class BossStatSheet : ScriptableObject
{
    public string enemyName = " ";
    public Sprite sprite = null;
    public AudioClip encounterIntro = null;
    public AudioClip encounterMusic = null;
    public float bashMultiplier = 0;
    public float slashMultiplier = 0;
    public float pokeMultiplier = 0;
    public float health = 0;
    public float attackStrength = 0;
    public float baseExpValue = 0;
    public bool canFlee = false;
    //public List<int> hpPhases = new List<int>();
    public List<EnemyAttack> enemyAttacks = new List<EnemyAttack>();
    //public List<EnemyStatSheet> minionSpawns = new List<EnemyStatSheet>();
}
