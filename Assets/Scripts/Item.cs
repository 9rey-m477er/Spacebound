using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spacebound/Inventory/New Item")]

public class Item : ScriptableObject
{
    public string itemName = string.Empty;
    public int amount = 0;
    public string description = string.Empty;
    public int buyPrice = 0;
    public int sellPrice = 0;
    public bool useableInWorld = false;
    public int typeIndex = 0;
    public int specIndex = 0;

}
