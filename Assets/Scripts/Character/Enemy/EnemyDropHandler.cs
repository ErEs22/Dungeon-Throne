using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropHandler : MonoBehaviour
{
    [SerializeField] DropObjects_SO dropObjects_SO;

    [SerializeField, Range(1, 100)] int minLootLevel = 1;

    [SerializeField, Range(1, 100)] int maxLootLevel = 2;

    [SerializeField] string[] weaponLoots;

    [SerializeField] string[] equipmentLoots;

    [SerializeField] string[] comsumableLoots;

    /// <summary>
    /// 掉落武器
    /// </summary>
    public void DropWeapons()
    {
        if (weaponLoots.Length < 1)
        {
            return;
        }
        //将武器的数组中的物品生成
        foreach (var item in weaponLoots)
        {
            GameObject loot = PoolManager.Release(dropObjects_SO.WeaponLoot, new Vector3(transform.position.x, transform.position.y + 1, 0));
            WeaponItem newWeaponItem = ItemManager.GetWeaponItem(item);
            loot.GetComponent<WeaponLoot>().weaponLoot = new WeaponItem(
                newWeaponItem.itemName,
                newWeaponItem.iconName,
                Random.Range(minLootLevel, maxLootLevel + 1),
                newWeaponItem.baseATK,
                newWeaponItem.description,
                newWeaponItem.baseStamina,
                newWeaponItem.baseStaminaLightMultiplier,
                newWeaponItem.baseStaminaHeavyMultiplier
            );
            loot.GetComponentInChildren<WeaponLoot>().SetSprite();
        }
    }

    /// <summary>
    /// 掉落装备
    /// </summary>
    public void DropEquipments()
    {
        if (equipmentLoots.Length < 1)
        {
            return;
        }
        //将装备的数组中的物品生成
        foreach (var item in equipmentLoots)
        {
            GameObject loot = PoolManager.Release(dropObjects_SO.EquipmentLoot, new Vector3(transform.position.x, transform.position.y + 1, 0));
            EquipmentItem newEquipmentItem = ItemManager.GetEquipmentItem(item);
            loot.GetComponent<EquipmentLoot>().equipmentLoot = new EquipmentItem(
                newEquipmentItem.itemName,
                newEquipmentItem.iconName,
                Random.Range(minLootLevel, maxLootLevel + 1),
                newEquipmentItem.baseGain,
                newEquipmentItem.description
            );
            loot.GetComponentInChildren<EquipmentLoot>().SetSprite();
        }
    }

    /// <summary>
    /// 掉落消耗品
    /// </summary>
    public void DropComsumables()
    {
        if (comsumableLoots.Length < 1)
        {
            return;
        }
        //将消耗品的数组中的物品生成
        foreach (var item in comsumableLoots)
        {
            GameObject loot = PoolManager.Release(dropObjects_SO.ComsumableLoot, new Vector3(transform.position.x, transform.position.y + 1, 0));
            ComsumableItem newComsumableItem = ItemManager.GetComsumableItem(item);
            loot.GetComponent<ComsumableLoot>().comsumableLoot = new ComsumableItem(
                newComsumableItem.itemID,
                newComsumableItem.itemName,
                newComsumableItem.iconName,
                newComsumableItem.baseGain,
                newComsumableItem.count,
                newComsumableItem.description
            );
            loot.GetComponentInChildren<ComsumableLoot>().SetSprite();
        }
    }
}
