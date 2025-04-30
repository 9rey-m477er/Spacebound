using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public Item[,] inventory = new Item[3,1]; //[type index, spec index] - healing items = [0][] - attack items [1][] - defense items [2][0] 
    public int money = 999; //999 for the sake of testing things, will change later
    public OverworldMenu overworldMenu;
    
    public Item phillycake;
    public Item rock;
    public Item paddle;
    
    public Button phillyView;
    public Button phillyUse;
    public Button rockView;
    public Button paddleView;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    
    public TextMeshProUGUI phillyAmount;
    public TextMeshProUGUI rockAmount;
    public TextMeshProUGUI paddleAmount;

    public Image[] UIarrows = new Image[4];
    public Button[] InvButtons = new Button[4];
    int pointer = 0;
    public void Start()
    {
        InitializeInventory();
        updateAmounts();
        
    }
    public void Update()
    {
        // Debug Tools

        //if (Input.GetKeyUp(KeyCode.Keypad0))
        //{
        //    buy(1, phillycake);
        //    Debug.Log("Bought a Philly Cake, remaining money = " + money);
        //}
        //if (Input.GetKeyUp(KeyCode.Keypad1))
        //{
        //    buy(1, rock);
        //    Debug.Log("Bought a rock, remaining money = " + money);
        //}
        //if (Input.GetKeyUp(KeyCode.Keypad2))
        //{
        //    buy(1, paddle);
        //    Debug.Log("Bought a Paddle.png, remaining money = " + money);
        //}
        //if (Input.GetKeyUp(KeyCode.Keypad3))
        //{
        //    sell(1, phillycake);
        //    Debug.Log("Sold a Philly Cake, current money = " + money);
        //}
        //if (Input.GetKeyUp(KeyCode.Keypad4))
        //{
        //    sell(1, rock);
        //    Debug.Log("Sold a rock, current money = " + money);
        //}
        //if (Input.GetKeyUp(KeyCode.Keypad5))
        //{
        //    sell(1, paddle);
        //    Debug.Log("Sold a Paddle.png, current money = " + money);
        //}
        if (overworldMenu.owMenuOpen && overworldMenu.menuOpen == 2)
        {
            NavigateInventory();
        }

    }
    public void NavigateInventory() //controls arrow selecting inventory buttons
    {
        Image currentArrow = UIarrows[pointer];
        if (Input.GetKeyUp(KeyCode.W)) //move arrow up
        {
            if (pointer <= 0)
            {
                pointer = UIarrows.Length;
            }
            else if(pointer == 3) //if pressing up on top most arrow, bring to bottom
            {
                pointer = 1;
            }
            else if(pointer == 2) //prevents arrow from going to use button if pressing down on philly cake view button
            {
                pointer = 0;
            }
            else
            pointer--;
        }
        if (Input.GetKeyUp(KeyCode.S)) //move arrow down
        {
            if(pointer == 1)
            {
                pointer = 3;
            }
            else
            {
                pointer++;
            }
            
            
            if(pointer >= UIarrows.Length) //if pressing down on bottom-most arrow, bring arrow back to the top
            {
                pointer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.D)) //controls moving the arrow horizontally for the use button
        {
            pointer = 2;
        }
        if (Input.GetKeyUp(KeyCode.A) && pointer == 2)
        {
            pointer = 1;
        }
        for (int i = 0; i < UIarrows.Length; i++)
        {
            UIarrows[i].gameObject.SetActive(false);
        }
        UIarrows[pointer].gameObject.SetActive(true);
        Debug.Log("Pointer = " + pointer);
        if (Input.GetKeyUp(KeyCode.E)) //view item description of corresponding item
        {
            InvButtons[pointer].GetComponent<ItemViewButtons>().describe();
        }
    }
    public void buy(int quantity, Item i) //buy items from shop
    {
        int cost = i.buyPrice * quantity;
        if(money >= cost)
        {
            i.amount += quantity;
            money -= cost;
        }
        updateAmounts();
    }
    public void sell(int quantity, Item i) //sell items from inventory
    {
        int profit = i.sellPrice * quantity;
        if (i.amount >= quantity)
        {
            i.amount -= quantity;
            money += profit;
        }
        updateAmounts();
    }

    public void addCredits(int amount) //gives player credits after battles
    {
        money += amount;
    }

    public void addItem(Item i, int amount) //item pickups
    {
        i.amount += amount;
        updateAmounts();
    }
    public void consume(Item i) //use items
    {
        i.amount--;
        updateAmounts();
    }

    public void updateAmounts() //update amounts on the inventory menu
    {
        rockAmount.text = rock.amount.ToString();
        phillyAmount.text = phillycake.amount.ToString();
        paddleAmount.text = paddle.amount.ToString();
    }

    public void resetText()
    {
        itemName.text = "";
        itemDescription.text = "Press view on an item to see its description. Navigate with W and S";
    }

    public void viewDescription(Item i)
    {
        itemName.text = i.name;
        itemDescription.text = i.description;
    }

    public void LoadData(GameData data) //loads inventory on game start
    {
        money = data.credits;
        rock.amount = data.rockAmt;
        phillycake.amount = data.philAmt;
        paddle.amount = data.paddAmt;
    }

    public void SaveData(ref GameData data) //saves inventory at save machine
    {
        data.credits = money;
        data.rockAmt = rock.amount;
        data.philAmt = phillycake.amount;
        data.paddAmt = paddle.amount;
    }
    public void InitializeInventory() //assigns item scriptable objects to inventory array
    {
        inventory[0, 0] = phillycake;
        inventory[1, 0] = rock;
        inventory[2, 0] = paddle;
    }
}
