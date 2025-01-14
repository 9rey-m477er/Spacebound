using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/NewEnemyStatSheet")]

public class EnemyStatSheet : ScriptableObject
{
    public string enemyName = " ";
    public Sprite sprite = null;
    public AudioClip encounterIntro = null;
    public AudioClip encounterMusic = null;
    public float health = 0;
    public float attackStrength = 0;
    public float baseExpValue = 0;
    public List<EnemyAttack> enemyAttacks = new List<EnemyAttack>();
}
