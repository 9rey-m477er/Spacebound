using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;

public class BattleUIScript : MonoBehaviour
{
    public AudioListener battleEars;
    public AudioListener overworldEars;

    public GameObject battleSystem;
    public GameObject atkMenu;
    public GameObject defMenu;
    public GameObject invMenu;
    public GameObject runMenu;
    public int playerTurn;
    public GameObject party1Arrow;
    public GameObject party2Arrow;
    public GameObject party3Arrow;
    public GameObject party4Arrow;

    public GameObject party1Reticle;
    public GameObject party2Reticle;
    public GameObject party3Reticle;
    public GameObject party4Reticle;

    public GameObject enemy1Arrow;
    public GameObject enemy2Arrow;
    public GameObject enemy3Arrow;
    public GameObject enemy4Arrow;

    public GameObject attackArrow;
    public GameObject defendArrow;
    public GameObject invenArrow;
    public GameObject runArrow;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public BattleEnemyScript battleEnemyScript;
    public BattlePlayerScript battlePlayerScript;
    public DataPersistenceManager dataPersistenceManager;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public bool isSelectingEnemy = false;
    public int currentEnemy = 1;
    public int currentEnemy2 = 0;
    public char currentAttack = 'n';
    public int attackPower;
    public bool isBattleOver = false;

    public GameObject enemyHPBar1;
    public GameObject enemyHPBar2;
    public GameObject enemyHPBar3;
    public GameObject enemyHPBar4;
    public TextMeshProUGUI enemyName1;
    public TextMeshProUGUI enemyName2;
    public TextMeshProUGUI enemyName3;
    public TextMeshProUGUI enemyName4;

    public GameObject playerHPBar1;
    public GameObject playerHPBar2;
    public GameObject playerHPBar3;
    public GameObject playerHPBar4;

    public GameObject playerHPBar1Border;
    public GameObject playerHPBar2Border;
    public GameObject playerHPBar3Border;
    public GameObject playerHPBar4Border;

    public GameObject playerHPBar1inside;
    public GameObject playerHPBar2inside;
    public GameObject playerHPBar3inside;
    public GameObject playerHPBar4inside;


    public Image fadeImage;
    public float fadeDuration = 0.3f;

    public TextMeshProUGUI turnCounter;
    public int turnCounterIndex = 1;

    private OmniDirectionalMovement johnMovement;
    private SoundManager soundManager;
    public bool isinMenu = false;
    public Button menuBlocking;
    public int currentMenuArrow = 1;
    public int innerMenuArrow = 1;
    int menuArrowTemp = 0;

    public GameObject atkArrow1;
    public GameObject atkArrow2;
    public GameObject atkArrow3;

    public GameObject defArrow1;
    public GameObject defArrow2;
    public GameObject defArrow3;

    public GameObject invArrow1;
    public GameObject invArrow2;
    public GameObject invArrow3;

    public GameObject runArrow1;
    public GameObject runArrow2;

    public bool canSelect = false;
    public bool canSelect2 = false;

    public List<Sprite> forestSpritePool = new List<Sprite>();
    public List<EnemyStatSheet> forestEnemyPool = new List<EnemyStatSheet>();

    private List<string> battleLog = new List<string>();
    private string battleLogEntry = string.Empty;
    public TextMeshProUGUI battleLogLine1;
    public TextMeshProUGUI battleLogLine2;
    public TextMeshProUGUI battleLogLine3;
    public TextMeshProUGUI battleLogLine4;
    public GameObject battleLogObj;

    public CharacterStatSheet john, bob, thozos, janet, stephven;

    public TextMeshProUGUI turnName;
    void OnEnable()
    {
        resetMenu();
        currentMenuArrow = 1;
        updateMenuArrows();
        innerMenuArrow = 1;
        updateInnerArrow();
        isinMenu = true;
        battleEars.enabled = true;
        overworldEars.enabled = false;
        menuBlocking.gameObject.SetActive(false);
        turnCounter.text = "Turn 1";
        enemy1.gameObject.SetActive(true);
        enemy2.gameObject.SetActive(true);
        enemy3.gameObject.SetActive(true);
        enemy4.gameObject.SetActive(true);

        playerHPBar1.gameObject.SetActive(true);
        playerHPBar2.gameObject.SetActive(true);
        playerHPBar3.gameObject.SetActive(true);
        playerHPBar4.gameObject.SetActive(true);

        playerHPBar1Border.gameObject.SetActive(true);
        playerHPBar2Border.gameObject.SetActive(true);
        playerHPBar3Border.gameObject.SetActive(true);
        playerHPBar4Border.gameObject.SetActive(true);

        playerHPBar1inside.gameObject.SetActive(true);
        playerHPBar2inside.gameObject.SetActive(true);
        playerHPBar3inside.gameObject.SetActive(true);
        playerHPBar4inside.gameObject.SetActive(true);

        johnMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<OmniDirectionalMovement>();
        soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        StartCoroutine(fadeIntoBattle());
        playerTurn = 1; // initialize battle with party member 1 attacking first
        party1Arrow.gameObject.SetActive(true);
        party2Arrow.gameObject.SetActive(false);
        party3Arrow.gameObject.SetActive(false);
        party4Arrow.gameObject.SetActive(false);

        enemy1Arrow.gameObject.SetActive(false);
        enemy2Arrow.gameObject.SetActive(false);
        enemy3Arrow.gameObject.SetActive(false);
        enemy4Arrow.gameObject.SetActive(false);

        party1Reticle.SetActive(false);
        party2Reticle.SetActive(false);
        party3Reticle.SetActive(false);
        party4Reticle.SetActive(false);

        attackArrow.gameObject.SetActive(true);
        defendArrow.gameObject.SetActive(false);
        invenArrow.gameObject.SetActive(false);
        runArrow.gameObject.SetActive(false);

        atkArrow1.gameObject.SetActive(false);
        atkArrow2.gameObject.SetActive(false);
        atkArrow3.gameObject.SetActive(false);

        defArrow1.gameObject.SetActive(false);
        defArrow2.gameObject.SetActive(false);
        defArrow3.gameObject.SetActive(false);

        invArrow1.gameObject.SetActive(false);
        invArrow2.gameObject.SetActive(false);
        invArrow3.gameObject.SetActive(false);

        runArrow1.gameObject.SetActive(false);
        runArrow2.gameObject.SetActive(false);

        battleLogLine1.text = "";
        battleLogLine2.text = "";
        battleLogLine3.text = "";
        battleLogLine4.text = "";
        //battleLogObj.SetActive(true);

        //randomly assign forest enemies
        //need an if statement here for when different biomes are put in
        rollEnemyForest(enemy1, forestEnemyPool);
        rollEnemyForest(enemy2, forestEnemyPool);
        rollEnemyForest(enemy3, forestEnemyPool);
        rollEnemyForest(enemy4, forestEnemyPool);
        updateEnemyNames();
        playerTeamSpawn();
        isBattleOver = false;
    }

