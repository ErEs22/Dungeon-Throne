using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponItem : Item
{
    public WeaponItem()
    {

    }
    public WeaponItem(string itemName, string iconName, int level, int baseATK, string description, int baseStamina, float baseStaminaLightMultiplier, float baseStaminaHeavyMultiplier)
    {
        this.itemName = itemName;
        this.iconName = iconName;
        this.level = level;
        this.baseATK = baseATK;
        this.description = description;
        this.baseStamina = baseStamina;
        this.baseStaminaLightMultiplier = baseStaminaLightMultiplier;
        this.baseStaminaHeavyMultiplier = baseStaminaHeavyMultiplier;
    }

    public int level = 1;

    public int baseATK = 10;

    public string description = "";

    public int baseStamina = 10;

    public float baseStaminaLightMultiplier = 1;

    public float baseStaminaHeavyMultiplier = 2;
}
