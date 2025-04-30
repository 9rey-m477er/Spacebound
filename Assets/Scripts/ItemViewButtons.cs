using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewButtons : MonoBehaviour
{
    public Item viewItem;
    public InventoryManager im;

    public void describe() //mapped script to buttons so they run when E is pressed
    {
        im.viewDescription(viewItem);
    }
}
