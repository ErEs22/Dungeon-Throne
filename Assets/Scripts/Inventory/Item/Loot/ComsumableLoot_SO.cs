using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Loot/Comsumable", fileName = "ComsumableLoot_SO")]
public class ComsumableLoot_SO : ScriptableObject
{
    public GameObject lootPrefab;

    public ComsumableItem comsumableItem;
}
