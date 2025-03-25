using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //General Information: Simple
    public int steps, membersMissing, partyLevel, partyExp, expToNext, area;
    public bool bobFlag, thozosFlag, janetFlag, stephvenFlag, openingScenePlayed;
    //General Information: Complex
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> staticsCleared;
    public SerializableDictionary<string, bool> cutscenesWatched;
    //Character Stats: John
    public float jHP, jCHP, jBash, jSlash, jPoke;
    //Character Stats: Bob
    public float bHP, bCHP, bBash, bSlash, bPoke;
    //Character Stats: Thozos
    public float tHP, tCHP, tBash, tSlash, tPoke;
    //Character Stats: Janet
    public float nHP, nCHP, nBash, nSlash, nPoke;
    //Character Stats: Stephven
    public float sHP, sCHP, sBash, sSlash, sPoke;

    public GameData()
    {
        //General Information: Simple
        steps = 0;
        area = 1;
        membersMissing = 4;
        partyLevel = 1;
        partyExp = 0;
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
        jCHP = 30;
        jBash = 1.0f;
        jSlash = 1.0f;
        jPoke = 1.0f;
        //Character Stats: Bob
        bHP = 40;
        bCHP = 40;
        bBash = 1.1f;
        bSlash = 1.0f;
        bPoke = 0.9f;
        //Character Stats: Thozos
        tHP = 20;
        tCHP = 20;
        tBash = 0.9f;
        tSlash = 0.9f;
        tPoke = 0.9f;
        //Character Stats: Janet
        nHP = 35;
        nCHP = 35;
        nBash = 1.0f;
        nSlash = 0.9f;
        nPoke = 1.1f;
        //Character Stats: Stephven
        sHP = 25;
        sCHP = 25;
        sBash = 0.9f;
        sSlash = 1.1f;
        sPoke = 1.0f;
    }
}
