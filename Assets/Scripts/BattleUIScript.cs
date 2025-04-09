using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEditor;

public class BattleUIScript : MonoBehaviour
{
    public int tutorialStage = 0;
    public GameObject tutorialDoor;
    public TextMeshProUGUI diaText;
    public TextMeshProUGUI diaName;
    public GameObject textbox;
    public TextMeshProUGUI tutAdvancetxt;

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
    public GameObject reportArrow;

    public GameObject bigReport;

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
    public bool waiting = false;
    public bool isAttacking = false;

    public List<Sprite> forestSpritePool = new List<Sprite>();
    public List<EnemyStatSheet> enemyPool = new List<EnemyStatSheet>();

    //Battle Log
    private List<string> battleLog = new List<string> {"", "", "", "", "", "", "", "", "", "", "", ""};
    private string battleLogEntry = string.Empty;
    public TextMeshProUGUI battleLogLine1;
    public TextMeshProUGUI battleLogLine2;
    public TextMeshProUGUI battleLogLine3;
    public TextMeshProUGUI battleLogLine4;
    public TextMeshProUGUI bigLogLine1, bigLogLine2, bigLogLine3, bigLogLine4, bigLogLine5, bigLogLine6, 
        bigLogLine7, bigLogLine8, bigLogLine9, bigLogLine10, bigLogLine11, bigLogLine12;
    public GameObject battleLogObj;
    public bool p1DeathRecorded;

    //Post-Battle Report
    private List<EnemyStatSheet> enemies = new List<EnemyStatSheet>();
    public TextMeshProUGUI line1Name, line1EXP;
    public TextMeshProUGUI line2Name, line2EXP;
    public TextMeshProUGUI line3Name, line3EXP;
    public TextMeshProUGUI line4Name, line4EXP;
    public TextMeshProUGUI line5Total, expToNextTXT;
    public TextMeshProUGUI currentLVL, nextLVL;
    public Image expBarInner;
    public GameObject postReportObj;
    private bool postReportOpen;

    //Level Up
    public GameObject LUJohn, LUBob, LUThozos, LUJanet, LUStevphen, LevelUpButton, XButton, EXPArea, LUArea;
    public TextMeshProUGUI LUJohnHP, LUJohnBash, LUJohnSlash, LUJohnPoke;
    public TextMeshProUGUI LUBobHP, LUBobBash, LUBobSlash, LUBobPoke;
    public TextMeshProUGUI LUThozosHP, LUThozosBash, LUThozosSlash, LUThozosPoke;
    public TextMeshProUGUI LUJanetHP, LUJanetBash, LUJanetSlash, LUJanetPoke;
    public TextMeshProUGUI LUStevphenHP, LUStevphenBash, LUStevphenSlash, LUStevphenPoke;
    public TextMeshProUGUI LevelReached, LUExpToNext;

    public GameObject overworldMenu;

    public CharacterStatSheet john, bob, thozos, janet, stephven;
    public CharacterStatHandler characterStatHandler;

    public bool fled = false;
    public int expToGive;
    public bool char1Dead, char2Dead, char3Dead, char4Dead;

    public TextMeshProUGUI turnName;

    public float p1BaseEvade;
    public float p2BaseEvade;
    public float p3BaseEvade;
    public float p4BaseEvade;

    public float p1BaseDefense;
    public float p2BaseDefense;
    public float p3BaseDefense;
    public float p4BaseDefense;

    
    public int defender;
    public int beingDefended;
    public TextMeshProUGUI chooseDefendText;
    public bool isSelectingAlly;


    public int selection;

    public int fleeChance = 100;
    public TextMeshProUGUI fleeText;

    void OnEnable()
    {
        tutAdvancetxt.gameObject.SetActive(false);
        resetMenu();
        currentMenuArrow = 1;
        expToGive = 0;
        updateMenuArrows();
        innerMenuArrow = 1;
        updateInnerArrow();
        isinMenu = true;
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
        reportArrow.gameObject.SetActive(false);

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

        bigReport.SetActive(false);
        postReportOpen = false;
        postReportObj.SetActive(false);
        LUBob.gameObject.SetActive(false);
        LUThozos.gameObject.SetActive(false);
        LUJanet.gameObject.SetActive(false);
        LUStevphen.gameObject.SetActive(false);
        LevelUpButton.gameObject.SetActive(false);

        battleLogLine1.text = "";
        battleLogLine2.text = "";
        battleLogLine3.text = "";
        battleLogLine4.text = "";
        bigLogLine1.text = "";
        bigLogLine2.text = "";
        bigLogLine3.text = "";
        bigLogLine4.text = "";
        bigLogLine5.text = "";
        bigLogLine6.text = "";
        bigLogLine7.text = "";
        bigLogLine8.text = "";
        bigLogLine9.text = "";
        bigLogLine10.text = "";
        bigLogLine11.text = "";
        bigLogLine12.text = "";
        //battleLogObj.SetActive(true);

        chooseDefendText.gameObject.SetActive(false);



        if(johnMovement.tutorialBattle == false)
        {
            //Randomly Assign Enemies
            enemyPool = johnMovement.encounterPool;
            enemies.Clear();
            rollEnemy(enemy1, enemyPool);
            rollEnemy(enemy2, enemyPool);
            rollEnemy(enemy3, enemyPool);
            rollEnemy(enemy4, enemyPool);
            updateEnemyNames();
            updateEnemyHealth();
            playerTeamSpawn();
        }
        else
        {
            tutorialStage = 1;
            turnName.text = "Party Turn!";
            turnName.color = Color.cyan;
            enemyPool = johnMovement.tutorialPool; //what should atk strength be - based on party size
            enemies.Clear();
            rollEnemy(enemy1, enemyPool);
            rollEnemy(enemy2, enemyPool);
            rollEnemy(enemy3, enemyPool);
            rollEnemy(enemy4, enemyPool);
            updateEnemyNames();
            updateEnemyHealth();
            playerTeamSpawn();
            atkArrow1.gameObject.SetActive(true);
            menuBlocking.gameObject.SetActive(true);           
        }

        isBattleOver = false;
        isSelectingAlly = false;

        fleeText.text = fleeChance + "% Chance";
    }
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    public IEnumerator tutorial1() //select attack
    { 
        if(fadeImage.gameObject.active == false)
        {
            textbox.gameObject.SetActive(true);
        }
        textbox.GetComponent<RectTransform>().anchoredPosition = new Vector2(-187f, 130f);
        diaName.text = "T.H.E";
        diaText.text = "Welcome to battle! Press E to open the attack menu!";
        menuBlocking.gameObject.SetActive(false);
        atkArrow1.gameObject.SetActive(true);
        if (fadeImage.gameObject.active == false && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            atkMenu.SetActive(true);
            tutorialStage = 2;
            innerMenuArrow = 1;
            updateInnerArrow();
        }

        yield return new WaitUntil(() => tutorialStage == 2);
    }

