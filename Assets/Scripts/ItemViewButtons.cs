using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewButtons : MonoBehaviour
{
    public Item viewItem;
    public InventoryManager im;

    public void describe()
    {
        im.viewDescription(viewItem);
    }
}
