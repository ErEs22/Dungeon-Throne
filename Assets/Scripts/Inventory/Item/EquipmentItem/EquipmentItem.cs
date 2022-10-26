using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipmentItem : Item
{
    public EquipmentItem()
    {

    }
    public EquipmentItem(string itemName, string iconName, int level, int baseGain, string description)
    {
        this.itemName = itemName;
        this.iconName = iconName;
        this.level = level;
        this.baseGain = baseGain;
        this.description = description;
    }
    public int level = 1;

    public int baseGain = 1;

    public string description = "";
}