    public IEnumerator tutorial2() //select bash
    {
        diaText.text = "Bash is a single target attack that does considerable damage! Press E to try it out!";
        yield return new WaitForSeconds(0.2f);
        if (atkArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)))
        {
            attackSlot1();
            tutorialStage = 3;
        }
        yield return new WaitUntil(() => tutorialStage == 3);
    }

    public IEnumerator tutorial3() //select enemy
    {
        diaText.text = "This is how you will select enemies, for now we will attack this one. Press E to use your bash attack!";
        yield return new WaitForSeconds(0.2f);
        enemy1Arrow.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
            atkMenu.SetActive(false);
            e1.health = 25;
            Transform textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
            ShowFloatingText("25", textHolder.position, textHolder);
            updateEnemyHealth();
            enemy1Arrow.gameObject.SetActive(false);
            innerMenuArrow = 2;
            tutorialStage = 4;
        }
        yield return new WaitUntil(() => tutorialStage == 4);
    }
    ////////
    public IEnumerator tutorial4() //select menu
    {
        diaText.text = "Great Job! Now let's try another attack, press E again to open the menu.";
        yield return new WaitForSeconds(0.2f);
        enemy1Arrow.gameObject.SetActive(false);
        atkArrow2.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            atkMenu.SetActive(true);
            tutorialStage = 5;
            innerMenuArrow = 2;
            updateInnerArrow();
        }
        yield return new WaitUntil(() => tutorialStage == 5);
    }
    public IEnumerator tutorial5() //select slash
    {
        diaText.text = "Slash is a vertical based attack that does less damage but affects two enemies at once!";
        yield return new WaitForSeconds(0.2f);
        if (atkArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)))
        {
            attackSlot2();
            tutorialStage = 6;
        }
        yield return new WaitUntil(() => tutorialStage == 6);
    }
    public IEnumerator tutorial6() //select enemy
    {
        diaText.text = "Show those dummies what your cool new attack does!";
        yield return new WaitForSeconds(0.2f);
        enemy1Arrow.gameObject.SetActive(true);
        enemy2Arrow.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
            BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();

            atkMenu.SetActive(false);
            e1.health = 0;
            e2.health = 25;
            Transform textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
            Transform textHolder2 = GameObject.Find("textHolder(e" + 2 + ")")?.transform;

            ShowFloatingText("25", textHolder.position, textHolder);
            ShowFloatingText("25", textHolder2.position, textHolder2);

            updateEnemyHealth();
            enemy1Arrow.gameObject.SetActive(false);
            enemy2Arrow.gameObject.SetActive(false);
            tutorialStage = 7;
        }
        yield return new WaitUntil(() => tutorialStage == 7);
    }
    ////////////
    public IEnumerator tutorial7() //select menu
    {
        enemy1Arrow.gameObject.SetActive(false);
        enemy2Arrow.gameObject.SetActive(false);
        diaText.text = "Awesome! Open up your menu again and I'll teach you about your last attack!";
        yield return new WaitForSeconds(0.2f);
        atkArrow3.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            atkMenu.SetActive(true);
            tutorialStage = 8;
            innerMenuArrow = 3;
            updateInnerArrow();
        }
        yield return new WaitUntil(() => tutorialStage == 8);
    }
    public IEnumerator tutorial8() //select poke
    {
        diaText.text = "Poke is similar to slash in that it attacks two enemies, but this allows you to attack in a horizontal manner!";
        yield return new WaitForSeconds(0.2f);
        if (atkArrow3.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)))
        {
            attackSlot3();
            tutorialStage = 9;
        }
        yield return new WaitUntil(() => tutorialStage == 9);
    }
    public IEnumerator tutorial9() //select enemy
    {
        diaText.text = "Take them out!";
        yield return new WaitForSeconds(0.2f);
        enemy2Arrow.gameObject.SetActive(true);
        enemy4Arrow.gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            BattleEnemyScript e1 = enemy2.GetComponent<BattleEnemyScript>();
            BattleEnemyScript e2 = enemy4.GetComponent<BattleEnemyScript>();

            atkMenu.SetActive(false);
            e1.health = 0;
            e2.health = 25;
            Transform textHolder = GameObject.Find("textHolder(e" + 2 + ")")?.transform;
            Transform textHolder2 = GameObject.Find("textHolder(e" + 4 + ")")?.transform;

            ShowFloatingText("25", textHolder.position, textHolder);
            ShowFloatingText("25", textHolder2.position, textHolder2);

            updateEnemyHealth();
            enemy2Arrow.gameObject.SetActive(false);
            enemy4Arrow.gameObject.SetActive(false);
            tutorialStage = 10;
        }
        yield return new WaitUntil(() => tutorialStage == 10);
    }
    public IEnumerator tutorial10()
    {
        enemy2Arrow.gameObject.SetActive(false);
        enemy4Arrow.gameObject.SetActive(false);
        diaText.text = "Great job attacking! Defending is also important in battle, we will learn about that next.";
        tutAdvancetxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            tutorialStage = 11;
        }
        yield return new WaitUntil(() => tutorialStage == 11);
    }
    public IEnumerator tutorial11()
    {
        diaText.text = "Dodge increases your ability to avoid attacks for the rest of that turn. Super helpful if you are low on health!";
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            tutorialStage = 12;
        }
        yield return new WaitUntil(() => tutorialStage == 12);
    }
    public IEnumerator tutorial12()
    {
        diaText.text = "Defend Ally allows you to take hits for whomever you choose, while Brace reduces the amount of damage you take for a turn.";
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            tutorialStage = 13;
        }
        yield return new WaitUntil(() => tutorialStage == 13);
    }
    public IEnumerator tutorial13()
    {
        diaText.text = "Fleeing is usually always an option, when selected you can also see your chance to escape!";
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            tutorialStage = 14;
        }
        yield return new WaitUntil(() => tutorialStage == 14);
    }
    public IEnumerator tutorial14()
    {
        diaText.text = "Thats all for now, good luck adventurer!";
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            tutorialStage = 15;
        }
        yield return new WaitUntil(() => tutorialStage == 15);
    }
    public void tutorial15()
    {
        tutAdvancetxt.gameObject.SetActive(false);
        textbox.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -10f);
        textbox.gameObject.SetActive(false);
        johnMovement.tutorialBattle = false;
        menuBlocking.gameObject.SetActive(false);
        tutorialStage = 0;
        tutorialDoor.gameObject.SetActive(false);
        StartCoroutine(exitBattle());
    }
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    public void playerTeamSpawn()
    {
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        p1BaseEvade = p1.evasiveness;
        p2BaseEvade = p2.evasiveness;
        p3BaseEvade = p3.evasiveness;
        p4BaseEvade = p4.evasiveness;

        p1.health = john.currentHP;
        p1.startingHealth = john.characterHP;
        p1.bashMultiplier = john.bashMultiplier;
        p1.slashMultiplier = john.slashMultiplier;
        p1.pokeMultiplier = john.pokeMultiplier;
        p1.characterName = john.characterName;
        p1.characterSprite = john.characterSprite;
        p1.attackSprite = john.attackSprite;
        p1.downedSprite = john.downedSprite;
        p1.hurtSprite = john.hurtSprite;

        if (johnMovement.bobActive == true)
        {
            p2.gameObject.SetActive(true);
            p2.startingHealth = bob.characterHP;
            p2.health = bob.currentHP;
            p2.bashMultiplier = bob.bashMultiplier;
            p2.slashMultiplier = bob.slashMultiplier;
            p2.pokeMultiplier = bob.pokeMultiplier;
            p2.characterName = bob.characterName;
            p2.characterSprite = bob.characterSprite;
            p2.attackSprite = bob.attackSprite;
            p2.downedSprite = bob.downedSprite;
            p2.hurtSprite = bob.hurtSprite;
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
            p3.characterSprite = thozos.characterSprite;
            p3.attackSprite = thozos.attackSprite;
            p3.downedSprite = thozos.downedSprite;
            p3.hurtSprite = thozos.hurtSprite;
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
            p4.characterSprite = janet.characterSprite;
            p4.attackSprite = janet.attackSprite;
            p4.downedSprite = janet.downedSprite;
            p4.hurtSprite = janet.hurtSprite;
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

    void rollEnemy(GameObject enemy, List<EnemyStatSheet> sheetPool)
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
        targetScript.enemyName = sheet.enemyName;
        targetScript.enemyAttacks = sheet.enemyAttacks;
        targetScript.bashMultiplier = sheet.bashMultiplier;
        targetScript.slashMultiplier = sheet.slashMultiplier;
        targetScript.pokeMultiplier = sheet.pokeMultiplier;
        targetScript.baseExpValue = sheet.baseExpValue;
        expToGive += sheet.baseExpValue;
        Debug.Log("EXP To Give: " + expToGive);
        enemies.Add(sheet);

        //If the player is in the forest level and have reached level 10 or higher
        if (johnMovement.level == 0 && characterStatHandler.partyLevel >= 10)
        {
            //Exp gain is set to 1.
            expToGive = 1;
        }
    }

    void Update()
    {
        // Only check input if the player is currently selecting an enemy
        if (isBattleOver == false)
        {
            if (isSelectingEnemy)
            {
                StartCoroutine(HandleEnemySelection());
            }

            else if (isAttacking)
            {
                atkMenu.SetActive(false);
                defMenu.SetActive(false);
                invMenu.SetActive(false);
                runMenu.SetActive(false);
            }

            else if (isSelectingAlly)
            {
                StartCoroutine(ChoosingAlly());
            }

            else if (isinMenu && isSelectingEnemy == false && menuBlocking.gameObject.active == false && johnMovement.tutorialBattle == false)
            {
                menuArrowNav();
            }
        }
        else if (postReportOpen == false)
        {
            postReportOpen = true;
            postBattleReport();
        }
        if (johnMovement.tutorialBattle == false)
        {
            checkForEndOfBattle();
            updateTurnText();
        }
        else if(johnMovement.tutorialBattle == true)
        {
            if(tutorialStage == 1)
            {
                StartCoroutine(tutorial1());
            }
            else if(tutorialStage == 2)
            {
                StartCoroutine(tutorial2());
            }
            else if (tutorialStage == 3)
            {
                StartCoroutine(tutorial3());
            }
            else if (tutorialStage == 4)
            {
                StartCoroutine(tutorial4());
            }
            else if (tutorialStage == 5)
            {
                StartCoroutine(tutorial5());
            }
            else if (tutorialStage == 6)
            {
                StartCoroutine(tutorial6());
            }
            else if (tutorialStage == 7)
            {
                StartCoroutine(tutorial7());
            }
            else if (tutorialStage == 8)
            {
                StartCoroutine(tutorial8());
            }
            else if (tutorialStage == 9)
            {
                StartCoroutine(tutorial9());
            }
            else if (tutorialStage == 10)
            {
                StartCoroutine(tutorial10());
            }
            else if (tutorialStage == 11)
            {
                StartCoroutine(tutorial11());
            }
            else if (tutorialStage == 12)
            {
                StartCoroutine(tutorial12());
            }
            else if (tutorialStage == 13)
            {
                StartCoroutine(tutorial13());
            }
            else if (tutorialStage == 14)
            {
                StartCoroutine(tutorial14());
            }
            else if (tutorialStage == 15)
            {
                tutorial15();
            }
        }
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
            reportArrow.gameObject.SetActive(false);
        }
        else if(currentMenuArrow == 2)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(true);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(false);
            reportArrow.gameObject.SetActive(false);
        }
        else if (currentMenuArrow == 3)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(true);
            runArrow.gameObject.SetActive(false);
            reportArrow.gameObject.SetActive(false);
        }
        else if (currentMenuArrow == 4)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(true);
            reportArrow.gameObject.SetActive(false);
        }
        else if (currentMenuArrow == 5)
        {
            attackArrow.gameObject.SetActive(false);
            defendArrow.gameObject.SetActive(false);
            invenArrow.gameObject.SetActive(false);
            runArrow.gameObject.SetActive(false);
            reportArrow.gameObject.SetActive(true);
        }
    }
    public void menuArrowNav()
    {
        if(atkMenu.active == false && defMenu.active == false && invMenu.active == false && runMenu.active == false && isSelectingEnemy == false && bigReport.active == false)
        {
            if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(currentMenuArrow != 5)
                {
                    currentMenuArrow = 5;
                    updateMenuArrows();
                }
                else if(currentMenuArrow == 5)
                {
                    currentMenuArrow = 1;
                    updateMenuArrows();
                }
                menuArrowTemp = currentMenuArrow;
            }
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
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return) && canSelect2 == false && isSelectingEnemy == false && waiting == false && isAttacking == false)
            {
                Debug.Log("top");
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
                else if (currentMenuArrow == 5)
                {
                    openMenu(4);
                    StartCoroutine(SelectionWaitCoroutine(0.2f));
                }
                innerMenuArrow = 1;
                updateInnerArrow();
                canSelect = false;
            }
        }
        
        if(bigReport.active == true)
        {
            if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
            {
                openMenu(4);
                StartCoroutine(SelectionWaitCoroutine(0.2f));
                canSelect = false;
                resetMenu();
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
            if (atkArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                attackSlot1();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            if (atkArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                attackSlot2();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            if (atkArrow3.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                attackSlot3();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            //
            if (defArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                dodge();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            
            if (defArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                defend();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            if (defArrow3.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                protect();
                StartCoroutine(postSelectionWaitCoroutine(0.2f));
            }
            //
            if (invArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                invSlot1();
                Debug.Log("islot1");
            }
            if (invArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                invSlot2();
                Debug.Log("islot2");
            }
            //
            if (runArrow1.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
            {
                fleeButtonClick();
            }
            if (runArrow2.activeInHierarchy == true && (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Return)) && canSelect == true && waiting == false)
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
            if(p1.health <= 0)
            {
                playerTurn = 2;
                incrementTurn();
                return;
            }
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
            if(p1.health > 0)
            {
                Debug.Log("starting on player 1");
                playerTurn = 1;
            }
            else if (p2.health > 0)
            {
                Debug.Log("starting on player 2");
                playerTurn = 2;
            }
            else if (p3.health > 0)
            {
                playerTurn = 3;
            }
            else if (p4.health > 0)
            {
                playerTurn = 4;
            }
            else
            {
                checkForEndOfBattle();
            }
            turnCounterIndex++;
        }
        updateTurns();
        UpdatePartyArrow();
    }

    private IEnumerator EnemyAttackSequence()
    {
        yield return new WaitForSeconds(0.5f);

        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();


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

                // Show the reticle for the selected target and play damage sound
                if (target == players[0])
                {
                    party1Reticle.SetActive(true);
                    Image playerImage = player1.GetComponent<Image>();
                    playerImage.sprite = p1.hurtSprite;
                    soundManager.PlaySoundClip(6);
                    RectTransform rt = playerImage.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
                }
                else if (target == players[1])
                {
                    party2Reticle.SetActive(true);
                    Image playerImage = player2.GetComponent<Image>();
                    playerImage.sprite = p2.hurtSprite;
                    soundManager.PlaySoundClip(6);
                    RectTransform rt = playerImage.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);

                }
                else if (target == players[2])
                {
                    party3Reticle.SetActive(true);
                    Image playerImage = player3.GetComponent<Image>();
                    playerImage.sprite = p3.hurtSprite;
                    soundManager.PlaySoundClip(6);
                    RectTransform rt = playerImage.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
                }
                else if (target == players[3])
                {
                    party4Reticle.SetActive(true);
                    Image playerImage = player4.GetComponent<Image>();
                    playerImage.sprite = p4.hurtSprite;
                    soundManager.PlaySoundClip(6);
                    RectTransform rt = playerImage.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
                }

                // Play attack sound and deal damage using the enemy's attackStrength
                yield return new WaitForSeconds(0.75f);
                target.health -= enemyScript.attackStrength * ((100 - target.defMultiplier)/100);

                //Write the Attack to the Battle Log
                Debug.Log($"{target.characterName} was attacked by {enemyScript.enemyName}! ({enemyScript.attackStrength} HP)");
                battleLogEntry = $"{target.characterName} was attacked by {enemyScript.enemyName}! ({enemyScript.attackStrength} HP)";
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
                            float targetDodge = 0;
                            BattlePlayerScript originalTarget = target;

                            // Handle Defenders

                            if (target == players[0] && defender != 0 && beingDefended == 1)
                            {
                                if (defender == 2 && p2.health > 0)
                                {
                                    target = players[1];
                                }
                                else if (defender == 3 && p3.health > 0)
                                {
                                    target = players[2];
                                }
                                else if (defender == 4 && p4.health > 0)
                                {
                                    target = players[3];
                                }
                                Debug.Log($"New Target = {target.characterName}");
                            }
                            //////////
                            if (target == players[1] && defender != 0 && beingDefended == 2)
                            {
                                if (defender == 1 && p1.health > 0)
                                {
                                    target = players[0];
                                }
                                else if (defender == 3 && p3.health > 0)
                                {
                                    target = players[2];
                                }
                                else if (defender == 4 && p4.health > 0)
                                {
                                    target = players[3];
                                }
                                Debug.Log($"New Target = {target.characterName}");
                            }
                            /////////
                            if (target == players[2] && defender != 0 && beingDefended == 3)
                            {
                                if (defender == 2 && p2.health > 0)
                                {
                                    target = players[1];
                                }
                                else if (defender == 1 && p1.health > 0)
                                {
                                    target = players[0];
                                }
                                else if (defender == 4 && p4.health > 0)
                                {
                                    target = players[3];
                                }
                                Debug.Log($"New Target = {target.characterName}");
                            }
                            ////////
                            if (target == players[3] && defender != 0 && beingDefended == 4)
                            {
                                if (defender == 2 && p2.health > 0)
                                {
                                    target = players[1];
                                }
                                else if (defender == 3 && p3.health > 0)
                                {
                                    target = players[2];
                                }
                                else if (defender == 1 && p1.health > 0)
                                {
                                    target = players[0];
                                }
                                Debug.Log($"New Target = {target.characterName}");
                            }
                            // target now equals player taking damage
                            // Show the reticle for the selected target and play damage sound
                            if (target == players[0])
                            {
                                targetDodge = p1.evasiveness;
                                if(defender != 0)
                                {
                                    if(originalTarget == players[0])
                                    {
                                        partyReticle(1);
                                    }
                                    else if(originalTarget == players[1])
                                    {
                                        partyReticle(2);
                                    }
                                    else if (originalTarget == players[2])
                                    {
                                        partyReticle(3);
                                    }
                                    else if (originalTarget == players[3])
                                    {
                                        partyReticle(4);
                                    }
                                }
                                else
                                {
                                    party1Reticle.SetActive(true);
                                }
                                Image playerImage = player1.GetComponent<Image>();
                                soundManager.PlaySoundClip(6);
                                playerImage.sprite = p1.hurtSprite;
                                RectTransform rt = playerImage.GetComponent<RectTransform>();
                                rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
                            }
                            else if (target == players[1])
                            {
                                targetDodge = p2.evasiveness;
                                if (defender != 0)
                                {
                                    if (originalTarget == players[0])
                                    {
                                        partyReticle(1);
                                    }
                                    else if (originalTarget == players[1])
                                    {
                                        partyReticle(2);
                                    }
                                    else if (originalTarget == players[2])
                                    {
                                        partyReticle(3);
                                    }
                                    else if (originalTarget == players[3])
                                    {
                                        partyReticle(4);
                                    }
                                }
                                else
                                {
                                    party2Reticle.SetActive(true);
                                }
                                Image playerImage = player2.GetComponent<Image>();
                                soundManager.PlaySoundClip(6);
                                playerImage.sprite = p2.hurtSprite;
                                RectTransform rt = playerImage.GetComponent<RectTransform>();
                                rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);

                            }
                            else if (target == players[2])
                            {
                                targetDodge = p3.evasiveness;
                                if (defender != 0)
                                {
                                    if (originalTarget == players[0])
                                    {
                                        partyReticle(1);
                                    }
                                    else if (originalTarget == players[1])
                                    {
                                        partyReticle(2);
                                    }
                                    else if (originalTarget == players[2])
                                    {
                                        partyReticle(3);
                                    }
                                    else if (originalTarget == players[3])
                                    {
                                        partyReticle(4);
                                    }
                                }
                                else
                                {
                                    party3Reticle.SetActive(true);
                                }
                                Image playerImage = player3.GetComponent<Image>();
                                soundManager.PlaySoundClip(6);
                                playerImage.sprite = p3.hurtSprite;
                                RectTransform rt = playerImage.GetComponent<RectTransform>();
                                rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
                            }
                            else if (target == players[3])
                            {
                                targetDodge = p4.evasiveness;
                                if (defender != 0)
                                {
                                    if (originalTarget == players[0])
                                    {
                                        partyReticle(1);
                                    }
                                    else if (originalTarget == players[1])
                                    {
                                        partyReticle(2);
                                    }
                                    else if (originalTarget == players[2])
                                    {
                                        partyReticle(3);
                                    }
                                    else if (originalTarget == players[3])
                                    {
                                        partyReticle(4);
                                    }
                                }
                                else
                                {
                                    party4Reticle.SetActive(true);
                                }
                                Image playerImage = player4.GetComponent<Image>();
                                soundManager.PlaySoundClip(6);
                                playerImage.sprite = p4.hurtSprite;
                                RectTransform rt = playerImage.GetComponent<RectTransform>();
                                rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
                            }

                            yield return new WaitForSeconds(0.75f);

                            // Deal damage using the enemy's attackStrength and the attack's attackStrength
                            int accuracyCheck = Random.Range(0, 100);
                            //////
                            int textTarget = 0;
                            if (target == players[0])
                            {
                                textTarget = 1;
                            }
                            else if (target == players[1])
                            {
                                textTarget = 2;
                            }
                            else if (target == players[2])
                            {
                                textTarget = 3;
                            }
                            else if (target == players[3])
                            {
                                textTarget = 4;
                            }
                            /////

                            Transform textHolder = null;

                            if (textTarget == 1) textHolder = player1.transform.Find("textHolder(p1)");
                            else if (textTarget == 2) textHolder = player2.transform.Find("textHolder(p2)");
                            else if (textTarget == 3) textHolder = player3.transform.Find("textHolder(p3)");
                            else if (textTarget == 4) textHolder = player4.transform.Find("textHolder(p4)");

                            if (enemyScript.accuracy - targetDodge < accuracyCheck) //miss
                            {
                                Debug.Log($"{target.characterName} dodged the {enemyScript.enemyName + "'s attack"}! (0 HP)");
                                battleLogEntry = $"{target.characterName} dodged the {enemyScript.enemyName + "'s attack"}! (0 HP)";
                                ShowFloatingText("Miss!", textHolder.position, textHolder);
                            }
                            else
                            {
                                target.health -= (enemyScript.attackStrength + chosenAttack.attackStrength) * ((100 - target.defMultiplier) / 100);
                                float temp = (enemyScript.attackStrength + chosenAttack.attackStrength) * ((100 - target.defMultiplier) / 100);
                                //Write the Attack to the Battle Log
                                Debug.Log($"{target.characterName} {attackReadout} {enemyScript.enemyName}! ({enemyScript.attackStrength} HP)");
                                battleLogEntry = $"{target.characterName} {attackReadout} {enemyScript.enemyName}! ({enemyScript.attackStrength} HP)";
                                ShowFloatingText(enemyScript.attackStrength.ToString(), textHolder.position, textHolder);
                            }

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

        // Reset Defenses
        p1.evasiveness = p1BaseEvade;
        p2.evasiveness = p2BaseEvade;
        p3.evasiveness = p3BaseEvade;
        p4.evasiveness = p4BaseEvade;
        //
        p1.defMultiplier = p1BaseDefense;
        p2.defMultiplier = p2BaseDefense;
        p3.defMultiplier = p3BaseDefense;
        p4.defMultiplier = p4BaseDefense;

        defender = 0;
        beingDefended = 0;

        if (p1.health > 0)
        {
            //Debug.Log("meowing on player 1");
            playerTurn = 1;
        }
        else if (p2.health > 0)
        {
            //Debug.Log("meowing on player 2");
            playerTurn = 2;
        }
        else if (p3.health > 0)
        {
            playerTurn = 3;
        }
        else if (p4.health > 0)
        {
            playerTurn = 4;
        }

        resetMenu();
        UpdatePartyArrow();
    }

    public void partyReticle(int target)
    {
        if(target == 1)
        {
            party1Reticle.SetActive(true);
        }
        else if(target == 2)
        {
            party2Reticle.SetActive(true);
        }
        else if (target == 3)
        {
            party3Reticle.SetActive(true);
        }
        else if (target == 4)
        {
            party4Reticle.SetActive(true);
        }
    }

    private void updateBattleLog(string logUpdate)
    {

        //If the battle log already has 4 entries
        if (battleLog.Count == 12)
        {
            //Remove the least recent entry of the battle log
            battleLog.RemoveAt(11);
        }

        //Insert the new entry (logUpdate) into the front of the battle log
        battleLog.Insert(0, logUpdate);

        Debug.Log(battleLog.ToString());

        //Write as many lines as needed based on the length of the battle log (1-4, bottom-top)
        //switch (battleLog.Count)
        //{
        //    case 1:
        //        battleLogLine1.text = battleLog[0];
        //        break;
        //    case 2:
        //        battleLogLine1.text = battleLog[0];
        //        battleLogLine2.text = battleLog[1];
        //        break;
        //    case 3:
        //        battleLogLine1.text = battleLog[0];
        //        battleLogLine2.text = battleLog[1];
        //        battleLogLine3.text = battleLog[2];
        //        break;
        //    case 4:
        //        battleLogLine1.text = battleLog[0];
        //        battleLogLine2.text = battleLog[1];
        //        battleLogLine3.text = battleLog[2];
        //        battleLogLine4.text = battleLog[3];
        //        break;
        //}
        battleLogLine1.text = battleLog[0];
        battleLogLine2.text = battleLog[1];
        battleLogLine3.text = battleLog[2];
        battleLogLine4.text = battleLog[3];
    }

    private void updateBigBattleLog()
    {
        bigLogLine1.text = battleLog[0];
        bigLogLine2.text = battleLog[1];
        bigLogLine3.text = battleLog[2];
        bigLogLine4.text = battleLog[3];
        bigLogLine5.text = battleLog[4];
        bigLogLine6.text = battleLog[5];
        bigLogLine7.text = battleLog[6];
        bigLogLine8.text = battleLog[7];
        bigLogLine9.text = battleLog[8];
        bigLogLine10.text = battleLog[9];
        bigLogLine11.text = battleLog[10];
        bigLogLine12.text = battleLog[11];
    }

    private void UpdatePartyArrow()
    {
        party1Arrow.gameObject.SetActive(playerTurn == 1 && menuBlocking.isActiveAndEnabled == false);
        party2Arrow.gameObject.SetActive(playerTurn == 2);
        party3Arrow.gameObject.SetActive(playerTurn == 3);
        party4Arrow.gameObject.SetActive(playerTurn == 4);
    }
    public void attackSlot1() //bash    XO
    {                         //        OO
        currentMenuArrow = 1;
        menuArrowTemp = currentMenuArrow;
        if(johnMovement.tutorialBattle == false)
        {
            resetMenu();
            isSelectingEnemy = true;
        }
        currentAttack = 'b';
        soundManager.PlaySoundClip(5);
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
        if (johnMovement.tutorialBattle == false)
        {
            resetMenu();
            isSelectingEnemy = true;
        }
        currentAttack = 's';
        soundManager.PlaySoundClip(5);
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
        if (johnMovement.tutorialBattle == false)
        {
            resetMenu();
            isSelectingEnemy = true;
        }
        currentAttack = 'p';
        soundManager.PlaySoundClip(5);
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

    public void dodge()
    {
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        if (playerTurn == 1)
        {
            p1.evasiveness += 45;
        }
        else if (playerTurn == 2)
        {
            p2.evasiveness += 45;
        }
        else if (playerTurn == 3)
        {
            p3.evasiveness += 45;
        }
        else if (playerTurn == 4)
        {
            p4.evasiveness += 45;
        }
        incrementTurn();
    }
    
    public void defend()
    {
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();

        if (playerTurn == 1)
        {
            defender = 1;
        }
        else if (playerTurn == 2)
        {
            defender = 2;
        }
        else if (playerTurn == 3)
        {
            defender = 3;
        }
        else if (playerTurn == 4)
        {
            defender = 4;
        }

        chooseDefendText.gameObject.SetActive(true);
        selection = defender;
        isSelectingAlly = true;
    }
    
    public IEnumerator ChoosingAlly()
    {
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>(); ////   3   1
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>(); ////   4   2
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            soundManager.PlaySoundClip(3);
            if (selection == 3 && p4.health > 0) selection = 4;
            else if (selection == 3 && p4.health <= 0) selection = 2;
            else if (selection == 1 && p2.health > 0) selection = 2;
            else if (selection == 1 && p2.health <= 0) selection = 4;
            selectingAllyArrows(selection);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            soundManager.PlaySoundClip(3);
            if (selection == 4 && p3.health > 0) selection = 3;
            else if (selection == 4 && p3.health <= 0) selection = 1;
            else if (selection == 2 && p1.health > 0) selection = 1;
            else if (selection == 2 && p1.health <= 0) selection = 3;
            selectingAllyArrows(selection);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            soundManager.PlaySoundClip(3);
            if (selection == 1 && p3.health > 0) selection = 3;
            else if (selection == 1 && p3.health <= 0) selection = 4;
            else if (selection == 2 && p4.health > 0) selection = 4;
            else if (selection == 2 && p4.health <= 0) selection = 3;
            selectingAllyArrows(selection);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            soundManager.PlaySoundClip(3);
            if (selection == 3 && p1.health > 0) selection = 1;
            else if (selection == 3 && p1.health <= 0) selection = 2;
            else if (selection == 4 && p2.health > 0) selection = 2;
            else if (selection == 4 && p2.health <= 0) selection = 1;
            selectingAllyArrows(selection);
        }


        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
        {
            beingDefended = selection;
            chooseDefendText.gameObject.SetActive(false);
            party1Arrow.gameObject.SetActive(false);
            party2Arrow.gameObject.SetActive(false);
            party3Arrow.gameObject.SetActive(false);
            party4Arrow.gameObject.SetActive(false);

            if(defender == 1)
            {
                p1.defMultiplier += 20;
                if(p1.defMultiplier > 100)
                {
                    p1.defMultiplier = 100;
                }
            }
            else if(defender == 2)
            {
                p2.defMultiplier += 20;
                if (p2.defMultiplier > 100)
                {
                    p2.defMultiplier = 100;
                }
            }
            else if (defender == 3)
            {
                p3.defMultiplier += 20;
                if (p3.defMultiplier > 100)
                {
                    p3.defMultiplier = 100;
                }
            }
            else if (defender == 4)
            {
                p4.defMultiplier += 20;
                if (p4.defMultiplier > 100)
                {
                    p4.defMultiplier = 100;
                }
            }
            yield return new WaitForSeconds(.5f);////////////////////////////// ANIMATION IS ALLOWED TO SIMMER HERE
            isSelectingAlly = false;
            updatePlayerHealth(); //player sprites get reset back to base form here
            resetMenu();
            incrementTurn();
            //checkForEndOfBattle();
        }
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            menuArrowTemp = currentMenuArrow;
            Debug.Log("back out");
            party1Arrow.gameObject.SetActive(false);
            party2Arrow.gameObject.SetActive(false);
            party3Arrow.gameObject.SetActive(false);
            party4Arrow.gameObject.SetActive(false);
            resetMenu();
            isSelectingAlly = false;
            chooseDefendText.gameObject.SetActive(false);
            UpdatePartyArrow();
        }
    }
    
    public void selectingAllyArrows(int selection)
    {
        if(selection == 1)
        {
            party1Arrow.SetActive(true);
            party2Arrow.SetActive(false);
            party3Arrow.SetActive(false);
            party4Arrow.SetActive(false);
        }
        else if (selection == 2)
        {
            party1Arrow.SetActive(false);
            party2Arrow.SetActive(true);
            party3Arrow.SetActive(false);
            party4Arrow.SetActive(false);
        }
        else if (selection == 3)
        {
            party1Arrow.SetActive(false);
            party2Arrow.SetActive(false);
            party3Arrow.SetActive(true);
            party4Arrow.SetActive(false);
        }
        else if (selection == 4)
        {
            party1Arrow.SetActive(false);
            party2Arrow.SetActive(false);
            party3Arrow.SetActive(false);
            party4Arrow.SetActive(true);
        }
    }
    
    
    public void protect()
    {
        menuArrowTemp = currentMenuArrow;
        resetMenu();
        BattlePlayerScript p1 = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p2 = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p3 = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript p4 = player4.GetComponent<BattlePlayerScript>();
        if (playerTurn == 1)
        {
            p1.defMultiplier += 50;
        }
        else if (playerTurn == 2)
        {
            p2.defMultiplier += 50;
        }
        else if (playerTurn == 3)
        {
            p3.defMultiplier += 50;
        }
        else if (playerTurn == 4)
        {
            p4.defMultiplier += 50;
        }
        incrementTurn();
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
            isAttacking = true;
            ExecuteAttack();
            resetMenu();
            isSelectingEnemy = false;
            updateEnemyHealth();
            enemy1Arrow.gameObject.SetActive(false);
            enemy2Arrow.gameObject.SetActive(false);
            enemy3Arrow.gameObject.SetActive(false);
            enemy4Arrow.gameObject.SetActive(false);

            waiting = true;
            yield return new WaitForSeconds(0.5f);////////////////////////////// ANIMATION IS ALLOWED TO SIMMER HERE
            updatePlayerHealth(); //player sprites get reset back to base form here
            incrementTurn();
            resetMenu();
            waiting = false;
            isAttacking = false;
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

    private void postBattleReport()
    {
        EXPArea.SetActive(true);
        LUArea.SetActive(false);
        XButton.SetActive(false);
        int currentLevel = characterStatHandler.partyLevel;
        postReportObj.SetActive(true);
        if (enemies[0] != null || enemies.Count > 0)
        {
            line1Name.text = enemies[0].enemyName;
            line1EXP.text = (enemies[0].baseExpValue.ToString() + " EXP");
        }
        if (enemies[1] != null || enemies.Count > 1)
        {
            line2Name.text = enemies[1].enemyName;
            line2EXP.text = (enemies[1].baseExpValue.ToString() + " EXP");
        }
        if (enemies[2] != null || enemies.Count > 2)
        {
            line3Name.text = enemies[2].enemyName;
            line3EXP.text = (enemies[2].baseExpValue.ToString() + " EXP");
        }
        if (enemies[3] != null || enemies.Count > 3)
        {
            line4Name.text = enemies[3].enemyName;
            line4EXP.text = (enemies[3].baseExpValue.ToString() + " EXP");
        }
        line5Total.text = (expToGive.ToString());
        currentLVL.text = characterStatHandler.partyLevel.ToString();
        if (characterStatHandler.partyLevel < characterStatHandler.levelMax)
        {
            nextLVL.text = (characterStatHandler.partyLevel + 1).ToString();
            characterStatHandler.addEXP(expToGive);
            if (currentLevel == characterStatHandler.partyLevel)
            {
                expToNextTXT.text = ((characterStatHandler.expToNext - characterStatHandler.partyEXP).ToString() + " TO NEXT LEVEL!");
                expBarInner.fillAmount = characterStatHandler.partyEXP / characterStatHandler.expToNext;
                XButton.SetActive(true);
            }
            else
            {
                expToNextTXT.text = ("LEVEL " + characterStatHandler.partyLevel.ToString() + " REACHED!");
                expBarInner.fillAmount = 1;
                LevelUpButton.gameObject.SetActive(true);
            }
        }
        else if (characterStatHandler.partyLevel >= characterStatHandler.levelMax)
        {
            expToNextTXT.text = "Max Level";
            XButton.SetActive(true);
        }
    }
    public void postReportLevelUp()
    {
        EXPArea.SetActive(false);
        LUArea.SetActive(true);

        LevelReached.text = ("LEVEL " + characterStatHandler.partyLevel.ToString());

        //Show John's Stats
        LUJohnHP.text = ("HP: " + (john.characterHP).ToString() + " (+5)");
        LUJohnBash.text = ("BASH: " + ((int)(john.bashMultiplier * 100)).ToString() + "% (+10%)");
        LUJohnSlash.text = ("SLASH: " + ((int)(john.slashMultiplier * 100)).ToString() + "% (+10%)");
        LUJohnPoke.text = ("POKE: " + ((int)(john.pokeMultiplier * 100)).ToString() + "% (+10%)");
        if (johnMovement.bobActive)
        {
            //Set Bob's area to active and show stats
            LUBob.SetActive(true);
            LUBobHP.text = ("HP: " + (bob.characterHP).ToString() + " (+5)");
            LUBobBash.text = ("BASH: " + ((int)(bob.bashMultiplier * 100)).ToString() + "% (+13%)");
            LUBobSlash.text = ("SLASH: " + ((int)(bob.slashMultiplier * 100)).ToString() + "% (+10%)");
            LUBobPoke.text = ("POKE: " + ((int)(bob.pokeMultiplier * 100)).ToString() + "% (+7%)");
        }
        if (johnMovement.thozosActive)
        {
            //Set Thozos's area to active and show stats
            LUThozos.SetActive(true);
            LUThozosHP.text = ("HP: " + (thozos.characterHP).ToString() + " (+5)");
            LUThozosBash.text = ("BASH: " + ((int)(thozos.bashMultiplier * 100)).ToString() + "% (+7%)");
            LUThozosSlash.text = ("SLASH: " + ((int)(thozos.slashMultiplier * 100)).ToString() + "% (+7%)");
            LUThozosPoke.text = ("POKE: " + ((int)(thozos.pokeMultiplier * 100)).ToString() + "% (+7%)");
        }
        if (johnMovement.janetActive)
        {
            //Set Janet's area to active and show stats
            LUJanet.SetActive(true);
            LUJanetHP.text = ("HP: " + (janet.characterHP).ToString() + " (+5)");
            LUJanetBash.text = ("BASH: " + ((int)(janet.bashMultiplier * 100)).ToString() + "% (+10%)");
            LUJanetSlash.text = ("SLASH: " + ((int)(janet.slashMultiplier * 100)).ToString() + "% (+7%)");
            LUJanetPoke.text = ("POKE: " + ((int)(janet.pokeMultiplier * 100)).ToString() + "% (+13%)");
        }
        if (johnMovement.stephvenActive)
        {
            //Set Stevphen's area to active and show stats
            LUStevphen.SetActive(true);
            LUStevphenHP.text = ("HP: " + (stephven.characterHP).ToString() + " (+5)");
            LUStevphenBash.text = ("BASH: " + ((int)(stephven.bashMultiplier * 100)).ToString() + "% (+7%)");
            LUStevphenSlash.text = ("SLASH: " + ((int)(stephven.slashMultiplier * 100)).ToString() + "% (+13%)");
            LUStevphenPoke.text = ("POKE: " + ((int)(stephven.pokeMultiplier * 100)).ToString() + "% (+10%)");
        }

        //Check whether the player is at the max level or not
        if (characterStatHandler.partyLevel == characterStatHandler.levelMax)
        {
            //If so, display MAX LEVEL for exp to next level
            LUExpToNext.text = ("MAX LEVEL");
        }
        else
        {
            //Otherwise, display the amount of EXP needed to reach the next level
            LUExpToNext.text = (characterStatHandler.expToNext.ToString());
        }
        XButton.SetActive(true);
    }

    public void postReportClose()
    {
        postReportObj.SetActive(false);
        StartCoroutine(exitBattle());
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
        string readoutDamage = string.Empty;


        Transform textHolder = null;
        if (selectedEnemy == 1) textHolder = enemy1.transform.Find("textHolder(e1)");
        else if (selectedEnemy == 2) textHolder = enemy2.transform.Find("textHolder(e2)");
        else if (selectedEnemy == 3) textHolder = enemy3.transform.Find("textHolder(e3)");
        else if (selectedEnemy == 4) textHolder = enemy4.transform.Find("textHolder(e4)");



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
            Image playerImage = player1.GetComponent<Image>();
            playerImage.sprite = p1.attackSprite;
            RectTransform rt = playerImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
            attacker = p1;
        }
        else if(currentAttacker == 2)
        {
            Image playerImage = player2.GetComponent<Image>();
            playerImage.sprite = p2.attackSprite;
            RectTransform rt = playerImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
            attacker = p2;
        }
        else if(currentAttacker == 3)
        {
            Image playerImage = player3.GetComponent<Image>();
            playerImage.sprite = p3.attackSprite;
            RectTransform rt = playerImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
            attacker = p3;
        }
        else if (currentAttacker == 4)
        {
            Image playerImage = player4.GetComponent<Image>();
            playerImage.sprite = p4.attackSprite;
            RectTransform rt = playerImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(playerImage.sprite.bounds.size.x * 1f, playerImage.sprite.bounds.size.y * 1f);
            attacker = p4;
        }
        attackerName = attacker.characterName;
        ///////////////////////////////////
        if (selectedEnemyScript != null)
        {
            if(currentAttack == 'b') //bash
            {
                int damage = (int)(attackPower * attacker.bashMultiplier * selectedEnemyScript.bashMultiplier);
                int accuracyCheck = Random.Range(0, 100);


                if ((attacker.accuracy - selectedEnemyScript.evasiveness) < accuracyCheck) //if miss
                {
                    damage = 0;
                    readoutDamage = damage.ToString();
                    attackDesc = "missed";
                    enemyName = selectedEnemyScript.enemyName;


                    textHolder = GameObject.Find("textHolder(e" + selectedEnemy + ")")?.transform;
                    ShowFloatingText("Miss!", textHolder.position, textHolder);
                    selectedEnemyScript.health -= damage;

                }
                else
                {
                    readoutDamage = damage.ToString();
                    
                    attackDesc = "bashed";
                    enemyName = selectedEnemyScript.enemyName;

                    textHolder = GameObject.Find("textHolder(e" + selectedEnemy + ")")?.transform;
                    ShowFloatingText(damage.ToString(), textHolder.position, textHolder);
                    selectedEnemyScript.health -= damage;
                }

            }
            else if(currentAttack == 's') //slash
            {
                attackDesc = "slashed";
                int damage = (int)(attackPower * attacker.slashMultiplier * selectedEnemyScript.slashMultiplier);

                readoutDamage = damage.ToString();
                int accuracyCheck = Random.Range(0, 100);
                int attack1damage = damage;
                int attack2damage = damage;

                BattleEnemyScript enemy1Script = null;
                BattleEnemyScript enemy2Script = null;

                if (selectedEnemy == 1 || selectedEnemy == 2)
                {
                    enemy1Script = e1;
                    enemy2Script = e2;
                }
                else if (selectedEnemy == 3 || selectedEnemy == 4)
                {
                    enemy1Script = e3;
                    enemy2Script = e4;
                }

                if (selectedEnemy == 1 || selectedEnemy == 2)
                {
                    if ((attacker.accuracy - e1.evasiveness) < accuracyCheck) //miss
                    {
                        attack1damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);
                    }
                    else //hit
                    {
                        e1.health -= attack1damage;
                        textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);
                    }

                    if ((attacker.accuracy - e2.evasiveness) < accuracyCheck) //miss
                    {
                        attack2damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 2 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);

                    }
                    else //hit
                    {
                        e2.health -= attack2damage;
                        textHolder = GameObject.Find("textHolder(e" + 2 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);

                    }

                    readoutDamage = attack1damage.ToString() + " / " + attack2damage.ToString();
                    enemyName = ($"{e1.enemyName} and {e2.enemyName}");

                }
                else if (selectedEnemy == 3 || selectedEnemy == 4)
                {
                    if ((attacker.accuracy - e3.evasiveness) < accuracyCheck) //miss
                    {
                        attack1damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 3 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);

                    }
                    else //hit
                    {
                        e3.health -= attack1damage;
                        textHolder = GameObject.Find("textHolder(e" + 3 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);

                    }

                    if ((attacker.accuracy - e4.evasiveness) < accuracyCheck) //miss
                    {
                        attack2damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 4 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);

                    }
                    else //hit
                    {
                        e4.health -= attack2damage;
                        textHolder = GameObject.Find("textHolder(e" + 4 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);
                    }

                    readoutDamage = attack1damage.ToString() + " / " + attack2damage.ToString();
                    enemyName = ($"{e3.enemyName} and {e4.enemyName}");
                }
            }
            else if(currentAttack == 'p') //poke
            {
                attackDesc = "poked";
                int damage = (int)(attackPower * attacker.pokeMultiplier * selectedEnemyScript.pokeMultiplier);
                readoutDamage = damage.ToString();
                int accuracyCheck = Random.Range(0, 100);
                int attack1damage = damage;
                int attack2damage = damage;

                if (selectedEnemy == 1 || selectedEnemy == 3)
                {
                    if ((attacker.accuracy - e1.evasiveness) < accuracyCheck) //miss
                    {
                        attack1damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);

                    }
                    else //hit
                    {
                        e1.health -= attack1damage;
                        textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);
                    }

                    if ((attacker.accuracy - e3.evasiveness) < accuracyCheck) //miss
                    {
                        attack2damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 3 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);
                    }
                    else //hit
                    {
                        e3.health -= attack2damage;
                        textHolder = GameObject.Find("textHolder(e" + 3 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);
                    }

                    readoutDamage = attack1damage.ToString() + " / " + attack2damage.ToString();
                    enemyName = ($"{e1.enemyName} and {e3.enemyName}");
                }
                else if (selectedEnemy == 2 || selectedEnemy == 4)
                {
                    if ((attacker.accuracy - e2.evasiveness) < accuracyCheck) //miss
                    {
                        attack1damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 2 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);
                    }
                    else //hit
                    {
                        e2.health -= attack1damage;
                        textHolder = GameObject.Find("textHolder(e" + 2 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);

                    }

                    if ((attacker.accuracy - e4.evasiveness) < accuracyCheck) //miss
                    {
                        attack2damage = 0;
                        attackDesc = "missed";
                        textHolder = GameObject.Find("textHolder(e" + 4 + ")")?.transform;
                        ShowFloatingText("Miss!", textHolder.position, textHolder);
                    }
                    else //hit
                    {
                        e4.health -= attack2damage;
                        textHolder = GameObject.Find("textHolder(e" + 4 + ")")?.transform;
                        ShowFloatingText(damage.ToString(), textHolder.position, textHolder);
                    }

                    readoutDamage = attack1damage.ToString() + " / " + attack2damage.ToString();
                    enemyName = ($"{e2.enemyName} and {e4.enemyName}");
                }
            }
            else if(currentAttack == 'r') //rock
            {
                readoutDamage = attackPower.ToString();
                enemyName = selectedEnemyScript.enemyName;
                //selectedEnemyScript.health -= attackPower;
                int accuracyCheck = Random.Range(0, 100);
                if ((attacker.accuracy - selectedEnemyScript.evasiveness) < accuracyCheck) //if miss
                {
                    attackPower = 0;
                    readoutDamage = attackPower.ToString();
                    selectedEnemyScript.health -= attackPower;
                    attackDesc = "missed";
                    enemyName = selectedEnemyScript.enemyName;


                    if (selectedEnemy == 1) textHolder = enemy1.transform.Find("textHolder(e1)");
                    else if (selectedEnemy == 2) textHolder = enemy2.transform.Find("textHolder(e2)");
                    else if (selectedEnemy == 3) textHolder = enemy3.transform.Find("textHolder(e3)");
                    else if (selectedEnemy == 4) textHolder = enemy4.transform.Find("textHolder(e4)");

                    ShowFloatingText("Miss!", textHolder.position, textHolder);
                }
                else
                {
                    readoutDamage = attackPower.ToString();
                    selectedEnemyScript.health -= attackPower;
                    attackDesc = "threw a rock at";
                    enemyName = selectedEnemyScript.enemyName;


                    if (selectedEnemy == 1) textHolder = enemy1.transform.Find("textHolder(e1)");
                    else if (selectedEnemy == 2) textHolder = enemy2.transform.Find("textHolder(e2)");
                    else if (selectedEnemy == 3) textHolder = enemy3.transform.Find("textHolder(e3)");
                    else if (selectedEnemy == 4) textHolder = enemy4.transform.Find("textHolder(e4)");

                    textHolder = GameObject.Find("textHolder(e" + 1 + ")")?.transform;
                    ShowFloatingText(attackPower.ToString(), textHolder.position, textHolder);
                }
            }
            currentAttack = 'n';
            soundManager.PlaySoundClip(6);


            /*if (attackPower > 0)
            {
                ShowFloatingText(attackPower.ToString(), textHolder.position, textHolder);
            }*/

            updateBattleLog($"{attackerName} {attackDesc} {enemyName}! ({readoutDamage} HP)");
            //Debug.Log("Attacked enemy " + currentEnemy + ", remaining health: " + selectedEnemyScript.health); //BATTLE NARRATION
        }
    }
    public void ShowFloatingText(string message, Vector3 position, Transform parent)
    {
        GameObject floatingTextPrefab = Resources.Load<GameObject>("damageTMP");
        GameObject floatingText = Instantiate(floatingTextPrefab, position, Quaternion.identity, parent);

        // Get all TMP components in children
        TextMeshProUGUI[] textComponents = floatingText.GetComponentsInChildren<TextMeshProUGUI>();

        if (textComponents.Length > 0)
        {
            foreach (var tmp in textComponents)
            {
                tmp.text = message; // Apply message to all TMP objects
            }
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI components found in the instantiated prefab!");
        }
    }


    public void updateEnemyHealth()
    {
        BattleEnemyScript currentEnemy = null;


        if (enemy1.activeSelf)
        {
            currentEnemy = enemy1.GetComponent<BattleEnemyScript>();
            RectTransform enemyHP1 = enemyHPBar1.GetComponent<RectTransform>();
            if (currentEnemy.health <= 0)
            {
                updateBattleLog($"{currentEnemy.enemyName} was defeated! ({currentEnemy.baseExpValue} XP)");
                enemy1.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
            }
            enemyHP1.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP1.sizeDelta.y);
        }

        if (enemy2.activeSelf)
        {
            currentEnemy = enemy2.GetComponent<BattleEnemyScript>();
            RectTransform enemyHP2 = enemyHPBar2.GetComponent<RectTransform>();
            if (currentEnemy.health <= 0)
            {
                updateBattleLog($"{currentEnemy.enemyName} was defeated! ({currentEnemy.baseExpValue} XP)");
                enemy2.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
            }
            enemyHP2.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP2.sizeDelta.y);
        }

        if (enemy3.activeSelf)
        {
            currentEnemy = enemy3.GetComponent<BattleEnemyScript>();
            RectTransform enemyHP3 = enemyHPBar3.GetComponent<RectTransform>();
            if (currentEnemy.health <= 0)
            {
                updateBattleLog($"{currentEnemy.enemyName} was defeated! ({currentEnemy.baseExpValue} XP)");
                enemy3.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
            }
            enemyHP3.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP3.sizeDelta.y);
        }
        
        if (enemy4.activeSelf)
        {
            currentEnemy = enemy4.GetComponent<BattleEnemyScript>();
            RectTransform enemyHP4 = enemyHPBar4.GetComponent<RectTransform>();
            if (currentEnemy.health <= 0)
            {
                updateBattleLog($"{currentEnemy.enemyName} was defeated! ({currentEnemy.baseExpValue} XP)");
                enemy4.gameObject.SetActive(false); //probably want to put a fade and sound effect here on enemy death
            }
            enemyHP4.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP4.sizeDelta.y);
        }

    }

    public void updatePlayerHealth()
    {
        BattlePlayerScript currentPlayer = null;

        BattlePlayerScript player1sprite = player1.GetComponent<BattlePlayerScript>();
        BattlePlayerScript player2sprite = player2.GetComponent<BattlePlayerScript>();
        BattlePlayerScript player3sprite = player3.GetComponent<BattlePlayerScript>();
        BattlePlayerScript player4sprite = player4.GetComponent<BattlePlayerScript>();

        currentPlayer = player1.GetComponent<BattlePlayerScript>();
        RectTransform playerHP1 = playerHPBar1.GetComponent<RectTransform>();

        Image p1 = player1.GetComponent<Image>();
        p1.sprite = player1sprite.characterSprite;
        RectTransform rt = player1.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(p1.sprite.bounds.size.x * 16f, p1.sprite.bounds.size.y * 16f);

        string name1 = p1.sprite.name;
        if (currentPlayer.health <= 0)
        {
            p1.sprite = currentPlayer.downedSprite;
            rt.sizeDelta = new Vector2(p1.sprite.bounds.size.x * 16f, p1.sprite.bounds.size.y * 16f);
            if (char1Dead == false)
            {
                updateBattleLog($"{currentPlayer.characterName} is unconscious!");
                char1Dead = true;
            }
        }
        else
        {
            p1.sprite = currentPlayer.characterSprite;
        }
        playerHP1.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP1.sizeDelta.y);

        if (player2.active)
        {
            currentPlayer = player2.GetComponent<BattlePlayerScript>();
            RectTransform playerHP2 = playerHPBar2.GetComponent<RectTransform>();
            Image p2 = player2.GetComponent<Image>();
            p2.sprite = player2sprite.characterSprite;
            RectTransform rt2 = player2.GetComponent<RectTransform>();
            rt2.sizeDelta = new Vector2(p2.sprite.bounds.size.x * 16f, p2.sprite.bounds.size.y * 16f);
            string name2 = p2.sprite.name;
            if (currentPlayer.health <= 0)
            {
                p2.sprite = currentPlayer.downedSprite;
                rt2.sizeDelta = new Vector2(p2.sprite.bounds.size.x * 16f, p2.sprite.bounds.size.y * 16f);
                if (char2Dead == false)
                {
                    updateBattleLog($"{currentPlayer.characterName} is unconscious!");
                    char2Dead = true;
                }
            }
            else
            {
                p2.sprite = currentPlayer.characterSprite;
            }
            playerHP2.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP2.sizeDelta.y);
        }

        if (player3.active)
        {
            currentPlayer = player3.GetComponent<BattlePlayerScript>();
            RectTransform playerHP3 = playerHPBar3.GetComponent<RectTransform>();
            Image p3 = player3.GetComponent<Image>();
            p3.sprite = player3sprite.characterSprite;
            RectTransform rt3 = player3.GetComponent<RectTransform>();
            rt3.sizeDelta = new Vector2(48, 48); //(p3.sprite.bounds.size.x * 1f, p3.sprite.bounds.size.y * 1f);
            string name = p3.sprite.name;
            if (currentPlayer.health <= 0)
            {
                p3.sprite = currentPlayer.downedSprite;
                rt3.sizeDelta = new Vector2(48, 48); //(p3.sprite.bounds.size.x * 1f, p3.sprite.bounds.size.y * 1f);
                if (char3Dead == false)
                {
                    updateBattleLog($"{currentPlayer.characterName} is unconscious!");
                    char3Dead = true;
                }
            }
            else
            {
                p3.sprite = currentPlayer.characterSprite;
            }
            playerHP3.sizeDelta = new Vector2((currentPlayer.health / currentPlayer.startingHealth) * 180, playerHP3.sizeDelta.y);
        }

        if (player4.active)
        {
            currentPlayer = player4.GetComponent<BattlePlayerScript>();
            RectTransform playerHP4 = playerHPBar4.GetComponent<RectTransform>();
            Image p4 = player4.GetComponent<Image>();
            p4.sprite = player4sprite.characterSprite;
            RectTransform rt4 = player4.GetComponent<RectTransform>();
            rt4.sizeDelta = new Vector2(p4.sprite.bounds.size.x * 16f, p4.sprite.bounds.size.y * 16f);
            string name = p4.sprite.name;
            if (currentPlayer.health <= 0)
            {
                p4.sprite = currentPlayer.downedSprite;
                rt4.sizeDelta = new Vector2(p4.sprite.bounds.size.x * 16f, p4.sprite.bounds.size.y * 16f);
                if (char4Dead == false)
                {
                    updateBattleLog($"{currentPlayer.characterName} is unconscious!");
                    char4Dead = true;
                }
            }
            else
            {
                p4.sprite = currentPlayer.characterSprite;
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
            SceneManager.LoadScene("game");
            dataPersistenceManager.LoadGame();
            Debug.Log("all players dead");
        }


        if (e1.health <= 0 && e2.health <= 0 && e3.health <= 0 && e4.health <= 0) //battle won
        {
            isBattleOver = true;
            fleeChance = 100;
        }
    }

    public void fleeButtonClick()
    {
        if(Random.Range(0,100) > fleeChance)
        {
            resetMenu();
            incrementTurn();
            updateBattleLog("The party failed to escape!");
            currentMenuArrow = 1;
            updateMenuArrows();
        }
        else
        {
            fleeChance = fleeChance - 10;
            if(fleeChance < 0)
            {
                fleeChance = 0;
            }
            fled = true;
            StartCoroutine(exitBattle());
        }
    }

    public IEnumerator exitBattle()
    {
        Debug.Log("exiting battle");
        isBattleOver = false;
        battleLog.Clear();
        battleLog = new List<string> { "", "", "", "", "", "", "", "", "", "", "", "" };
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

        //Checks to make sure the player didn't flee the fight
        if (!fled)
        {
            //Gives EXP to player.
        }
        //Reset Fled Flag
        fled = false;

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
            case 4:
                if (!bigReport.activeSelf)
                {
                    soundManager.PlaySoundClip(0);
                    atkMenu.SetActive(false);
                    defMenu.SetActive(false);
                    invMenu.SetActive(false);
                    runMenu.SetActive(false);
                    bigReport.SetActive(true);
                    updateBigBattleLog();
                }
                else
                {
                    soundManager.PlaySoundClip(4);
                    bigReport.SetActive(false);
                }
                break;
        }
    }

    public void bigReportClose()
    {
        openMenu(4);
        StartCoroutine(SelectionWaitCoroutine(0.2f));
        canSelect = false;
        resetMenu();
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