    public void playerTeamSpawn()
    {
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        p1.health = john.currentHP;
        p1.startingHealth = john.characterHP;
        p1.bashMultiplier = john.bashMultiplier;
        p1.slashMultiplier = john.slashMultiplier;
        p1.pokeMultiplier = john.pokeMultiplier;
        p1.characterName = john.characterName;

        if (johnMovement.bobActive == true)
        {
            p2.gameObject.SetActive(true);
            p2.startingHealth = bob.characterHP;
            p2.health = bob.currentHP;
            p2.bashMultiplier = bob.bashMultiplier;
            p2.slashMultiplier = bob.slashMultiplier;
            p2.pokeMultiplier = bob.pokeMultiplier;
            p2.characterName = bob.characterName;
            playerHPBar2.gameObject.SetActive(true);
            playerHPBar2Border.gameObject.SetActive(true);
            playerHPBar2inside.gameObject.SetActive(true);
        }
        else
        {
            p2.gameObject.SetActive(false);
            p2.health = 0;
            playerHPBar2.gameObject.SetActive(false);
            playerHPBar2Border.gameObject.SetActive(false);
            playerHPBar2inside.gameObject.SetActive(false);
        }
        //
        if (johnMovement.thozosActive == true)
        {
            p3.gameObject.SetActive(true);
            p3.health = thozos.currentHP;
            p3.startingHealth = thozos.characterHP;
            p3.bashMultiplier = thozos.bashMultiplier;
            p3.slashMultiplier = thozos.slashMultiplier;
            p3.pokeMultiplier = thozos.pokeMultiplier;
            p3.characterName = thozos.characterName;
            playerHPBar3.gameObject.SetActive(true);
            playerHPBar3Border.gameObject.SetActive(true);
            playerHPBar3inside.gameObject.SetActive(true);
        }
        else
        {
            p3.gameObject.SetActive(false);
            p3.health = 0;
            playerHPBar3.gameObject.SetActive(false);
            playerHPBar3Border.gameObject.SetActive(false);
            playerHPBar3inside.gameObject.SetActive(false);
        }
        //
        if (johnMovement.janetActive == true)
        {
            p4.gameObject.SetActive(true);
            p4.health = janet.currentHP;
            p4.startingHealth = janet.characterHP;
            p4.bashMultiplier = janet.bashMultiplier;
            p4.slashMultiplier = janet.slashMultiplier;
            p4.pokeMultiplier = janet.pokeMultiplier;
            p4.characterName = janet.characterName;
            playerHPBar4.gameObject.SetActive(true);
            playerHPBar4Border.gameObject.SetActive(true);
            playerHPBar4inside.gameObject.SetActive(true);
        }
        else
        {
            p4.gameObject.SetActive(false);
            p4.health = 0;
            playerHPBar4.gameObject.SetActive(false);
            playerHPBar4Border.gameObject.SetActive(false);
            playerHPBar4inside.gameObject.SetActive(false);
        }
        updatePlayerHealth();
    }

    void updateEnemyNames()
    {
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        enemyName1.text = e1.enemyName;
        enemyName2.text = e2.enemyName;
        enemyName3.text = e3.enemyName;
        enemyName4.text = e4.enemyName;
    }

    void rollEnemyForest(GameObject enemy, List<EnemyStatSheet> sheetPool)
    {
        // Get a random sprite from the pool
        EnemyStatSheet sheet = sheetPool[Random.Range(0, sheetPool.Count)];

        // Get the Image component from the enemy GameObject
        Image targetImage = enemy.GetComponent<Image>();
        BattleEnemyScript targetScript = enemy.GetComponent<BattleEnemyScript>();
        targetImage.sprite = sheet.sprite;

        // Resize the GameObject based on the sprite dimensions
        RectTransform rectTransform = enemy.GetComponent<RectTransform>();
        if (rectTransform != null && sheet.sprite != null)
        {
            rectTransform.sizeDelta = new Vector2(sheet.sprite.rect.width, sheet.sprite.rect.height);
            //Debug.Log($"Resized {enemy.name} to: {randomSprite.rect.width}x{randomSprite.rect.height}");
        }
        else
        {
            //Debug.LogWarning($"RectTransform or Sprite missing for {enemy.name}");
        }

        //Set the rest of the values
        targetScript.health = sheet.health;
        targetScript.startingHealth = sheet.health;
        targetScript.attackStrength = sheet.attackStrength - johnMovement.membersMissing;
        targetScript.baseExpValue = sheet.baseExpValue;
        targetScript.enemyName = sheet.enemyName;
        targetScript.enemyAttacks = sheet.enemyAttacks;
    }

    void Update()
    {
        // Only check input if the player is currently selecting an enemy
        if (isSelectingEnemy)
        {
            StartCoroutine(HandleEnemySelection());
        }
        else if (isinMenu && isSelectingEnemy == false && menuBlocking.gameObject.active == false)
        {
            menuArrowNav();
        }
        if (isBattleOver == true)
        {
            StartCoroutine(exitBattle());
        }
        checkForEndOfBattle();
        updateTurnText();
    }

