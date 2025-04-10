using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    public InventoryManager inventoryManager;
    public DialogueController dialogueController;
    public GameObject MenuObject, ShopMenu, MainMenu, StatMenu; //, InvMenu;
    public OmniDirectionalMovement John;
    private Shop currentShop;

    public Sprite shopImage;
    public TextMeshProUGUI shopNameText;
    public TextMeshProUGUI shopItem1Name, shopItem1Price, shopItem1Amount;
    public TextMeshProUGUI shopItem2Name, shopItem2Price, shopItem2Amount;
    public TextMeshProUGUI shopItem3Name, shopItem3Price, shopItem3Amount;
    public TextMeshProUGUI moneyHeld;

    public void OpenShop(Shop shop)
    {
        //Save shop as currentShop
        currentShop = shop;
        //Load Basic Shop Info
        shopImage = shop.shopPortrait;
        shopNameText.text = shop.shopName;

        //Load Item 1
        shopItem1Name.text = shop.shopInventory[0].itemName;
        shopItem1Price.text = shop.shopInventory[0].buyPrice.ToString();
        shopItem1Amount.text = shop.shopInventory[0].amount.ToString();

        if (shop.shopInventory.Count > 1)
        {
            //Load Item 2
            shopItem2Name.text = shop.shopInventory[1].itemName;
            shopItem2Price.text = shop.shopInventory[1].buyPrice.ToString();
            shopItem2Amount.text = shop.shopInventory[1].amount.ToString();
        }

        if (shop.shopInventory.Count > 2)
        {
            //Load Item 3
            shopItem3Name.text = shop.shopInventory[2].itemName;
            shopItem3Price.text = shop.shopInventory[2].buyPrice.ToString();
            shopItem3Amount.text = shop.shopInventory[2].amount.ToString();
        }

        MenuObject.SetActive(true);
        ShopMenu.SetActive(true);
        MainMenu.SetActive(false);
        StatMenu.SetActive(false);
        //InvMenu.SetActive(false);

        dialogueController.startCutscene(shop.shopOpenText);
    }

    public void CloseShop()
    {
        ShopMenu.SetActive(false);
        MenuObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
