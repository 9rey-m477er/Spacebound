using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int steps;
    public bool bobFlag, thozosFlag, janetFlag, stephvenFlag;
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> staticsCleared;

    public GameData()
    {
        //General Information: Simple
        steps = 0;
        bobFlag = false;
        thozosFlag = false;
        janetFlag = false;
        stephvenFlag = false;
        //General Information: Complex
        playerPosition = new Vector3(329.01f, -25.24f, 0f); //change to start position for full game (Starts in 1st forest room right now)
        staticsCleared = new SerializableDictionary<string, bool>();
        //Character Stats: John
        //Character Stats: Bob
        //Character Stats: Thozos
        //Character Stats: Janet
        //Character Stats: Stephven
    }
}
