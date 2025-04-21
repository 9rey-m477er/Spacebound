using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour, IDataPersistence
{
    public Item[,] inventory = new Item[3,1]; //[type index, spec index] - healing items = [0][] - attack items [1][] - defense items [2][0] 
    public int money = 999; //999 for the sake of testing things, will change later
    
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
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Keypad0))
        {
            buy(1, phillycake);
            Debug.Log("Bought a Philly Cake, remaining money = " + money);
        }
        if (Input.GetKeyUp(KeyCode.Keypad1))
        {
            buy(1, rock);
            Debug.Log("Bought a rock, remaining money = " + money);
        }
        if (Input.GetKeyUp(KeyCode.Keypad2))
        {
            buy(1, paddle);
            Debug.Log("Bought a Paddle.png, remaining money = " + money);
        }
        if (Input.GetKeyUp(KeyCode.Keypad3))
        {
            sell(1, phillycake);
            Debug.Log("Sold a Philly Cake, current money = " + money);
        }
        if (Input.GetKeyUp(KeyCode.Keypad4))
        {
            sell(1, rock);
            Debug.Log("Sold a rock, current money = " + money);
        }
        if (Input.GetKeyUp(KeyCode.Keypad5))
        {
            sell(1, paddle);
            Debug.Log("Sold a Paddle.png, current money = " + money);
        }
        NavigateInventory();
    }
    public void NavigateInventory()
    {
        Image currentArrow = UIarrows[pointer];
        if (Input.GetKeyUp(KeyCode.W))
        {
            if(pointer < 0)
            {
                pointer = UIarrows.Length-1;
            }
            pointer--;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            pointer++;
            
            if(pointer >= UIarrows.Length)
            {
                pointer = 0;
            }
        }
        for(int i = 0; i < UIarrows.Length; i++)
        {
            UIarrows[i].gameObject.SetActive(false);
        }
        UIarrows[pointer].gameObject.SetActive(true);
        Debug.Log("Pointer = " + pointer);
        if (Input.GetKeyUp(KeyCode.E))
        {
            InvButtons[pointer].GetComponent<ItemViewButtons>().describe();
        }
    }
    public void buy(int quantity, Item i)
    {
        int cost = i.buyPrice * quantity;
        if(money > cost)
        {
            i.amount += quantity;
            money -= cost;
        }
        updateAmounts();
    }
    public void sell(int quantity, Item i)
    {
        int profit = i.sellPrice * quantity;
        if (i.amount > quantity)
        {
            i.amount -= quantity;
            money += profit;
        }
        updateAmounts();
    }

    public void addCredits(int amount)
    {
        money += amount;
    }

    public void addItem(Item i)
    {
        i.amount++;
        updateAmounts();
    }
    public void consume(Item i)
    {
        i.amount--;
        updateAmounts();
    }

    public void updateAmounts()
    {
        rockAmount.text = rock.amount.ToString();
        phillyAmount.text = phillycake.amount.ToString();
        paddleAmount.text = paddle.amount.ToString();
    }

    public void viewDescription(Item i)
    {
        itemName.text = i.name;
        itemDescription.text = i.description;
    }

    public void LoadData(GameData data)
    {
        money = data.credits;
        rock.amount = data.rockAmt;
        phillycake.amount = data.philAmt;
        paddle.amount = data.paddAmt;
    }

    public void SaveData(ref GameData data)
    {
        data.credits = money;
        data.rockAmt = rock.amount;
        data.philAmt = phillycake.amount;
        data.paddAmt = paddle.amount;
    }
}
