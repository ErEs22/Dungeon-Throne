using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [DisplayOnly] public Loot currentCollectable;

    [DisplayOnly] public ItemUIManager itemUIManager;

    [DisplayOnly] public EquipmentItem currentEquipment;

    [DisplayOnly] public ComsumableItem currentComsumable;

    /// <summary>
    /// 背包的UI对象
    /// </summary>
    GameObject inventoryUI;

    /// <summary>
    /// 当前的物品ID
    /// </summary>
    [DisplayOnly][SerializeField] int currentMaxID = 1;

    string saveDataName = "SaveData" + SaveSystem.dataIndex + ".txt";

    [HideInInspector] public PlayerManager PM;

    /// <summary>
    /// 玩家背包的所有武器字典
    /// </summary>
    /// <typeparam name="int">武器ID</typeparam>
    /// <typeparam name="WeaponItem">武器对象</typeparam>
    public Dictionary<int, WeaponItem> playerWeaponInventory = new Dictionary<int, WeaponItem>();//玩家拥有的武器

    /// <summary>
    /// 玩家背包的所有装备字典
    /// </summary>
    /// <typeparam name="int"></typeparam>
    /// <typeparam name="EquipmentItem"></typeparam>
    /// <returns></returns>
    public Dictionary<int, EquipmentItem> playerEquipmentInventory = new Dictionary<int, EquipmentItem>();

    /// <summary>
    /// 玩家背包的所有消耗品字典
    /// </summary>
    /// <typeparam name="int"></typeparam>
    /// <typeparam name="ComsumableItem"></typeparam>
    /// <returns></returns>
    public Dictionary<int, ComsumableItem> playerComsumableInventory = new Dictionary<int, ComsumableItem>();

    private void Awake()
    {
        InitilizeObject();
        LoadPlayerData();
        // LoadAllWeaponToPlayer();
        // LoadAllEquipmentToPlayer();
        // LoadAllComsumableToPlayer();
    }

    private void OnEnable()
    {
        PM.playerInputHandler.showInventory += ShowInventory;
        PM.playerInputHandler.collect += Collect;
        PM.playerInputHandler.useComsumable += UseComsumable;
    }

    private void Start()
    {
        StartCoroutine(nameof(SaveDataCoroutine));
    }

    private void OnDisable()
    {
        PM.playerInputHandler.showInventory -= ShowInventory;
        PM.playerInputHandler.collect -= Collect;
        PM.playerInputHandler.useComsumable -= UseComsumable;
        StopCoroutine(nameof(SaveDataCoroutine));
    }

    /// <summary>
    /// 保存玩家数据
    /// </summary>
    public void SavePlayerData()
    {
        //保存数据对象
        PlayerData playerData = new PlayerData();
        int i = 0;
        //根据当前背包大小创建相应大小的数组
        playerData.weaponItems = new WeaponItem[playerWeaponInventory.Count];
        playerData.equipmentItems = new EquipmentItem[playerEquipmentInventory.Count];
        playerData.comsumableItems = new ComsumableItem[playerComsumableInventory.Count];
        //将背包中的物品添加到保存数据对象中
        foreach (var item in playerWeaponInventory)
        {
            playerData.weaponItems[i] = item.Value;
            i++;
        }
        i = 0;
        foreach (var item in playerEquipmentInventory)
        {
            playerData.equipmentItems[i] = item.Value;
            i++;
        }
        i = 0;
        foreach (var item in playerComsumableInventory)
        {
            playerData.comsumableItems[i] = item.Value;
            i++;
        }
        playerData.currentMaxID = currentMaxID;
        //保存数据到指定路径
        SaveSystem.Save(SaveSystem.saveFileName + SaveSystem.dataIndex + ".txt", playerData);
    }

    /// <summary>
    /// 加载存档数据
    /// </summary>
    public void LoadPlayerData()
    {
        //加载数据，储存到加载的数据对象上
        PlayerData playerData = SaveSystem.Load<PlayerData>(SaveSystem.saveFileName + SaveSystem.dataIndex + ".txt");
        if (playerData == null)
        {
            return;
        }
        //将数据对象上的数据添加到玩家的数据中
        for (int i = 0; i < playerData.weaponItems.Length; i++)
        {
            playerWeaponInventory.Add(playerData.weaponItems[i].itemID, playerData.weaponItems[i]);
        }
        for (int i = 0; i < playerData.equipmentItems.Length; i++)
        {
            playerEquipmentInventory.Add(playerData.equipmentItems[i].itemID, playerData.equipmentItems[i]);
        }
        for (int i = 0; i < playerData.comsumableItems.Length; i++)
        {
            playerComsumableInventory.Add(playerData.comsumableItems[i].itemID, playerData.comsumableItems[i]);
        }
        currentMaxID = playerData.currentMaxID;
    }

    void InitilizeObject()
    {
        PM = GetComponent<PlayerManager>();
        inventoryUI = FindObjectOfType<ItemUIManager>(true).gameObject;
        itemUIManager = FindObjectOfType<ItemUIManager>(true);
    }

    /// <summary>
    /// 打开背包
    /// </summary>
    void ShowInventory()
    {
        inventoryUI.SetActive(!inventoryUI.gameObject.activeSelf);
        if (inventoryUI.activeSelf == true)
        {
            PM.playerInputHandler.DisablePlayerActions();
        }
        else
        {
            PM.playerInputHandler.EnablePlayerActions();
        }
    }

    /// <summary>
    /// 加载游戏武器库的所有武器到玩家背包（测试用）
    /// </summary>
    void LoadAllWeaponToPlayer()
    {
        foreach (var item in ItemManager.GameWeaponInventory.Values)
        {
            AddWeaponItem(item);
        }
    }

    /// <summary>
    /// 加载游戏装备库的所有装备到玩家背包（测试用）
    /// </summary>
    void LoadAllEquipmentToPlayer()
    {
        foreach (var item in ItemManager.GameEquipmentInventory.Values)
        {
            AddEquipmentItem(item);
        }
    }

    /// <summary>
    /// 加载游戏消耗品库的所有消耗品到玩家背包（测试用）
    /// </summary>
    void LoadAllComsumableToPlayer()
    {
        foreach (var item in ItemManager.GameComsumableInventory.Values)
        {
            AddComsumableItem(item);
        }
    }

    /// <summary>
    /// 添加武器到玩家武器背包
    /// </summary>
    /// <param name="item">武器对象</param>
    public void AddWeaponItem(WeaponItem item)
    {
        item.itemID = currentMaxID;
        currentMaxID++;
        print(item.itemID);
        playerWeaponInventory.Add(item.itemID, item);
        itemUIManager?.LoadWeaponItemUIs();
    }

    /// <summary>
    /// 添加装备到玩家装备背包
    /// </summary>
    /// <param name="item">装备对象</param>
    public void AddEquipmentItem(EquipmentItem item)
    {
        item.itemID = currentMaxID;
        currentMaxID++;
        playerEquipmentInventory.Add(item.itemID, item);
        itemUIManager?.LoadEquipmentItemUIs();
    }

    /// <summary>
    /// 添加消耗品到玩家消耗品背包
    /// </summary>
    /// <param name="item">消耗品对象</param>
    public void AddComsumableItem(ComsumableItem item)
    {
        if (playerComsumableInventory.ContainsKey(item.itemID))
        {
            playerComsumableInventory[item.itemID].count++;
        }
        else
        {
            playerComsumableInventory.Add(item.itemID, item);
        }
        itemUIManager?.LoadComsumableItemUIs();
    }

    /// <summary>
    /// 切换当前武器
    /// </summary>
    /// <param name="weapon">要切换的武器</param>
    public void SetCurrentWeapon(WeaponItem weapon)
    {
        PM.playerAttackHandler.currentWeapon = weapon;
        PM.weaponSlotManager.LoadWeaponToSlot(weapon);
    }

    /// <summary>
    /// 切换当前装备
    /// </summary>
    /// <param name="equipment">要切换的装备</param>
    public void SetCurrentEquipment(EquipmentItem equipment)
    {
        currentEquipment = equipment;
        PM.playerStats.RecalculatePlayerStats();
    }

    /// <summary>
    /// 切换当前消耗品
    /// </summary>
    /// <param name="equipment">要切换的消耗品</param>
    public void SetCurrentComsumable(ComsumableItem comsumable)
    {
        currentComsumable = comsumable;
    }

    /// <summary>
    /// /// 使用消耗品
    /// </summary>
    void UseComsumable()
    {
        if (itemUIManager.currentUsingComsumableItemUI == null)
        {
            return;
        }
        if (currentComsumable.count < 1)
        {
            return;
        }
        PM.playerStats.RecoverHealth(currentComsumable.baseGain);
        currentComsumable.count--;
        //如果数量为零，则用完了，从背包中删除,然后更新背包
        if (currentComsumable.count == 0)
        {
            playerComsumableInventory.Remove(currentComsumable.itemID);
            itemUIManager.currentUsingComsumableItemUI.usingBackground.SetActive(false);
            itemUIManager.currentUsingComsumableItemUI = null;
            itemUIManager.comsumableIcon.sprite = itemUIManager.transparencySprite;
            itemUIManager.comsumableCount.text = "0";
        }
        else
        {
            itemUIManager.comsumableCount.text = currentComsumable.count.ToString();
        }
        itemUIManager.LoadComsumableItemUIs();
    }

    /// <summary>
    /// 拾取物品
    /// /// </summary>
    void Collect()
    {
        currentCollectable?.Collect();
    }

    IEnumerator SaveDataCoroutine()
    {
        while (true)
        {
            SavePlayerData();
            yield return new WaitForSeconds(10f);
        }
    }
}
