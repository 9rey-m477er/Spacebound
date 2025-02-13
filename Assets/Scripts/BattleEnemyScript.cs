using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyScript : MonoBehaviour
{
    //Assign shit in the editor
    //Can probably put this on enemy prefabs we can spawn in and assign the values inside the prefab
    //Will probably place enemy moves in here as well
    //move type resistances? the math for that would probably go here id imagine
    public float health = 0;
    public float startingHealth = 0;
    public float attackStrength = 0;
    public int baseExpValue = 0;
    public float bashMultiplier = 0;
    public float slashMultiplier = 0;
    public float pokeMultiplier = 0;
    public float defMultiplier = 1.0f;
    public float accuracy = 1.0f;
    public float evasiveness = 0f;
    public string enemyName = " ";
    public bool canFlee = true;
    public List<EnemyAttack> enemyAttacks = new List<EnemyAttack>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
