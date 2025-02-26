using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
//using Unity.VisualScripting.ReorderableList;

public class OmniDirectionalMovement : MonoBehaviour, IDataPersistence
{
    public float moveSpeed = 4f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private Vector2 lastPosition;
    public float stepThreshold = 2f;
    public int steps = 0;
    public int randomNum, specRoll;
    public int level;
    public int membersMissing = 4;

    public AudioSource sfx;
    public AudioClip shipWalk, forestWalk, caveWalk, glacierWalk;
    public SoundManager soundManager;
    public AudioClip shipBM, shipBMIntro, shipLM, 
        forestBM, forestBMIntro, forestLM,
        glacierBM, glacierBMIntro, glacierLM;
    private AudioClip battleMusic, battleMusicIntro, levelMusic;
    private AudioClip walkSound;

    public AudioListener battleEars;
    public AudioListener overworldEars;

    public LayerMask unwalkableLayer;
    public GameObject fadetoBlackImage;
    public GameObject battleSystem, bossSystem, pauseMenu;
    public GameObject dialogueSystem;
    public BossStatSheet forestSE, caveSE;
    public BossBattleUIScript bossScript;
    public BossStatSheet specEncounter;

    public List<EnemyStatSheet> encounterPool = new List<EnemyStatSheet>();

    public List<EnemyStatSheet> forestPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> fCavePool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> fgConnPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> glacierPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> gCavePool = new List<EnemyStatSheet>();

    public bool bobActive, stephvenActive, janetActive, thozosActive;

    public bool devTools;

    private DataPersistenceManager dpm;

    public Animator anim;
    public bool inBattle;
    private bool moving;
    void Start()
    {
        dpm = GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
        UpdateStepSpawn();
        battleSystem.gameObject.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        BiomeChange(level);
    }

    void Update()
    {

        //wasd input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
        Animate();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 6f;
        }
        else
        {
            moveSpeed = 4f;
        }
        if (devTools)
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                dpm.SaveGame();
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        if (!IsTileUnwalkable(newPosition) && fadetoBlackImage.activeSelf == false && battleSystem.activeSelf == false && dialogueSystem.activeSelf == false && bossSystem.activeSelf == false && pauseMenu.activeSelf == false)
        {
            rb.MovePosition(newPosition);
            CountSteps(newPosition);
        }
    }

    bool IsTileUnwalkable(Vector2 targetPosition)
    {
        return Physics2D.OverlapCircle(targetPosition, 0.1f, unwalkableLayer) != null;
    }

    void CountSteps(Vector2 currentPosition)
    {
        float distanceMoved = Vector2.Distance(currentPosition, lastPosition);
        if (distanceMoved >= stepThreshold)
        {
            steps++;
            if (steps%2 == 0)
            {
                sfx.PlayOneShot(walkSound);
            }
            lastPosition = currentPosition;
            randomNum = randomNum - 4;
            if(randomNum <= 0 && specRoll != 69)
            {
                battleEars.enabled = true;
                overworldEars.enabled = false;
                soundManager.BattleTransition(battleMusicIntro, battleMusic);
                inBattle = true;
                battleSystem.gameObject.SetActive(true);
                Debug.Log("Fight!");
                UpdateStepSpawn();
            }
            else if (randomNum <= 0 && specRoll == 69)
            {
                battleEars.enabled = true;
                overworldEars.enabled = false;
                inBattle = true;
                bossSystem.SetActive(true);
                bossScript.StartScriptedBattle(specEncounter);
                UpdateStepSpawn();
            }
        }
    }
    void UpdateStepSpawn()
    {
        randomNum = UnityEngine.Random.Range(64, 256);
        specRoll = UnityEngine.Random.Range(0, 100);
    }
    private void Animate()
    {
        if(movement.magnitude > .1f || movement.magnitude < -.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
        if (moving)
        {
            anim.SetFloat("X", movement.x);
            anim.SetFloat("Y", movement.y);
        }
        anim.SetBool("Moving", moving);
    }

    public void EndBattle()
    {
        battleEars.enabled = false;
        overworldEars.enabled = true;
        soundManager.ChangeMusic(levelMusic);
    }

    public void BiomeChange(int lvl)
    {
        switch (lvl)
        {
            case 0:
                battleMusic = shipBM;
                battleMusicIntro = shipBMIntro;
                levelMusic = shipLM;
                walkSound = shipWalk;
                //Ship-Specific Stuff (Turning off encounters, etc.)
                break;
            case 1:
                battleMusic = forestBM;
                battleMusicIntro = forestBMIntro;
                levelMusic = forestLM;
                walkSound = forestWalk;
                specEncounter = forestSE;
                encounterPool = forestPool;
                break;
            case 2:
                battleMusic = forestBM;
                battleMusicIntro = forestBMIntro;
                levelMusic = forestLM;
                walkSound = caveWalk;
                specEncounter = caveSE;
                encounterPool = fCavePool;
                break;
            case 3:
                battleMusic = forestBM;
                battleMusicIntro = forestBMIntro;
                levelMusic = forestLM;
                walkSound = caveWalk;
                specEncounter = caveSE;
                encounterPool = fgConnPool;
                break;
            case 4:
                battleMusic = glacierBM;
                battleMusicIntro = glacierBMIntro;
                levelMusic = glacierLM;
                walkSound = glacierWalk;
                specEncounter = caveSE;
                encounterPool = glacierPool;
                break;
            case 5:
                battleMusic = glacierBM;
                battleMusicIntro = glacierBMIntro;
                levelMusic = glacierLM;
                walkSound = glacierWalk;
                specEncounter = caveSE;
                encounterPool = gCavePool;
                break;

        }
    }

    public void setTeammateActive(int flag)
    {
        switch(flag)
        {
            case 0:
                bobActive = true;
                membersMissing -= 1;
                break;
            case 1:
                thozosActive = true;
                membersMissing -= 1;
                break;
            case 2:
                janetActive = true;
                membersMissing -= 1;
                break;
            case 3:
                stephvenActive = true;
                membersMissing -= 1;
                break;
            default:
                break;
        }
    }

    public void LoadData(GameData data)
    {
        this.steps = data.steps;
        this.membersMissing = data.membersMissing;
        this.transform.position = data.playerPosition;
        this.bobActive = data.bobFlag;
        this.thozosActive = data.thozosFlag;
        this.stephvenActive = data.stephvenFlag;
        this.janetActive = data.janetFlag;
        this.level = data.area;
        BiomeChange(level);
    }

    public void SaveData(ref GameData data)
    {
        data.steps = this.steps;
        data.membersMissing = this.membersMissing;
        data.playerPosition = this.transform.position;
        data.bobFlag = this.bobActive;
        data.thozosFlag = this.thozosActive;
        data.stephvenFlag = this.stephvenActive;
        data.janetFlag = this.janetActive;
        data.area = this.level;
    }
}
