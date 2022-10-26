using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Loot/Weapon", fileName = "WeaponLoot_SO")]
public class WeaponLoot_SO : ScriptableObject
{
    public GameObject lootPrefab;

    public WeaponItem weaponItem;
}
