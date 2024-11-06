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
    public int randomNum;
    public int level;

    public AudioSource sfx;
    public AudioClip shipWalk, forestWalk;
    public SoundManager soundManager;
    public AudioClip shipBM, shipBMIntro, shipLM, forestBM, forestBMIntro, forestLM;
    private AudioClip battleMusic, battleMusicIntro, levelMusic;
    private AudioClip walkSound;

    public LayerMask unwalkableLayer;
    public GameObject fadetoBlackImage;
    public GameObject battleSystem, bossSystem;
    public GameObject dialogueSystem;
    private BossBattleUIScript bossScript;

    public Animator anim;
    public bool inBattle;
    private bool moving;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
        UpdateStepSpawn();
        battleSystem.gameObject.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
        walkSound = forestWalk; //When we have a change in scenery, we can rework changing the step sound. ~Dylan
        battleMusic = forestBM;
        battleMusicIntro = forestBMIntro;
        levelMusic = forestLM;
    }

    void Update()
    {

        //wasd input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
        Animate();
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        if (!IsTileUnwalkable(newPosition) && fadetoBlackImage.activeSelf == false && battleSystem.activeSelf == false && dialogueSystem.activeSelf == false)
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
            if(randomNum <= 0)
            {
                soundManager.BattleTransition(battleMusicIntro, battleMusic);
                inBattle = true;
                battleSystem.gameObject.SetActive(true);
                Debug.Log("Fight!");
                UpdateStepSpawn();
            }
        }
    }
    void UpdateStepSpawn()
    {
        randomNum = UnityEngine.Random.Range(64, 256);
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

    public void StartScriptedBattle(GameObject enemyPrefab)
    {
        soundManager.BattleTransition(battleMusicIntro, battleMusic);
        bossScript = bossSystem.GetComponent<BossBattleUIScript>();
        bossSystem.SetActive(true);
        inBattle = true;
    }

    public void EndBattle()
    {
        soundManager.ChangeMusic(levelMusic);
    }

    private void MusicChange(int lvl)
    {
        switch(lvl)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    public void LoadData(GameData data)
    {
        this.steps = data.steps;
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.steps = this.steps;
        data.playerPosition = this.transform.position;
    }
}
