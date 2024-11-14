using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

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

    public Image fadeImage;
    public float fadeDuration = 0.3f;

    public TextMeshProUGUI turnCounter;
    public int turnCounterIndex = 1;

    private OmniDirectionalMovement johnMovement;
    private SoundManager soundManager;

    public Button menuBlocking;
    void OnEnable()
    {
        menuBlocking.gameObject.SetActive(false);
        turnCounter.text = "Turn 1";
        enemy1.gameObject.SetActive(true);

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
    }
    void Update()
    {
        // Only check input if the player is currently selecting an enemy
        if (isSelectingEnemy)
        {
            HandleEnemySelection();
        }
        if(isBattleOver == true)
        {
            StartCoroutine(exitBattle());
        }
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
        playerTurn++;
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
        int attackPower = 30;
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        if(randomNumber == 1)
        {
            p1.health = p1.health - attackPower;
            Debug.Log("attack 1");
        }
        else if(randomNumber == 2)
        {
            p2.health = p2.health - attackPower;
            Debug.Log("attack 2");
        }
        else if (randomNumber == 3)
        {
            p3.health = p3.health - attackPower;
            Debug.Log("attack 3");
        }
        else if (randomNumber == 4)
        {
            p4.health = p4.health - attackPower;
            Debug.Log("attack 4");
        }
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
        if (currentPlayer.health <= 0)
        {
            player1.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        playerHP1.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP1.sizeDelta.y);

        currentPlayer = player2.GetComponent<BattlePlayerScript>();
        RectTransform playerHP2 = playerHPBar2.GetComponent<RectTransform>();
        if (currentPlayer.health <= 0)
        {
            player2.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        playerHP2.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP2.sizeDelta.y);

        currentPlayer = player3.GetComponent<BattlePlayerScript>();
        RectTransform playerHP3 = playerHPBar3.GetComponent<RectTransform>();
        if (currentPlayer.health <= 0)
        {
            player3.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        playerHP3.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP3.sizeDelta.y);

        currentPlayer = player4.GetComponent<BattlePlayerScript>();
        RectTransform playerHP4 = playerHPBar4.GetComponent<RectTransform>();
        if (currentPlayer.health <= 0)
        {
            player4.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
        }
        playerHP4.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP4.sizeDelta.y);

    }
    public void checkForEndOfBattle()
    {
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>(); 

        if(e1.health <= 0)
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
