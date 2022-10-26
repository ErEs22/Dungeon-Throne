using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    /// <summary>
    /// 游戏所有的武器库
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="WeaponItem"></typeparam>
    /// <returns></returns>
    static Dictionary<string, WeaponItem> gameWeaponInventory = new Dictionary<string, WeaponItem>();

    public static Dictionary<string, WeaponItem> GameWeaponInventory => gameWeaponInventory;

    /// <summary>
    /// 游戏所有的装备库
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="EquipmentItem"></typeparam>
    /// <returns></returns>
    static Dictionary<string, EquipmentItem> gameEquipmentInventory = new Dictionary<string, EquipmentItem>();

    public static Dictionary<string, EquipmentItem> GameEquipmentInventory => gameEquipmentInventory;

    /// <summary>
    /// 游戏所有的消耗品库
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="ComsumableItem"></typeparam>
    /// <returns></returns>
    static Dictionary<string, ComsumableItem> gameComsumableInventory = new Dictionary<string, ComsumableItem>();

    public static Dictionary<string, ComsumableItem> GameComsumableInventory => gameComsumableInventory;

    /// <summary>
    /// 加载游戏所有武器
    /// </summary>
    public static void LoadWeaponItems()
    {
        //读取Excel表并返回武器数组
        WeaponItem[] weaponItems = ExcelTool.CreateWeaponItemArrayWithExcel("Assets/Resources/Excels/WeaponItem.xlsx");

        foreach (WeaponItem item in weaponItems)
        {
            //遍历将Excel中读取的武器数据加入游戏武器库中
            gameWeaponInventory.Add(item.itemName, item);
        }
    }

    /// <summary>
    /// 加载游戏所有装备
    /// </summary>
    public static void LoadEquipmentItems()
    {
        //读取Excel表并返回装备数组
        EquipmentItem[] equipmentItems = ExcelTool.CreateEquipmentItemArrayWithExcel("Assets/Resources/Excels/EquipmentItem.xlsx");

        foreach (EquipmentItem item in equipmentItems)
        {
            //遍历将Excel中读取的装备数据加入游戏装备库中
            gameEquipmentInventory.Add(item.itemName, item);
        }
    }

    public static void LoadComsumableItems()
    {
        ComsumableItem[] comsumableItems = ExcelTool.CreateComsumableItemArrayWithExcel("Assets/Resources/Excels/ComsumableItem.xlsx");

        foreach (ComsumableItem item in comsumableItems)
        {
            gameComsumableInventory.Add(item.itemName, item);
        }
    }

    /// <summary>
    /// 获取游戏武器库中的指定武器
    /// </summary>
    /// <param name="weaponName">武器名称</param>
    /// <returns>获取的武器</returns>
    public static WeaponItem GetWeaponItem(string weaponName)
    {
        return gameWeaponInventory[weaponName];
    }

    public static WeaponItem GetWeaponItem(string weaponName, int level)
    {
        WeaponItem weaponItem = gameWeaponInventory[weaponName];
        weaponItem.level = level;
        return weaponItem;
    }

    /// <summary>
    /// 获取游戏装备库中指定装备
    /// </summary>
    /// <param name="equipmentName">装备名称</param>
    /// <returns>获取的装备</returns>
    public static EquipmentItem GetEquipmentItem(string equipmentName)
    {
        return gameEquipmentInventory[equipmentName];
    }

    public static EquipmentItem GetEquipmentItem(string equipmentName, int level)
    {
        EquipmentItem equipmentItem = gameEquipmentInventory[equipmentName];
        equipmentItem.level = level;
        return equipmentItem;
    }

    /// <summary>
    /// 获取游戏消耗品库中指定消耗品
    /// </summary>
    /// <param name="comsumableName">消耗品名称</param>
    /// <returns>获取的消耗品</returns>
    public static ComsumableItem GetComsumableItem(string comsumableName)
    {
        return gameComsumableInventory[comsumableName];
    }
}