    void updateTurnText()
    {
        if(menuBlocking.gameObject.active == true)
        {
            turnName.text = "Enemy Turn!";
            turnName.color = Color.red;
        }
        else
        {
            turnName.text = "Party Turn!";
            turnName.color = Color.cyan;
        }
    }

    public void updateMenuArrows()
    {
        if(currentMenuArrow == 1)
        {
            attackArrow.gameObject.SetActive(true);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(false);
        }
        else if(currentMenuArrow == 2)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(true);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(false);
        }
        else if (currentMenuArrow == 3)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(true);
            runArrow.gameObject.SetActive(false);
        }
        else if (currentMenuArrow == 4)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(true);
        }
    }
    public void menuArrowNav()
    {
        if(atkMenu.active == false && defMenu.active == false && invMenu.active == false && runMenu.active == false && isSelectingEnemy == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (currentMenuArrow == 1)
                {
                    currentMenuArrow = 4;
                    updateMenuArrows();
                }
                else if (currentMenuArrow == 2)
                {
                    currentMenuArrow = 1;
                    updateMenuArrows();
                }
                else if (currentMenuArrow == 3)
                {
                    currentMenuArrow = 2;
                    updateMenuArrows();
                }
                else if (currentMenuArrow == 4)
                {
                    currentMenuArrow = 3;
                    updateMenuArrows();
                }
                menuArrowTemp = currentMenuArrow;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (currentMenuArrow == 1)
                {
                    currentMenuArrow = 2;
                    updateMenuArrows();
                }
                else if (currentMenuArrow == 2)
                {
                    currentMenuArrow = 3;
                    updateMenuArrows();
                }
                else if (currentMenuArrow == 3)
                {
                    currentMenuArrow = 4;
                    updateMenuArrows();
                }
                else if (currentMenuArrow == 4)
                {
                    currentMenuArrow = 1;
                    updateMenuArrows();
                }
                menuArrowTemp = currentMenuArrow;
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) && canSelect2 == false && isSelectingEnemy == false)
            {
                canSelect2 = true;
                if (currentMenuArrow == 1)
                {
                    openMenu(0);
                    StartCoroutine(SelectionWaitCoroutine(0.2f));
                    //currentMenuArrow = 0;
                }
                else if (currentMenuArrow == 2)
                {
                    openMenu(1);
                    StartCoroutine(SelectionWaitCoroutine(0.2f));
                }
                else if (currentMenuArrow == 3)
                {
                    openMenu(2);
                    StartCoroutine(SelectionWaitCoroutine(0.2f));
                }
                else if (currentMenuArrow == 4)
                {
                    openMenu(3);
                    StartCoroutine(SelectionWaitCoroutine(0.2f));
                }
                innerMenuArrow = 1;
                updateInnerArrow();
                canSelect = false;
            }
        }
        
        if(atkMenu.active == true || defMenu.active == true || invMenu.active == true || runMenu.active == true && isSelectingEnemy == false)
        {
            if(innerMenuArrow == 1)
            {
                if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    innerMenuArrow = 2;
                    updateInnerArrow();
                    //Debug.Log("1 > 2");
                }

                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {                    
                    if (runMenu.activeInHierarchy == true)
                    {
                        //Debug.Log("run is active going from 2 to 1");
                        innerMenuArrow = 2;
                        updateInnerArrow();
                    }
                    else
                    {
                        //Debug.Log("1 > 3");
                        innerMenuArrow = 3;
                        updateInnerArrow();
                    }
                }
            }
            else if (innerMenuArrow == 2)
            {
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    if(runMenu.activeInHierarchy == true)
                    {
                        //Debug.Log("run is active going from 2 to 1");
                        innerMenuArrow = 1;
                        updateInnerArrow();
                    }
                    else
                    {
                        //Debug.Log("2 > 3");
                        innerMenuArrow = 3;
                        updateInnerArrow();
                    }
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    //Debug.Log("2 > 1");
                    innerMenuArrow = 1;
                    updateInnerArrow();
                }
            }
            else if (innerMenuArrow == 3)
            {
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                {
                    //Debug.Log("3 > 1");
                    innerMenuArrow = 1;
                    updateInnerArrow();
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    //Debug.Log("3 > 2");
                    innerMenuArrow = 2;
                    updateInnerArrow();
                }
            }


            ////////////
            ///
            if(atkArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                attackSlot1();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            if (atkArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                attackSlot2();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            if (atkArrow3.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                attackSlot3();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            if (invArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                invSlot1();
                Debug.Log("islot1");
            }
            if (invArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                invSlot2();
                Debug.Log("islot2");
            }
            if (runArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                fleeButtonClick();
            }
            if (runArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                resetMenu();
            }
        }

        if ((atkMenu.active == true || defMenu.active == true || invMenu.active == true || runMenu.active == true) && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)))
        {
            menuArrowTemp = currentMenuArrow;
            innerMenuArrow = 1;
            updateInnerArrow();
            resetMenu();
        }
    }

    public IEnumerator SelectionWaitCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        //Debug.Log("waiting");
        canSelect = true;
    }
    public IEnumerator postSelectionWaitCoroutine(float duration) //broken idk
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("waiting");
        //canSelect2 = false;
        currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        resetMenu();
    }

    public void updateInnerArrow()
    {
        if(atkMenu.active == true)
        {
            if(innerMenuArrow == 1)
            {
                atkArrow1.gameObject.SetActive(true);
                atkArrow2.gameObject.SetActive(false);
                atkArrow3.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 2)
            {
                atkArrow1.gameObject.SetActive(false);
                atkArrow2.gameObject.SetActive(true);
                atkArrow3.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 3)
            {
                atkArrow1.gameObject.SetActive(false);
                atkArrow2.gameObject.SetActive(false);
                atkArrow3.gameObject.SetActive(true);
            }
        }
        else if(defMenu.active == true)
        {
            if (innerMenuArrow == 1)
            {
                defArrow1.gameObject.SetActive(true);
                defArrow2.gameObject.SetActive(false);
                defArrow3.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 2)
            {
                defArrow1.gameObject.SetActive(false);
                defArrow2.gameObject.SetActive(true);
                defArrow3.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 3)
            {
                defArrow1.gameObject.SetActive(false);
                defArrow2.gameObject.SetActive(false);
                defArrow3.gameObject.SetActive(true);
            }
        }
        else if (invMenu.active == true)
        {
            if (innerMenuArrow == 1)
            {
                invArrow1.gameObject.SetActive(true);
                invArrow2.gameObject.SetActive(false);
                invArrow3.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 2)
            {
                invArrow1.gameObject.SetActive(false);
                invArrow2.gameObject.SetActive(true);
                invArrow3.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 3)
            {
                invArrow1.gameObject.SetActive(false);
                invArrow2.gameObject.SetActive(false);
                invArrow3.gameObject.SetActive(true);
            }
        }
        else if (runMenu.active == true)
        {
            if (innerMenuArrow == 1)
            {
                runArrow1.gameObject.SetActive(true);
                runArrow2.gameObject.SetActive(false);
            }
            else if (innerMenuArrow == 2)
            {
                runArrow1.gameObject.SetActive(false);
                runArrow2.gameObject.SetActive(true);
            }
        }
    }

    public IEnumerator fadeIntoBattle()
    {
        //Debug.Log("fade in");
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
        turnCounterIndex = 1;
        //Debug.Log("fade out");
    }

    public void incrementTurn()
    {
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();
        if (playerTurn == 1)
        {
            if (p2.health > 0 && p2.isActiveAndEnabled == true)
            {
                playerTurn = 2;
            }
            else if (p3.health > 0 && p3.isActiveAndEnabled == true)
            {
                playerTurn = 3;
            }
            else if (p4.health > 0 && p4.isActiveAndEnabled == true)
            {
                playerTurn = 4;
            }
            else
            {
                playerTurn = 5;
            }
        }
        else if (playerTurn == 2)
        {
            if (p3.health > 0 && p3.isActiveAndEnabled == true)
            {
                playerTurn = 3;
            }
            else if (p4.health > 0 && p4.isActiveAndEnabled == true)
            {
                playerTurn = 4;
            }
            else
            {
                playerTurn = 5;
            }
        }
        else if (playerTurn == 3)
        {
            if (p4.health > 0 && p4.isActiveAndEnabled == true)
            {
                playerTurn = 4;
            }
            else
            {
                playerTurn = 5;
            }
        }
        else if (playerTurn == 4)
        {
            playerTurn = 5;
        }
        if (playerTurn == 5) // END OF PLAYER TURNS
        {
            menuBlocking.gameObject.SetActive(true);
            StartCoroutine(EnemyAttackSequence()); // TURN THIS OFF TO DISABLE ENEMY ATTACKS
            playerTurn = 1;
            turnCounterIndex++;
        }
        updateTurns();
        UpdatePartyArrow();
    }
    /*
    public void enemyAttack()
    {
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        int attackPower = 10;
        List<BattlePlayerScript> validTargets = new List<BattlePlayerScript>();

        // Add valid targets (players with health > 0)
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        if (p1.health > 0) validTargets.Add(p1);
        if (p2.health > 0) validTargets.Add(p2);
        if (p3.health > 0) validTargets.Add(p3);
        if (p4.health > 0) validTargets.Add(p4);

        // If no valid targets exist, exit the attack method
        if (validTargets.Count == 0) return;

        // Randomly select a target
        BattlePlayerScript target = validTargets[Random.Range(0, validTargets.Count)];

        // Apply damage to the target
        target.health -= attackPower;
        Debug.Log($"Enemy attacked {target.name} for {attackPower} damage.");

        // Update player health UI
        updatePlayerHealth();
        checkForEndOfBattle();
    }
    */

    private IEnumerator EnemyAttackSequence()
    {
        yield return new WaitForSeconds(0.5f);

        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        // List of all players
        List<BattlePlayerScript> players = new List<BattlePlayerScript>
    {
        player1.GetComponent<BattlePlayerScript>(),
        player2.GetComponent<BattlePlayerScript>(),
        player3.GetComponent<BattlePlayerScript>(),
        player4.GetComponent<BattlePlayerScript>()
    };

        // List of all enemies with their corresponding scripts and arrows
        List<(BattleEnemyScript enemyScript, GameObject arrow)> activeEnemies = new List<(BattleEnemyScript, GameObject)>
    {
        (e1, enemy1Arrow),
        (e2, enemy2Arrow),
        (e3, enemy3Arrow),
        (e4, enemy4Arrow)
    };

        // Iterate through each active enemy
        foreach (var (enemyScript, arrow) in activeEnemies)
        {
            if (!enemyScript.gameObject.activeSelf) continue; // Skip if the enemy is not active

            // Find valid targets (players with health > 0)
            List<BattlePlayerScript> validTargets = players.FindAll(player => player.health > 0);

            // If no valid targets exist, stop attacking
            if (validTargets.Count == 0) yield break;

            //If there aren't any attacks in the enemy's attack list
            if (enemyScript.enemyAttacks == null || enemyScript.enemyAttacks.Count == 0)
            {
                // Randomly select a target
                BattlePlayerScript target = validTargets[Random.Range(0, validTargets.Count)];

                // Show the arrow for the current attacking enemy
                arrow.SetActive(true);

                // Show the reticle for the selected target
                if (target == players[0]) party1Reticle.SetActive(true);
                else if (target == players[1]) party2Reticle.SetActive(true);
                else if (target == players[2]) party3Reticle.SetActive(true);
                else if (target == players[3]) party4Reticle.SetActive(true);

                yield return new WaitForSeconds(0.75f);

                // Play attack sound and deal damage using the enemy's attackStrength
                soundManager.PlaySoundClip(6);
                target.health -= enemyScript.attackStrength;

                //Write the Attack to the Battle Log
                Debug.Log($"{target.characterName} was attacked by {enemyScript.enemyName} ({enemyScript.attackStrength} damage)!");
                battleLogEntry = $"{target.characterName} was attacked by {enemyScript.enemyName} ({enemyScript.attackStrength} damage)!";
            }
            else
            {
                //Otherwise, use a listed attack
                EnemyAttack chosenAttack = enemyScript.enemyAttacks[Random.Range(0, enemyScript.enemyAttacks.Count)];

                BattlePlayerScript target = null;

                foreach (int t in chosenAttack.targets)
                {
                    string attackReadout = "";
                    if (chosenAttack.attackReadout.Count == 0)
                    {
                        attackReadout = "was attacked by";
                    }
                    else
                    {
                        attackReadout = chosenAttack.attackReadout[Random.Range(0, chosenAttack.attackReadout.Count)];
                    }
                    switch (t)
                    {
                        case 0:
                            target = validTargets[Random.Range(0, validTargets.Count)];
                            break;
                        case 1:
                            target = players[0];
                            break;
                        case 2:
                            target = players[1];
                            break;
                        case 3:
                            target = players[2];
                            break;
                        case 4:
                            target = players[3];
                            break;
                        case -1:
                            target = null;
                            break;
                        default:
                            target = validTargets[Random.Range(0, validTargets.Count)];
                            break;
                    }

                    if (target != null)
                    {
                        if (target.health > 0)
                        {
                            // Show the reticle for the selected target
                            if (target == players[0]) party1Reticle.SetActive(true);
                            else if (target == players[1]) party2Reticle.SetActive(true);
                            else if (target == players[2]) party3Reticle.SetActive(true);
                            else if (target == players[3]) party4Reticle.SetActive(true);

                            yield return new WaitForSeconds(0.75f);

                            // Play attack sound and deal damage using the enemy's attackStrength and the attack's attackStrength
                            soundManager.PlaySoundClip(6);
                            target.health -= enemyScript.attackStrength + chosenAttack.attackStrength;

                            //Write the Attack to the Battle Log
                            Debug.Log($"{target.characterName} {attackReadout} {enemyScript.enemyName} ({enemyScript.attackStrength} damage)!");
                            battleLogEntry = $"{target.characterName} {attackReadout} {enemyScript.enemyName} ({enemyScript.attackStrength} damage)!";
                            updateBattleLog(battleLogEntry);
                        }
                    }
                    else
                    {
                        //Need you to put a reticule thing here for the enemies to attack themselves (for recoil and shit).
                        enemyScript.health -= enemyScript.attackStrength + chosenAttack.attackStrength;
                    }

                    updatePlayerHealth();
                    party1Reticle.SetActive(false);
                    party2Reticle.SetActive(false);
                    party3Reticle.SetActive(false);
                    party4Reticle.SetActive(false);

                    yield return new WaitForSeconds(0.25f);
                }
            }

            // Update turn and UI
            updateTurns();
            updatePlayerHealth();

            // Hide the reticles and arrow after the attack
            party1Reticle.SetActive(false);
            party2Reticle.SetActive(false);
            party3Reticle.SetActive(false);
            party4Reticle.SetActive(false);
            arrow.SetActive(false);

            // Wait a bit before the next enemy attack
            yield return new WaitForSeconds(0.5f);
        }

        // End of enemy attack sequence
        turnCounterIndex++;
        menuBlocking.gameObject.SetActive(false);
        isinMenu = true;
        resetMenu();

    }

    private void updateBattleLog(string logUpdate)
    {

        //If the battle log already has 4 entries
        if (battleLog.Count == 4)
        {
            //Remove the least recent entry of the battle log
            battleLog.RemoveAt(3);
        }

        //Insert the new entry (logUpdate) into the front of the battle log
        battleLog.Insert(0, logUpdate);

        Debug.Log(battleLog.ToString());

        //Write as many lines as needed based on the length of the battle log (1-4, bottom-top)
        switch (battleLog.Count)
        {
            case 1:
                battleLogLine1.text = battleLog[0];
                break;
            case 2:
                battleLogLine1.text = battleLog[0];
                battleLogLine2.text = battleLog[1];
                break;
            case 3:
                battleLogLine1.text = battleLog[0];
                battleLogLine2.text = battleLog[1];
                battleLogLine3.text = battleLog[2];
                break;
            case 4:
                battleLogLine1.text = battleLog[0];
                battleLogLine2.text = battleLog[1];
                battleLogLine3.text = battleLog[2];
                battleLogLine4.text = battleLog[3];
                break;
        }
    }


    private void UpdatePartyArrow()
    {
        party1Arrow.gameObject.SetActive(playerTurn == 1);
        party2Arrow.gameObject.SetActive(playerTurn == 2);
        party3Arrow.gameObject.SetActive(playerTurn == 3);
        party4Arrow.gameObject.SetActive(playerTurn == 4);
    }
    public void attackSlot1() //bash    XO
    {                         //        OO
        currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        currentAttack = 'b';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 25;
        
        if(enemy1.active == true)
        {
            currentEnemy = 1;
        }
        else if (enemy2.active == true)
        {
            currentEnemy = 2;
        }
        else if (enemy3.active == true)
        {
            currentEnemy = 3;
        }
        else if (enemy4.active == true)
        {
            currentEnemy = 4;
        }
        UpdateEnemyArrows();
    }

    public void attackSlot2() //slash   //   XO
    {                                   //   XO
        currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        currentAttack = 's';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 15;

        if (enemy1.active == true)
        {
            currentEnemy = 1;
        }
        else if (enemy2.active == true)
        {
            currentEnemy = 2;
        }
        else if (enemy3.active == true)
        {
            currentEnemy = 3;
        }
        else if (enemy4.active == true)
        {
            currentEnemy = 4;
        }
        UpdateEnemyArrows();
    }

    public void attackSlot3() //poke    // XX
    {                                   // OO
        currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        currentAttack = 'p';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 15;

        if (enemy1.active == true)
        {
            currentEnemy = 1;
        }
        else if (enemy2.active == true)
        {
            currentEnemy = 2;
        }
        else if (enemy3.active == true)
        {
            currentEnemy = 3;
        }
        else if (enemy4.active == true)
        {
            currentEnemy = 4;
        }
        UpdateEnemyArrows();
    }

    public void invSlot1()
    {
        //currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();
        if (playerTurn == 1)
        {
            if (p1.health < p1.startingHealth)
            {
                p1.health += 10;
                if(p1.health > p1.startingHealth)
                {
                    p1.health = p1.startingHealth;
                }
            }
        }
        else if(playerTurn == 2)
        {
            if (p2.health < p2.startingHealth)
            {
                p2.health += 10;
                if (p2.health > p2.startingHealth)
                {
                    p2.health = p2.startingHealth;
                }
            }
        }
        else if (playerTurn == 3)
        {
            if (p3.health < p3.startingHealth)
            {
                p3.health += 10;
                if (p3.health > p3.startingHealth)
                {
                    p3.health = p3.startingHealth;
                }
            }
        }
        else if (playerTurn == 4)
        {
            if (p4.health < p4.startingHealth)
            {
                p4.health += 10;
                if (p4.health > p4.startingHealth)
                {
                    p4.health = p4.startingHealth;
                }
            }
        }
        incrementTurn();
        updatePlayerHealth();
    }

    public void invSlot2()
    {
        //currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        currentAttack = 'r';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 10;

        if (enemy1.active == true)
        {
            currentEnemy = 1;
        }
        else if (enemy2.active == true)
        {
            currentEnemy = 2;
        }
        else if (enemy3.active == true)
        {
            currentEnemy = 3;
        }
        else if (enemy4.active == true)
        {
            currentEnemy = 4;
        }
        UpdateEnemyArrows();
    }
    private IEnumerator HandleEnemySelection()
    {
        BattleEnemyScript enemy1HealthCheck = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript enemy2HealthCheck = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript enemy3HealthCheck = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript enemy4HealthCheck = enemy4.GetComponent<BattleEnemyScript>();
        UpdateEnemyArrows();
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 1 && enemy2HealthCheck.health > 0) currentEnemy = 2;
            else if (currentEnemy == 1 && enemy2HealthCheck.health <= 0) currentEnemy = 4;
            else if (currentEnemy == 3 && enemy4HealthCheck.health > 0) currentEnemy = 4;
            else if (currentEnemy == 3 && enemy4HealthCheck.health <= 0) currentEnemy = 2;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 2 && enemy1HealthCheck.health > 0) currentEnemy = 1;
            else if (currentEnemy == 2 && enemy1HealthCheck.health <= 0) currentEnemy = 3;
            else if (currentEnemy == 4 && enemy3HealthCheck.health > 0) currentEnemy = 3;
            else if (currentEnemy == 4 && enemy3HealthCheck.health <= 0) currentEnemy = 1;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 3 && enemy1HealthCheck.health > 0) currentEnemy = 1;
            else if (currentEnemy == 3 && enemy1HealthCheck.health <= 0) currentEnemy = 2;
            else if (currentEnemy == 4 && enemy2HealthCheck.health > 0) currentEnemy = 2;
            else if (currentEnemy == 4 && enemy2HealthCheck.health <= 0) currentEnemy = 1;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 1 && enemy3HealthCheck.health > 0) currentEnemy = 3;
            else if (currentEnemy == 1 && enemy3HealthCheck.health <= 0) currentEnemy = 4;
            else if (currentEnemy == 2 && enemy4HealthCheck.health > 0) currentEnemy = 4;
            else if (currentEnemy == 2 && enemy4HealthCheck.health <= 0) currentEnemy = 3;
            UpdateEnemyArrows();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))//confirm
        {
            isSelectingEnemy = false; 
            ExecuteAttack();

            updateEnemyHealth();
            enemy1Arrow.gameObject.SetActive(false);
            enemy2Arrow.gameObject.SetActive(false);
            enemy3Arrow.gameObject.SetActive(false);
            enemy4Arrow.gameObject.SetActive(false);

            yield return new WaitForSeconds(.5f);////////////////////////////// ANIMATION IS ALLOWED TO SIMMER HERE

            updatePlayerHealth(); //player sprites get reset back to base form here
            incrementTurn();
            resetMenu();
            //checkForEndOfBattle();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            menuArrowTemp = currentMenuArrow;
            Debug.Log("back out");
            enemy1Arrow.gameObject.SetActive(false);
            enemy2Arrow.gameObject.SetActive(false);
            enemy3Arrow.gameObject.SetActive(false);
            enemy4Arrow.gameObject.SetActive(false);
            resetMenu();
            isSelectingEnemy = false;
        }
    }



    public void ExecuteAttack()
    {
        BattlePlayerScript attacker = null;
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();


        BattleEnemyScript selectedEnemyScript = null;
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        int currentAttacker = playerTurn;
        int selectedEnemy = 0;
        string attackerName = string.Empty;
        string attackDesc = string.Empty;
        string enemyName = string.Empty;
        int readoutDamage = 0;
        switch (currentEnemy)
        {
            case 1:
                selectedEnemyScript = enemy1.GetComponent<BattleEnemyScript>();
                selectedEnemy = 1;
                break;
            case 2:
                selectedEnemyScript = enemy2.GetComponent<BattleEnemyScript>();
                selectedEnemy = 2;
                break;
            case 3:
                selectedEnemyScript = enemy3.GetComponent<BattleEnemyScript>();
                selectedEnemy = 3;
                break;
            case 4:
                selectedEnemyScript = enemy4.GetComponent<BattleEnemyScript>();
                selectedEnemy = 4;
                break;
        }
        ////////////////////////////////// -- ATTACK ANIMATIONS THIS CHANGES CURRENT IMAGE TO THE IMAGE ATTACHED TO IMAGE COMPONENT OF FRONTLINE/BACKLINE GAMEOBJECT
        if(currentAttacker == 1)
        {
            //Image playerImage = player1.GetComponent<Image>();
            //playerImage.sprite = p1.attackSprite;
            attacker = p1;
        }
        else if(currentAttacker == 2)
        {
            Image playerImage = player2.GetComponent<Image>();
            playerImage.sprite = p2.attackSprite;
            attacker = p2;
        }
        else if(currentAttacker == 3)
        {
            //Image playerImage = player3.GetComponent<Image>();
            //playerImage.sprite = p3.attackSprite;
            attacker = p3;
        }
        else if (currentAttacker == 4)
        {
            //Image playerImage = player4.GetComponent<Image>();
            //playerImage.sprite = p4.attackSprite;
            attacker = p4;
        }
        attackerName = attacker.characterName;
        ///////////////////////////////////
        if (selectedEnemyScript != null)
        {
            if(currentAttack == 'b') //bash
            {
                readoutDamage = (int)(attackPower * attacker.bashMultiplier);
                selectedEnemyScript.health -= (int)(attackPower * attacker.bashMultiplier);
                attackDesc = "bashed";
                enemyName = selectedEnemyScript.enemyName;
            }
            else if(currentAttack == 's') //slash
            {
                attackDesc = "slashed";
                readoutDamage = (int)(attackPower * attacker.slashMultiplier);
                if (selectedEnemy == 1 || selectedEnemy == 2)
                {
                    e1.health -= (int)(attackPower * attacker.slashMultiplier);
                    e2.health -= (int)(attackPower * attacker.slashMultiplier);
                    enemyName = ($"{e1.enemyName} and {e2.enemyName}");
                }
                else if (selectedEnemy == 3 || selectedEnemy == 4)
                {
                    e3.health -= (int)(attackPower * attacker.slashMultiplier);
                    e4.health -= (int)(attackPower * attacker.slashMultiplier);
                    enemyName = ($"{e3.enemyName} and {e4.enemyName}");
                }
            }
            else if(currentAttack == 'p') //poke
            {
                attackDesc = "poked";
                readoutDamage = (int)(attackPower * attacker.pokeMultiplier);
                if (selectedEnemy == 1 || selectedEnemy == 3)
                {
                    e1.health -= (int)(attackPower * attacker.pokeMultiplier);
                    e3.health -= (int)(attackPower * attacker.pokeMultiplier);
                    enemyName = ($"{e1.enemyName} and {e3.enemyName}");
                }
                else if (selectedEnemy == 2 || selectedEnemy == 4)
                {
                    e2.health -= (int)(attackPower * attacker.pokeMultiplier);
                    e4.health -= (int)(attackPower * attacker.pokeMultiplier);
                    enemyName = ($"{e2.enemyName} and {e4.enemyName}");
                }
            }
            else if(currentAttack == 'r') //rock
            {
                readoutDamage = attackPower;
                enemyName = selectedEnemyScript.enemyName;
                selectedEnemyScript.health -= attackPower;
                attackDesc = "threw a rock at";
            }
            Debug.LogError(readoutDamage);
            currentAttack = 'n';
            soundManager.PlaySoundClip(6);
            updateBattleLog($"{attackerName} {attackDesc} {enemyName}! ({readoutDamage} HP)");
            //Debug.Log("Attacked enemy " + currentEnemy + ", remaining health: " + selectedEnemyScript.health); //BATTLE NARRATION
        }
    }

    public void updateEnemyHealth()
    {
        BattleEnemyScript currentEnemy = null;

        currentEnemy = enemy1.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP1 = enemyHPBar1.GetComponent<RectTransform>();
        if(currentEnemy.health <= 0)
        {
            enemy1.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        enemyHP1.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP1.sizeDelta.y);

        currentEnemy = enemy2.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP2 = enemyHPBar2.GetComponent<RectTransform>();
        if (currentEnemy.health <= 0)
        {
            enemy2.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        enemyHP2.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP2.sizeDelta.y);
        
        currentEnemy = enemy3.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP3 = enemyHPBar3.GetComponent<RectTransform>();
        if (currentEnemy.health <= 0)
        {
            enemy3.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        enemyHP3.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP3.sizeDelta.y);

        currentEnemy = enemy4.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP4 = enemyHPBar4.GetComponent<RectTransform>();
        if (currentEnemy.health <= 0)
        {
            enemy4.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        enemyHP4.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP4.sizeDelta.y);

    }

    public void updatePlayerHealth()
    {
        BattlePlayerScript currentPlayer = null;

        currentPlayer = player1.GetComponent<BattlePlayerScript>();
        RectTransform playerHP1 = playerHPBar1.GetComponent<RectTransform>();
        Image p1 = player1.GetComponent<Image>();
        string name1 = p1.sprite.name;
        if (currentPlayer.health <= 0)
        {
            p1.sprite = Resources.Load<Sprite>("Dead " + name1);
        }
        else
        {
            p1.sprite = Resources.Load<Sprite>("John Spacebound");
        }
        playerHP1.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP1.sizeDelta.y);

        if (player2.active)
        {
            currentPlayer = player2.GetComponent<BattlePlayerScript>();
            RectTransform playerHP2 = playerHPBar2.GetComponent<RectTransform>();
            Image p2 = player2.GetComponent<Image>();
            string name2 = p2.sprite.name;
            if (currentPlayer.health <= 0)
            {
                p2.sprite = Resources.Load<Sprite>("Dead " + name2);
            }
            else
            {
                p2.sprite = Resources.Load<Sprite>("Bob");
            }
            playerHP2.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP2.sizeDelta.y);
        }
        
        if (player3.active)
        {
            currentPlayer = player3.GetComponent<BattlePlayerScript>();
            RectTransform playerHP3 = playerHPBar3.GetComponent<RectTransform>();
            Image p3 = player3.GetComponent<Image>();
            string name = p3.sprite.name;
            if (currentPlayer.health <= 0)
            {
                p3.sprite = Resources.Load<Sprite>("Dead " + name);
            }
            else
            {
                p3.sprite = Resources.Load<Sprite>("Thozos");
            }
            playerHP3.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP3.sizeDelta.y);
        }

        if (player4.active)
        {
            currentPlayer = player4.GetComponent<BattlePlayerScript>();
            RectTransform playerHP4 = playerHPBar4.GetComponent<RectTransform>();
            Image p4 = player4.GetComponent<Image>();
            string name = p4.sprite.name;
            if (currentPlayer.health <= 0)
            {
                p4.sprite = Resources.Load<Sprite>("Dead " + name);
            }
            else
            {
                p4.sprite = Resources.Load<Sprite>("Janet");
            }
            playerHP4.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP4.sizeDelta.y);
        }

    }
    public void checkForEndOfBattle()
    {
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>(); 
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        if (p1.health <= 0 && p2.health <= 0 && p3.health <= 0 && p4.health <= 0)
        {
            //StartCoroutine(Fade(1));
            //StartCoroutine(Fade(0));
            isBattleOver = true;
            exitBattle();
            dataPersistenceManager.LoadGame();
            Debug.Log("all players dead");
        }


        if (e1.health <= 0 && e2.health <= 0 && e3.health <= 0 && e4.health <= 0)
        {
            isBattleOver = true;
        }
    }

    public void fleeButtonClick()
    {
        resetMenu();
        StartCoroutine(exitBattle());
    }

    public IEnumerator exitBattle()
    {
        Debug.Log("exiting battle");
        isBattleOver = false;
        battleEars.enabled = false;
        overworldEars.enabled = true;
        battleLog.Clear();
        johnMovement.EndBattle();
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));

        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        p1.health = p1.startingHealth;
        p2.health = p2.startingHealth;
        p3.health = p3.startingHealth;
        p4.health = p4.startingHealth;

        e1.health = e1.startingHealth;
        e2.health = e2.startingHealth;
        e3.health = e3.startingHealth;
        e4.health = e4.startingHealth;

        updatePlayerHealth();
        updateEnemyHealth();
        fadeImage.gameObject.SetActive(false);
        johnMovement.inBattle = false;
        battleSystem.gameObject.SetActive(false); //last
    }

    private void UpdateEnemyArrows()
    {
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();
        if (currentAttack == 'b' || currentAttack == 'r')
        {
            enemy1Arrow.gameObject.SetActive(currentEnemy == 1);
            enemy2Arrow.gameObject.SetActive(currentEnemy == 2);
            enemy3Arrow.gameObject.SetActive(currentEnemy == 3);
            enemy4Arrow.gameObject.SetActive(currentEnemy == 4);
        }
        else if(currentAttack == 's')
        {
            if (e1.health > 0)
            {
                enemy1Arrow.gameObject.SetActive(currentEnemy == 1 || currentEnemy == 2);
            }
            if (e2.health > 0)
            {
                enemy2Arrow.gameObject.SetActive(currentEnemy == 1 || currentEnemy == 2);
            }
            //
            if (e3.health > 0)
            {
                enemy3Arrow.gameObject.SetActive(currentEnemy == 3 || currentEnemy == 4);
            }
            if (e4.health > 0)
            {
                enemy4Arrow.gameObject.SetActive(currentEnemy == 3 || currentEnemy == 4);
            }
        }
        else if (currentAttack == 'p')
        {
            if (e1.health > 0)
            {
                enemy1Arrow.gameObject.SetActive(currentEnemy == 1 || currentEnemy == 3);
            }
            if (e3.health > 0)
            {
                enemy3Arrow.gameObject.SetActive(currentEnemy == 1 || currentEnemy == 3);
            }
            //
            if (e2.health > 0)
            {
                enemy2Arrow.gameObject.SetActive(currentEnemy == 2 || currentEnemy == 4);
            }
            if (e4.health > 0)
            {
                enemy4Arrow.gameObject.SetActive(currentEnemy == 2 || currentEnemy == 4);
            }
        }
    }
    public void resetMenu()
    {
        atkMenu.SetActive(false);
        defMenu.SetActive(false);
        invMenu.SetActive(false);
        runMenu.SetActive(false);
        canSelect2 = false;
        currentMenuArrow = menuArrowTemp; //THIS FIXES THE ISSUE WITH MENU LOCKING UP ON PRESSING ESCAPE
        updateMenuArrows();
        innerMenuArrow = 1;
        updateInnerArrow();
    }
    public void openMenu(int menu)
    {
        switch (menu)
        {
            case 0:
                if (!atkMenu.activeSelf)
                {
                    soundManager.PlaySoundClip(0);
                    atkMenu.SetActive(true);
                    defMenu.SetActive(false);
                    invMenu.SetActive(false);
                    runMenu.SetActive(false);
                }
                else
                {
                    soundManager.PlaySoundClip(4);
                    atkMenu.SetActive(false);
                }
                break;
            case 1:
                if (!defMenu.activeSelf)
                {
                    soundManager.PlaySoundClip(0);
                    atkMenu.SetActive(false);
                    defMenu.SetActive(true);
                    invMenu.SetActive(false);
                    runMenu.SetActive(false);
                }
                else
                {
                    soundManager.PlaySoundClip(4);
                    defMenu.SetActive(false);
                }
                break;
            case 2:
                if (!invMenu.activeSelf)
                {
                    soundManager.PlaySoundClip(0);
                    atkMenu.SetActive(false);
                    defMenu.SetActive(false);
                    invMenu.SetActive(true);
                    runMenu.SetActive(false);
                }
                else
                {
                    soundManager.PlaySoundClip(4);
                    invMenu.SetActive(false);
                }
                break;
            case 3:
                if (!runMenu.activeSelf)
                {
                    soundManager.PlaySoundClip(0);
                    atkMenu.SetActive(false);
                    defMenu.SetActive(false);
                    invMenu.SetActive(false);
                    runMenu.SetActive(true);
                }
                else
                {
                    soundManager.PlaySoundClip(4);
                    runMenu.SetActive(false);
                }
                break;
        }
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


    public void updateTurns()
    {
        turnCounter.text = "Turn " + turnCounterIndex;
    }
}
