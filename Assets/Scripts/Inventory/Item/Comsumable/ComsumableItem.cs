using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComsumableItem : Item
{
    public ComsumableItem()
    {

    }
    public ComsumableItem(int itemID, string itemName, string iconName, int baseGain, int count, string description)
    {
        this.itemID = itemID;
        this.itemName = itemName;
        this.iconName = iconName;
        this.baseGain = baseGain;
        this.count = count;
        this.description = description;
    }

    public int baseGain = 1;

    public int count = 1;

    public string description = "";
}
