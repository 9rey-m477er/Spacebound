using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int steps;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> bossesDefeated;

    public GameData()
    {
        steps = 0;
        playerPosition = new Vector3(329.01f, -25.24f, 0f); //change to start position for full game (Starts in 1st forest room right now)
        bossesDefeated = new SerializableDictionary<string, bool>();
    }
}
