using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //General Information: Simple
    public int steps, membersMissing, partyLevel, expToNext;
    public bool bobFlag, thozosFlag, janetFlag, stephvenFlag;
    //General Information: Complex
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> staticsCleared;
    //Character Stats: John
    public float jHP, jBash, jSlash, jPoke;
    //Character Stats: Bob
    public float bHP, bBash, bSlash, bPoke;
    //Character Stats: Thozos
    public float tHP, tBash, tSlash, tPoke;
    //Character Stats: Janet
    public float nHP, nBash, nSlash, nPoke;
    //Character Stats: Stephven
    public float sHP, sBash, sSlash, sPoke;

    public GameData()
    {
        //General Information: Simple
        steps = 0;
        membersMissing = 4;
        partyLevel = 1;
        expToNext = 100;
        bobFlag = false;
        thozosFlag = false;
        janetFlag = false;
        stephvenFlag = false;
        //General Information: Complex
        playerPosition = new Vector3(329.01f, -25.24f, 0f); //change to start position for full game (Starts in 1st forest room right now)
        staticsCleared = new SerializableDictionary<string, bool>();
        //Character Stats: John
        jHP = 30;
        jBash = 1.0f;
        jSlash = 1.0f;
        jPoke = 1.0f;
        //Character Stats: Bob
        bHP = 40;
        bBash = 1.1f;
        bSlash = 1.0f;
        bPoke = 0.9f;
        //Character Stats: Thozos
        tHP = 20;
        tBash = 0.9f;
        tSlash = 0.9f;
        tPoke = 0.9f;
        //Character Stats: Janet
        nHP = 35;
        nBash = 1.0f;
        nSlash = 0.9f;
        nPoke = 1.1f;
        //Character Stats: Stephven
        sHP = 25;
        sBash = 0.9f;
        sSlash = 1.1f;
        sPoke = 1.0f;
    }
}
