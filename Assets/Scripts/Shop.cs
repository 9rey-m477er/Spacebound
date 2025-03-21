using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spacebound/Inventory/New Shop")]

public class Shop : ScriptableObject
{
    public string shopName;
    public List<Item> shopInventory;
    public AudioClip shopMusic;
    public Sprite shopPortrait;
    public DialogueText shopOpenText;
    public DialogueText shopBuyText;
    public DialogueText shopSellText;
    public DialogueText shopCloseText;
}
