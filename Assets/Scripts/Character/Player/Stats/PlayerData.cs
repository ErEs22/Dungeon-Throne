using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] public WeaponItem[] weaponItems;

    [SerializeField] public EquipmentItem[] equipmentItems;

    [SerializeField] public ComsumableItem[] comsumableItems;

    [SerializeField] public int currentMaxID = 0;
}
