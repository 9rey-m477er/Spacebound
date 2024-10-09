using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIScript : MonoBehaviour
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

    public GameObject enemy1Arrow;
    public GameObject enemy2Arrow;
    public GameObject enemy3Arrow;
    public GameObject enemy4Arrow;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public BattleEnemyScript battleEnemyScript;

    public bool isSelectingEnemy = false;
    public int currentEnemy = 1;
    public int attackPower;
    public bool isBattleOver = false;

    public GameObject enemyHPBar1;
    public GameObject enemyHPBar2;
    public GameObject enemyHPBar3;
    public GameObject enemyHPBar4;

    public Image fadeImage;
    public float fadeDuration = 0.3f;

    private OmniDirectionalMovement johnMovement;
    private SoundManager soundManager;
    void OnEnable()
    {
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
    
    public IEnumerator fadeIntoBattle()
    {
        Debug.Log("fade in");
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));
        yield return StartCoroutine(Fade(0));
        fadeImage.gameObject.SetActive(false);
        Debug.Log("fade out");
    }

    public void incrementTurn()
    {
        playerTurn++;
        if (playerTurn == 5)
        {
            playerTurn = 1;
        }

        UpdatePartyArrow();
    }

    private void UpdatePartyArrow()
    {
        party1Arrow.gameObject.SetActive(playerTurn == 1);
        party2Arrow.gameObject.SetActive(playerTurn == 2);
        party3Arrow.gameObject.SetActive(playerTurn == 3);
        party4Arrow.gameObject.SetActive(playerTurn == 4);
    }
    public void attackSlot1()
    {
        resetMenu();
        soundManager.PlaySoundClip(5);
        isSelectingEnemy = true;
        attackPower = 25;
        currentEnemy = 1;
        UpdateEnemyArrows();
    }

    private void HandleEnemySelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 1) currentEnemy = 2;
            else if (currentEnemy == 3) currentEnemy = 4;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 2) currentEnemy = 1;
            else if (currentEnemy == 4) currentEnemy = 3;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 3) currentEnemy = 1;
            else if (currentEnemy == 4) currentEnemy = 2;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            soundManager.PlaySoundClip(3);
            if (currentEnemy == 1) currentEnemy = 3;
            else if (currentEnemy == 2) currentEnemy = 4;
            UpdateEnemyArrows();
        }

        if (Input.GetKeyDown(KeyCode.E))//confirm
        {
            isSelectingEnemy = false; 
            ExecuteAttack();
            checkForEndOfBattle();
        }
    }

    private void ExecuteAttack()
    {
        BattleEnemyScript selectedEnemyScript = null;

        switch (currentEnemy)
        {
            case 1:
                selectedEnemyScript = enemy1.GetComponent<BattleEnemyScript>();
                break;
            case 2:
                selectedEnemyScript = enemy2.GetComponent<BattleEnemyScript>();
                break;
            case 3:
                selectedEnemyScript = enemy3.GetComponent<BattleEnemyScript>();
                break;
            case 4:
                selectedEnemyScript = enemy4.GetComponent<BattleEnemyScript>();
                break;
        }

        if (selectedEnemyScript != null)
        {
            selectedEnemyScript.health -= attackPower;
            soundManager.PlaySoundClip(6);
            Debug.Log("Attacked enemy " + currentEnemy + ", remaining health: " + selectedEnemyScript.health);
        }
        updateEnemyHealth();
        enemy1Arrow.gameObject.SetActive(false);
        enemy2Arrow.gameObject.SetActive(false);
        enemy3Arrow.gameObject.SetActive(false);
        enemy4Arrow.gameObject.SetActive(false);
        incrementTurn();
    }

    public void updateEnemyHealth()
    {
        BattleEnemyScript currentEnemy = null;

        currentEnemy = enemy1.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP1 = enemyHPBar1.GetComponent<RectTransform>();
        enemyHP1.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP1.sizeDelta.y);

        currentEnemy = enemy2.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP2 = enemyHPBar2.GetComponent<RectTransform>();
        enemyHP2.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP2.sizeDelta.y);

        currentEnemy = enemy3.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP3 = enemyHPBar3.GetComponent<RectTransform>();
        enemyHP3.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP3.sizeDelta.y);

        currentEnemy = enemy4.GetComponent<BattleEnemyScript>();
        RectTransform enemyHP4 = enemyHPBar4.GetComponent<RectTransform>();
        enemyHP4.sizeDelta = new Vector2((currentEnemy.health / currentEnemy.startingHealth) * 180, enemyHP4.sizeDelta.y);

    }

    public void checkForEndOfBattle()
    {
        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>(); 
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        if(e1.health <= 0 && e2.health <= 0 && e3.health <= 0 && e4.health <= 0)
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
        fadeImage.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1));


        BattleEnemyScript e1 = enemy1.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e2 = enemy2.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e3 = enemy3.GetComponent<BattleEnemyScript>();
        BattleEnemyScript e4 = enemy4.GetComponent<BattleEnemyScript>();

        e1.health = e1.startingHealth;
        e2.health = e2.startingHealth;
        e3.health = e3.startingHealth;
        e4.health = e4.startingHealth;
        updateEnemyHealth();
        fadeImage.gameObject.SetActive(false);
        johnMovement.inBattle = false;
        battleSystem.gameObject.SetActive(false); //last
    }

    private void UpdateEnemyArrows()
    {
        enemy1Arrow.gameObject.SetActive(currentEnemy == 1);
        enemy2Arrow.gameObject.SetActive(currentEnemy == 2);
        enemy3Arrow.gameObject.SetActive(currentEnemy == 3);
        enemy4Arrow.gameObject.SetActive(currentEnemy == 4);
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

}
