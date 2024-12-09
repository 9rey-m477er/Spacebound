using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class BossBattleUIScript : MonoBehaviour
{
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

    public GameObject enemy1;
    public Image enemyImage;
    public BattleEnemyScript battleEnemyScript;
    public BattlePlayerScript battlePlayerScript;
    public DataPersistenceManager dataPersistenceManager;
    public TextMeshProUGUI enemyname;

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


    public int currentMenuArrow = 1;
    public int innerMenuArrow = 1;

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

    public GameObject attackArrow;
    public GameObject defendArrow;
    public GameObject invenArrow;
    public GameObject runArrow;
    public bool isinMenu = false;

    public bool canSelect = false;
    public bool canSelect2 = false;
    public Button menuBlocking;
    void OnEnable()
    {
        resetMenu();
        currentMenuArrow = 1;
        updateMenuArrows();
        innerMenuArrow = 1;
        updateInnerArrow();
        isinMenu = true;
        menuBlocking.gameObject.SetActive(false);
        turnCounter.text = "Turn 1";
        enemy1.gameObject.SetActive(true);

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
        playerTeamSpawn();
    }
    void Update()
    {
        // Only check input if the player is currently selecting an enemy
        if (isSelectingEnemy)
        {
            HandleEnemySelection();
        }
        else if (isinMenu && isSelectingEnemy == false && menuBlocking.gameObject.active == false)
        {
            menuArrowNav();
        }
        if (isBattleOver == true)
        {
            StartCoroutine(exitBattle());
        }
        //updateTurnText();
    }
    public void playerTeamSpawn()
    {
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        if (johnMovement.bobActive == true)
        {
            p2.gameObject.SetActive(true);
            p2.health = p2.startingHealth;
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
            p3.health = p3.startingHealth;
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
            p4.health = p4.startingHealth;
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

    public void menuArrowNav()
    {
        if (atkMenu.active == false && defMenu.active == false && invMenu.active == false && runMenu.active == false && isSelectingEnemy == false)
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
            }
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) && canSelect2 == false && isSelectingEnemy == false)
            {
                canSelect2 = true;
                if (currentMenuArrow == 1)
                {
                    openMenu(0);
                    StartCoroutine(SelectionWaitCoroutine(0.2f));
                    currentMenuArrow = 0;
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

        if (atkMenu.active == true || defMenu.active == true || invMenu.active == true || runMenu.active == true && isSelectingEnemy == false)
        {
            if (innerMenuArrow == 1)
            {
                if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
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
                    if (runMenu.activeInHierarchy == true)
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
            if (atkArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
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
            if (runArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                //fleeButtonClick();
            }
            if (runArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true)
            {
                resetMenu();
            }
        }

        if ((atkMenu.active == true || defMenu.active == true || invMenu.active == true || runMenu.active == true) && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)))
        {
            innerMenuArrow = 1;
            updateInnerArrow();
            resetMenu();
        }
    }

    public void updateInnerArrow()
    {
        if (atkMenu.active == true)
        {
            if (innerMenuArrow == 1)
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
        else if (defMenu.active == true)
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

    public void updateMenuArrows()
    {
        if (currentMenuArrow == 1)
        {
            attackArrow.gameObject.SetActive(true);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(false);
        }
        else if (currentMenuArrow == 2)
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
        resetMenu();
    }

    public void StartScriptedBattle(EnemyStatSheet enemy)
    {
        soundManager.BattleTransition(enemy.encounterIntro, enemy.encounterMusic) ;
        SetEnemyStats(enemy);
        johnMovement.inBattle = true;
    }

    public void SetEnemyStats(EnemyStatSheet sheet)
    {
        Debug.Log("Passing Information to Enemy");
        enemyImage.sprite = sheet.sprite;
        Debug.Log("Sprite Passed");

        // Resize the GameObject based on the sprite dimensions
        RectTransform rectTransform = enemy1.GetComponent<RectTransform>();
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
        battleEnemyScript.health = sheet.health;
        Debug.Log("Enemy Health = " + battleEnemyScript.health);
        battleEnemyScript.startingHealth = sheet.health;
        Debug.Log("Enemy Starting Health = " + battleEnemyScript.startingHealth);
        battleEnemyScript.attackStrength = sheet.attackStrength;
        Debug.Log("Enemy Attack Strength = " + battleEnemyScript.attackStrength);
        battleEnemyScript.baseExpValue = sheet.baseExpValue;
        Debug.Log("Enemy Base EXP Value = " + battleEnemyScript.baseExpValue);
        battleEnemyScript.enemyName = sheet.enemyName;
        enemyname.text = battleEnemyScript.enemyName;
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

    public void enemyAttack(int randomNumber)
    {
        float attackPower = battleEnemyScript.attackStrength;
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
    }

    private IEnumerator EnemyAttackSequence()
    {
        if(enemy1.gameObject.active == true)
        {
            yield return new WaitForSeconds(0.75f);
            int randomNumber = Random.Range(1, 5); // randomly choose who to attack
            if(randomNumber == 1)
            {
                party1Reticle.SetActive(true);
            }
            else if(randomNumber == 2)
            {
                party2Reticle.SetActive(true);
            }
            else if (randomNumber == 3)
            {
                party3Reticle.SetActive(true);
            }
            else if (randomNumber == 4)
            {
                party4Reticle.SetActive(true);
            }
            yield return new WaitForSeconds(0.75f);
            soundManager.PlaySoundClip(6);
            enemyAttack(randomNumber);
            //turnCounterIndex++;
            updateTurns();
            party1Reticle.SetActive(false);
            party2Reticle.SetActive(false);
            party3Reticle.SetActive(false);
            party4Reticle.SetActive(false);
        }
        /*
        for (int i = 1; i <= 4; i++)
        {
            yield return new WaitForSeconds(1.5f);
            int randomNumber = Random.Range(1, 5); // randomly choose who to attack
            //reticle
            enemyAttack(randomNumber);
            turnCounterIndex++;
            updateTurns();
        }
        */
        menuBlocking.gameObject.SetActive(false);
        isinMenu = true;
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
        resetMenu();
        currentAttack = 'b';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 25;
        
        if(enemy1.active == true)
        {
            currentEnemy = 1;
        }
        UpdateEnemyArrows();
    }

    public void attackSlot2() //slash   //   XO
    {                                   //   XO
        resetMenu();
        currentAttack = 's';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 15;

        if (enemy1.active == true)
        {
            currentEnemy = 1;
        }
        UpdateEnemyArrows();
    }

    public void attackSlot3() //poke    // XX
    {                                   // OO
        resetMenu();
        currentAttack = 'p';
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 15;

        if (enemy1.active == true)
        {
            currentEnemy = 1;
        }
        UpdateEnemyArrows();
    }
    private void HandleEnemySelection()
    {
        BattleEnemyScript enemy1HealthCheck = enemy1.GetComponent<BattleEnemyScript>();
        currentEnemy = 1;

        if (Input.GetKeyDown(KeyCode.E))//confirm
        {
            isSelectingEnemy = false; 
            ExecuteAttack();
            checkForEndOfBattle();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Debug.Log("back out");
            enemy1Arrow.gameObject.SetActive(false);
            resetMenu();
            isSelectingEnemy = false;
        }
    }

    public void ExecuteAttack()
    {
        Debug.Log("attacking");
        BattleEnemyScript selectedEnemyScript = null;
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        selectedEnemyScript = e1.GetComponent<BattleEnemyScript>();

        if (selectedEnemyScript != null)
        {
            if(currentAttack == 'b') //bash
            {
                e1.health -= attackPower;    
            }
            else if(currentAttack == 's') //slash
            {
                e1.health -= attackPower;
            }
            else if(currentAttack == 'p') //poke
            {
                e1.health -= attackPower;
            }
            currentAttack = 'n';
            soundManager.PlaySoundClip(6);
            Debug.Log("Attacked enemy " + currentEnemy + ", remaining health: " + e1.health);
        }
        updateEnemyHealth();
        enemy1Arrow.gameObject.SetActive(false);
        incrementTurn();
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
        enemyHP1.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 338, enemyHP1.sizeDelta.y); //adjust the number to be the actual full width of the current health bar
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
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();


        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        if (p1.health <= 0 && p2.health <= 0 && p3.health <= 0 && p4.health <= 0)
        {
            //StartCoroutine(Fade(1));
            //StartCoroutine(Fade(0));
            isBattleOver = true;
            exitBattle();
            dataPersistenceManager.LoadGame();
            Debug.Log("all players dead");
        }
        if (e1.health <= 0)
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
        isBattleOver = false;
        johnMovement.EndBattle();
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));

        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();

        p1.health = p1.startingHealth;
        p2.health = p2.startingHealth;
        p3.health = p3.startingHealth;
        p4.health = p4.startingHealth;

        e1.health = e1.startingHealth;

        updatePlayerHealth();
        updateEnemyHealth();
        fadeImage.gameObject.SetActive(false);
        johnMovement.inBattle = false;
        battleSystem.gameObject.SetActive(false); //last
    }

    private void UpdateEnemyArrows()
    {
        enemy1Arrow.gameObject.SetActive(currentEnemy == 1);
    }
    public void resetMenu()
    {
        atkMenu.SetActive(false);
        defMenu.SetActive(false);
        invMenu.SetActive(false);
        runMenu.SetActive(false);
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
