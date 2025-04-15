using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
//using Unity.VisualScripting.ReorderableList;

public class OmniDirectionalMovement : MonoBehaviour, IDataPersistence
{
    public bool tutorialBattle = false;
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
        forestBM, forestBMIntro, forestLM, forestCaveLM,
        glacierBM, glacierBMIntro, glacierLM;
    private AudioClip battleMusic, battleMusicIntro, levelMusic;
    private AudioClip walkSound;

    public AudioListener battleEars;
    public AudioListener overworldEars;


    public Image fadeImage;
    public float fadeDuration = 0.3f;

    public LayerMask unwalkableLayer;
    public GameObject fadetoBlackImage;
    public GameObject battleSystem, bossSystem, pauseMenu;
    public GameObject dialogueSystem;
    private DialogueController dialogueController;
    public BossStatSheet forestSE, caveSE;
    public BossBattleUIScript bossScript;
    public BossStatSheet specEncounter;

    public List<EnemyStatSheet> encounterPool = new List<EnemyStatSheet>();

    public List<EnemyStatSheet> tutorialPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> forestPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> fCavePool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> fgConnPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> glacierPool = new List<EnemyStatSheet>();
    public List<EnemyStatSheet> gCavePool = new List<EnemyStatSheet>();

    public Color shipBG, shipTxt, forestBG, forestTxt, fcaveBG, fcaveTxt, fgconnBG, fgconnTxt, glacierBG, glacierTxt, gCaveBG, gCaveTxt;
    public Image battleBackground, bossBackground;
    public TextMeshProUGUI battleHintText, bossHintText;

    public bool bobActive, stephvenActive, janetActive, thozosActive;

    public bool devTools;

    private DataPersistenceManager dpm;

    public DialogueText startingCutscene;
    private string id = "00000000-0000-0000-0000-000000000000";
    private bool startSceneWatched = false;

    public Animator anim;
    public bool inBattle;
    private bool moving;
    void Start()
    {
        StartCoroutine(fadeIn());
        dialogueController = dialogueSystem.GetComponent<DialogueController>();
        dpm = GameObject.Find("DataPersistenceManager").GetComponent<DataPersistenceManager>();
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
        UpdateStepSpawn();
        battleSystem.gameObject.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        BiomeChange(level);
        if (!startSceneWatched)
        {
            dialogueController.startCutscene(startingCutscene);
            startSceneWatched = true;
        }
    }

    public void OnAwake()
    {
    }

    public IEnumerator fadeIn()
    {
        Debug.Log("fade in");
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
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
            randomNum = randomNum - 3;
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
                bossScript.StartScriptedBattle(specEncounter, null);
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
        pauseMenu.SetActive(false);
        soundManager.ChangeMusic(levelMusic);
    }

    public void BiomeChange(int lvl)
    {
        AudioClip prevMusic = levelMusic;
        level = lvl;
        switch (lvl)
        {
            case 0: //Ship
                battleMusic = shipBM;
                battleMusicIntro = shipBMIntro;
                levelMusic = shipLM;
                walkSound = shipWalk;
                //Ship-Specific Stuff (Turning off encounters, etc.)
                break;
            case 1: //Forest
                battleMusic = forestBM;
                battleMusicIntro = forestBMIntro;
                levelMusic = forestLM;
                walkSound = forestWalk;
                specEncounter = forestSE;
                encounterPool = new List<EnemyStatSheet>();
                encounterPool = forestPool;
                battleBackground.color = forestBG;
                bossBackground.color = forestBG;
                battleHintText.color = forestTxt;
                bossHintText.color = forestTxt;
                break;
            case 2: //Forest Cave
                battleMusic = forestBM;
                battleMusicIntro = forestBMIntro;
                levelMusic = forestLM;
                walkSound = caveWalk;
                specEncounter = caveSE;
                encounterPool = new List<EnemyStatSheet>();
                encounterPool = fCavePool;
                battleBackground.color = fcaveBG;
                bossBackground.color = fcaveBG;
                battleHintText.color = fcaveTxt;
                bossHintText.color = fcaveTxt;
                break;
            case 3: //Forest Connector Cave
                battleMusic = forestBM;
                battleMusicIntro = forestBMIntro;
                levelMusic = forestCaveLM;
                walkSound = caveWalk;
                specEncounter = caveSE;
                encounterPool = new List<EnemyStatSheet>();
                encounterPool = fgConnPool;
                battleBackground.color = fgconnBG;
                bossBackground.color = fgconnBG;
                battleHintText.color = fgconnTxt;
                bossHintText.color = fgconnTxt;
                break;
            case 4: //Glacier
                battleMusic = glacierBM;
                battleMusicIntro = glacierBMIntro;
                levelMusic = glacierLM;
                walkSound = glacierWalk;
                specEncounter = caveSE;
                encounterPool = new List<EnemyStatSheet>();
                encounterPool = glacierPool;
                battleBackground.color = glacierBG;
                bossBackground.color = glacierBG;
                battleHintText.color = glacierTxt;
                bossHintText.color = glacierTxt;
                break;
            case 5: //Glacier Cave
                battleMusic = glacierBM;
                battleMusicIntro = glacierBMIntro;
                levelMusic = glacierLM;
                walkSound = glacierWalk;
                specEncounter = caveSE;
                encounterPool = new List<EnemyStatSheet>();
                encounterPool = gCavePool;
                battleBackground.color = gCaveBG;
                bossBackground.color = gCaveBG;
                battleHintText.color = gCaveTxt;
                bossHintText.color = gCaveTxt;
                break;
        }
        if (levelMusic != prevMusic)
        {
            soundManager.ChangeMusic(levelMusic);
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
        data.cutscenesWatched.TryGetValue(id, out startSceneWatched);
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
        if (data.cutscenesWatched.ContainsKey(id))
        {
            data.cutscenesWatched.Remove(id);
        }
        data.cutscenesWatched.Add(id, startSceneWatched);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Ensure the final alpha value is set
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}
