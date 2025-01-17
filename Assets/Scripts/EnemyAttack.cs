using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spacebound/Enemies/NewEnemyAttack")]

public class EnemyAttack : ScriptableObject
{
    public string attackName = "";
    [Header("Adds to enemy Attack Strength.")]
    public int attackStrength = 0;
    [Header("Target Value must be between -1 and 4")]
    [Tooltip("0 for random. 1-4 for party slot. -1 for self.")]
    public List<int> targets = new List<int>();
}
