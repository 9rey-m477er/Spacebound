using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public InventoryManager inventoryManager;
    public DialogueController dialogueController;
    public SoundManager soundManager;
    public GameObject MenuObject, ShopMenu, MainMenu, StatMenu; //, InvMenu;
    public OmniDirectionalMovement John;
    private Shop currentShop;
    private bool isShopOpen;

    public Image shopImage;
    public TextMeshProUGUI shopNameText;
    public TextMeshProUGUI shopItem1Name, shopItem1Price, shopItem1Amount;
    public TextMeshProUGUI shopItem2Name, shopItem2Price, shopItem2Amount;
    public TextMeshProUGUI shopItem3Name, shopItem3Price, shopItem3Amount;
    public TextMeshProUGUI moneyHeld;
    public GameObject Item1Arrow, Item2Arrow, Item3Arrow, BuyArrow, SellArrow,
        Item1SelectArrow, Item2SelectArrow, Item3SelectArrow;
    public int itemArrow;
    private bool isBuySell, onBuy;

    public Item selectedItem, item1, item2, item3;

    public bool devTools;
    public Shop demoShop;

    void Update()
    {
        if (devTools)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                OpenShop(demoShop);
            }
        }
        if (isShopOpen)
        {
            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                if (isBuySell)
                {
                    EndBuySell();
                }
                else
                {
                    CloseShop();
                }
            }
            if (!isBuySell)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    AdvanceArrow(true);
                    soundManager.PlaySoundClip(3);
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    AdvanceArrow(false);
                    soundManager.PlaySoundClip(3);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    onBuy = !onBuy;
                    BuyArrow.SetActive(onBuy);
                    SellArrow.SetActive(!onBuy);
                    soundManager.PlaySoundClip(3);
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (!isBuySell)
                {
                    StartBuySell();
                    soundManager.PlaySoundClip(5);
                }
                else
                {
                    if (onBuy)
                    {
                        if (inventoryManager.money >= selectedItem.buyPrice)
                        {
                            inventoryManager.buy(1, selectedItem);
                            soundManager.PlaySoundClip(1);
                            UpdateText();
                        }
                        else
                        {
                            soundManager.PlaySoundClip(2);
                        }
                    }
                    else
                    {
                        if (selectedItem.amount > 0)
                        {
                            inventoryManager.sell(1, selectedItem);
                            soundManager.PlaySoundClip(1);
                            UpdateText();
                        }
                        else
                        {
                            soundManager.PlaySoundClip(2);
                        }
                    }
                }
            }
        }
    }

    public void OpenShop(Shop shop)
    {
        John.menuOpen = true;
        soundManager.PlaySoundClip(0);
        //Save shop as currentShop
        currentShop = shop;
        //Load Basic Shop Info
        shopImage.sprite = shop.shopPortrait;
        shopNameText.text = shop.shopName;
        moneyHeld.text = "Credits Held: " + inventoryManager.money.ToString();

        //Load Item 1
        item1 = shop.shopInventory[0];
        shopItem1Name.text = item1.itemName;
        shopItem1Price.text = item1.buyPrice.ToString();
        shopItem1Amount.text = item1.amount.ToString();
        Item1Arrow.SetActive(true);

        if (shop.shopInventory.Count > 0)
        {
            //Load Item 2
            item2 = shop.shopInventory[1];
            shopItem2Name.text = item2.itemName;
            shopItem2Price.text = item2.buyPrice.ToString();
            shopItem2Amount.text = item2.amount.ToString();
        }

        if (shop.shopInventory.Count > 1)
        {
            //Load Item 3
            item3 = shop.shopInventory[2];
            shopItem3Name.text = item3.itemName;
            shopItem3Price.text = item3.buyPrice.ToString();
            shopItem3Amount.text = item3.amount.ToString();
        }

        itemArrow = 0;
        Item1Arrow.SetActive(true);
        Item2Arrow.SetActive(false);
        Item3Arrow.SetActive(false);
        Item1SelectArrow.SetActive(false);
        Item2SelectArrow.SetActive(false);
        Item3SelectArrow.SetActive(false);
        BuyArrow.SetActive(false);
        SellArrow.SetActive(false);

        MenuObject.SetActive(true);
        ShopMenu.SetActive(true);
        MainMenu.SetActive(false);
        StatMenu.SetActive(false);
        //InvMenu.SetActive(false);
        isShopOpen = true;
    }

    public void AdvanceArrow(bool isUp)
    {
        if (isUp)
        {
            if (itemArrow < 2)
            {
                itemArrow++;
            }
            else
            {
                itemArrow = 0;
            }
        }
        if (!isUp)
        {
            if (itemArrow > 0)
            {
                itemArrow--;
            }
            else
            {
                itemArrow = 2;
            }
        }
        switch (itemArrow)
        {
            case 0:
                Item1Arrow.SetActive(true);
                Item2Arrow.SetActive(false);
                Item3Arrow.SetActive(false);
                break;
            case 1:
                if (currentShop.shopInventory.Count < 1)
                {
                    Item1Arrow.SetActive(false);
                    Item2Arrow.SetActive(true);
                    Item3Arrow.SetActive(false);
                }
                else
                {
                    itemArrow = 0;
                    Item1Arrow.SetActive(true);
                    Item2Arrow.SetActive(false);
                    Item3Arrow.SetActive(false);
                }
                break;
            case 2:
                if (currentShop.shopInventory.Count < 2)
                {
                    Item1Arrow.SetActive(false);
                    Item2Arrow.SetActive(false);
                    Item3Arrow.SetActive(true);
                }
                else
                {
                    itemArrow = 1;
                    Item1Arrow.SetActive(false);
                    Item2Arrow.SetActive(true);
                    Item3Arrow.SetActive(false);
                }
                break;
        }
    }

    public void StartBuySell()
    {
        isBuySell = true;
        onBuy = true;
        switch(itemArrow)
        {
            case 0:
                Item1SelectArrow.SetActive(true);
                selectedItem = item1;
                break;
            case 1:
                Item2SelectArrow.SetActive(true);
                selectedItem = item2;
                break;
            case 2:
                Item3SelectArrow.SetActive(true);
                selectedItem = item3;
                break;
        }
        Item1Arrow.SetActive(false);
        Item2Arrow.SetActive(false);
        Item3Arrow.SetActive(false);
        BuyArrow.SetActive(true);
        SellArrow.SetActive(false);
    }

    public void EndBuySell()
    {
        Item1SelectArrow.SetActive(false);
        Item2SelectArrow.SetActive(false);
        Item3SelectArrow.SetActive(false);
        BuyArrow.SetActive(false);
        SellArrow.SetActive(false);
        switch (itemArrow)
        {
            case 0:
                Item1Arrow.SetActive(true);
                Item2Arrow.SetActive(false);
                Item3Arrow.SetActive(false);
                break;
            case 1:
                Item1Arrow.SetActive(false);
                Item2Arrow.SetActive(true);
                Item3Arrow.SetActive(false);
                break;
            case 2:
                Item1Arrow.SetActive(false);
                Item2Arrow.SetActive(false);
                Item3Arrow.SetActive(true);
                break;
        }
        isBuySell = false;

    }

    public void UpdateText()
    {
        moneyHeld.text = "Credits Held: " + inventoryManager.money.ToString();

        //Load Item 1
        shopItem1Name.text = item1.itemName;
        shopItem1Price.text = item1.buyPrice.ToString();
        shopItem1Amount.text = item1.amount.ToString();
        Item1Arrow.SetActive(true);

        if (currentShop.shopInventory.Count > 0)
        {
            //Load Item 2
            shopItem2Name.text = item2.itemName;
            shopItem2Price.text = item2.buyPrice.ToString();
            shopItem2Amount.text = item2.amount.ToString();
        }

        if (currentShop.shopInventory.Count > 1)
        {
            //Load Item 3
            shopItem3Name.text = item3.itemName;
            shopItem3Price.text = item3.buyPrice.ToString();
            shopItem3Amount.text = item3.amount.ToString();
        }
    }

    public void CloseShop()
    {
        ShopMenu.SetActive(false);
        MenuObject.SetActive(false);
        soundManager.PlaySoundClip(2);
        John.menuOpen = false;
    }
}
