using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Loot/Equipment", fileName = "EquipmentLoot_SO")]
public class EquipmentLoot_SO : ScriptableObject
{
    public GameObject lootPrefab;

    public EquipmentItem equipmentItem;
}
