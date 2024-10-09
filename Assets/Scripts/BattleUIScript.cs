using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIScript : MonoBehaviour
{
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


    public GameObject enemyHPBar1;
    public GameObject enemyHPBar2;
    public GameObject enemyHPBar3;
    public GameObject enemyHPBar4;
    void Start()
    {
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
        isSelectingEnemy = true;
        attackPower = 25;
        currentEnemy = 1;
        UpdateEnemyArrows();
    }

    private void HandleEnemySelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (currentEnemy == 1) currentEnemy = 2;
            else if (currentEnemy == 3) currentEnemy = 4;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (currentEnemy == 2) currentEnemy = 1;
            else if (currentEnemy == 4) currentEnemy = 3;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentEnemy == 3) currentEnemy = 1;
            else if (currentEnemy == 4) currentEnemy = 2;
            UpdateEnemyArrows();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentEnemy == 1) currentEnemy = 3;
            else if (currentEnemy == 2) currentEnemy = 4;
            UpdateEnemyArrows();
        }

        if (Input.GetKeyDown(KeyCode.E))//confirm
        {
            isSelectingEnemy = false; 
            ExecuteAttack();
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

    private void UpdateEnemyArrows()
    {
        enemy1Arrow.gameObject.SetActive(currentEnemy == 1);
        enemy2Arrow.gameObject.SetActive(currentEnemy == 2);
        enemy3Arrow.gameObject.SetActive(currentEnemy == 3);
        enemy4Arrow.gameObject.SetActive(currentEnemy == 4);
    }



public void openMenu(int menu)
    { //need a way to reset menu after an action is taken
        switch(menu)
        {
            case 0:
                if (!atkMenu.activeSelf)
                {
                    atkMenu.SetActive(true);
                    defMenu.SetActive(false);
                    invMenu.SetActive(false);
                    runMenu.SetActive(false);
                }
                else
                {
                    atkMenu.SetActive(false);
                }
                break;
            case 1:
                if (!defMenu.activeSelf)
                {
                    atkMenu.SetActive(false);
                    defMenu.SetActive(true);
                    invMenu.SetActive(false);
                    runMenu.SetActive(false);
                }
                else
                {
                    defMenu.SetActive(false);
                }
                break;
            case 2:
                if (!invMenu.activeSelf)
                {
                    atkMenu.SetActive(false);
                    defMenu.SetActive(false);
                    invMenu.SetActive(true);
                    runMenu.SetActive(false);
                }
                else
                {
                    invMenu.SetActive(false);
                }
                break;
            case 3:
                if (!runMenu.activeSelf)
                {
                    atkMenu.SetActive(false);
                    defMenu.SetActive(false);
                    invMenu.SetActive(false);
                    runMenu.SetActive(true);
                }
                else
                {
                    runMenu.SetActive(false);
                }
                break;
        }
    }
}
