using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUIManager : MonoBehaviour
{
    /// <summary>
    /// 当前选中的武器
    /// </summary>
    public WeaponItemUI currentSelectWeaponItemUI;

    /// <summary>
    /// 当前正在使用的武器
    /// </summary>
    public WeaponItemUI currentUsingWeaponItemUI;

    /// <summary>
    /// 当前选中的装备
    /// </summary>
    public EquipmentItemUI currentSelectEquipmentItemUI;

    /// <summary>
    /// 当前正在使用的装备
    /// </summary>
    public EquipmentItemUI currentUsingEquipmentItemUI;

    /// <summary>
    /// 当前选中的消耗品
    /// </summary>
    public ComsumableItemUI currentSelectComsumableItemUI;

    /// <summary>
    /// 当前正在使用的消耗品
    /// </summary>
    public ComsumableItemUI currentUsingComsumableItemUI;

    public TextMeshProUGUI comsumableCount;

    /// <summary>
    /// 游戏显示UI上的装备栏
    /// </summary>
    public Image comsumableIcon;

    /// <summary>
    /// 默认空栏位的的精灵图
    /// </summary>
    public Sprite transparencySprite;

    [SerializeField] RectTransform informationField;

    /// <summary>
    /// 游戏显示UI上的武器栏
    /// </summary>
    [SerializeField] Image weaponIcon;

    /// <summary>
    /// 游戏显示UI上的装备栏
    /// </summary>
    [SerializeField] Image equipmentIcon;

    /// <summary>
    /// 所有的武器栏位
    /// </summary>
    [SerializeField] WeaponItemUI[] weaponItemUIs;

    /// <summary>
    /// 所有的装备栏位
    /// </summary>
    [SerializeField] EquipmentItemUI[] equipmentItemUIs;

    /// <summary>
    /// 所有的消耗品栏位
    /// </summary>
    [SerializeField] ComsumableItemUI[] comsumableItemUIs;

    /// <summary>
    /// 玩家背包
    /// </summary>
    PlayerInventory playerInventory;

    int usingWeaponItemID = 0;

    int usingEquipmentItemID = 0;

    int usingComsumableItemID = 0;

    private void Awake()
    {
        InitilizeObject();
        GetAllItemUIs();
    }

    private void OnEnable()
    {
        EnableInformationUIs();
        LoadWeaponItemUIs();
        LoadEquipmentItemUIs();
        LoadComsumableItemUIs();
        EventManager.Instance.dropWeaponItem += DropWeaponItem;
        EventManager.Instance.useWeaponItem += UseWeaponItem;
        EventManager.Instance.dropEquipmentItem += DropEquipmentItem;
        EventManager.Instance.useEquipmentItem += UseEquipmentItem;
        EventManager.Instance.dropComsumableItem += DropComsumableItem;
        EventManager.Instance.useComsumableItem += UseComsumableItem;
    }

    private void OnDisable()
    {
        DisableInformationUIs();
        EventManager.Instance.dropWeaponItem -= DropWeaponItem;
        EventManager.Instance.useWeaponItem -= UseWeaponItem;
        EventManager.Instance.dropEquipmentItem -= DropEquipmentItem;
        EventManager.Instance.useEquipmentItem -= UseEquipmentItem;
        EventManager.Instance.dropComsumableItem -= DropComsumableItem;
        EventManager.Instance.useComsumableItem -= UseComsumableItem;
    }

    void InitilizeObject()
    {
        playerInventory = FindObjectOfType<PlayerInventory>(true);
    }

    /// <summary>
    /// 更新正在使用的武器栏位以及背景
    /// </summary>
    /// <param name="itemID">武器ID</param>
    public void UpdateUsingWeaponItem()
    {
        //如果当前没有使用武器或使用的武器栏位未改变位置，则不更新
        if (currentUsingWeaponItemUI == null)
        {
            return;
        }

        if (currentUsingWeaponItemUI.itemID == usingWeaponItemID)
        {
            return;
        }

        currentUsingWeaponItemUI.itemUsing = false;
        currentUsingWeaponItemUI.usingBackground.SetActive(false);

        foreach (var item in weaponItemUIs)
        {
            if (item.itemID == usingWeaponItemID)
            {
                currentUsingWeaponItemUI = item;
                currentUsingWeaponItemUI.itemUsing = true;
                currentUsingWeaponItemUI.usingBackground.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 更新正在使用的装备栏位以及背景
    /// </summary>
    /// <param name="itemID">装备ID</param>
    public void UpdateUsingEquipmentItem()
    {
        //如果当前没有使用装备或使用的装备栏位未改变位置，则不更新
        if (currentUsingEquipmentItemUI == null)
        {
            return;
        }

        if (currentUsingEquipmentItemUI.itemID == usingEquipmentItemID)
        {
            return;
        }

        currentUsingEquipmentItemUI.itemUsing = false;
        currentUsingEquipmentItemUI.usingBackground.SetActive(false);

        foreach (var item in equipmentItemUIs)
        {
            if (item.itemID == usingEquipmentItemID)
            {
                currentUsingEquipmentItemUI = item;
                currentUsingEquipmentItemUI.itemUsing = true;
                currentUsingEquipmentItemUI.usingBackground.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 更新正在使用的装备栏位以及背景
    /// </summary>
    /// <param name="itemID">装备ID</param>
    public void UpdateUsingComsumableItem()
    {
        //如果当前没有使用装备或使用的装备栏位未改变位置，则不更新
        if (currentUsingComsumableItemUI == null)
        {
            return;
        }

        if (currentUsingComsumableItemUI.itemID == usingComsumableItemID)
        {
            return;
        }

        currentUsingComsumableItemUI.itemUsing = false;
        currentUsingComsumableItemUI.usingBackground.SetActive(false);

        foreach (var item in comsumableItemUIs)
        {
            if (item.itemID == usingComsumableItemID)
            {
                currentUsingComsumableItemUI = item;
                currentUsingComsumableItemUI.itemUsing = true;
                currentUsingComsumableItemUI.usingBackground.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 丢弃武器
    /// </summary>
    public void DropWeaponItem()
    {
        //如果要丢弃的武器是正在使用的，则不可丢弃
        if (currentUsingWeaponItemUI != null)
        {
            usingWeaponItemID = currentUsingWeaponItemUI.itemID;
            if (currentSelectWeaponItemUI.itemID == currentUsingWeaponItemUI.itemID)
            {
                return;
            }
        }

        currentSelectWeaponItemUI.dropButton.PointExit();
        playerInventory.playerWeaponInventory.Remove(currentSelectWeaponItemUI.itemID);
        //丢弃完重新加载栏位显示图标和信息以及正在使用的武器栏位
        LoadWeaponItemUIs();
        UpdateUsingWeaponItem();
    }

    /// <summary>
    /// 丢弃装备
    /// </summary>
    public void DropEquipmentItem()
    {
        //如果要丢弃的装备是正在使用的，则不可丢弃
        if (currentUsingEquipmentItemUI != null)
        {
            usingEquipmentItemID = currentUsingEquipmentItemUI.itemID;
            if (currentSelectEquipmentItemUI.itemID == currentUsingEquipmentItemUI.itemID)
            {
                return;
            }
        }

        currentSelectEquipmentItemUI.dropButton.PointExit();
        playerInventory.playerEquipmentInventory.Remove(currentSelectEquipmentItemUI.itemID);
        //丢弃完重新加载栏位显示图标和信息以及正在使用的装备栏位
        LoadEquipmentItemUIs();
        UpdateUsingEquipmentItem();
    }

    /// <summary>
    /// 丢弃消耗品
    /// </summary>
    public void DropComsumableItem()
    {
        //如果要丢弃的装备是正在使用的，则不可丢弃
        if (currentUsingComsumableItemUI != null)
        {
            usingComsumableItemID = currentUsingComsumableItemUI.itemID;
            if (currentSelectComsumableItemUI.itemID == currentUsingComsumableItemUI.itemID)
            {
                return;
            }
        }

        currentSelectComsumableItemUI.dropButton.PointExit();
        playerInventory.playerComsumableInventory.Remove(currentSelectComsumableItemUI.itemID);
        //丢弃完重新加载栏位显示图标和信息以及正在使用的装备栏位
        LoadComsumableItemUIs();
        UpdateUsingComsumableItem();
    }

    /// <summary>
    /// 使用武器
    /// </summary>
    public void UseWeaponItem()
    {
        //未装备任何武器的情况下
        if (currentUsingWeaponItemUI == null)
        {
            usingWeaponItemID = currentSelectWeaponItemUI.itemID;
            currentUsingWeaponItemUI = currentSelectWeaponItemUI;
            currentUsingWeaponItemUI.itemUsing = true;
            currentUsingWeaponItemUI.itemID = usingWeaponItemID;
            currentUsingWeaponItemUI.usingBackground.SetActive(true);
            playerInventory.SetCurrentWeapon(playerInventory.playerWeaponInventory[currentUsingWeaponItemUI.itemID]);
            weaponIcon.sprite = currentUsingWeaponItemUI.fieldIcon.sprite;
        }
        //装备了武器的情况下
        else if (currentUsingWeaponItemUI.itemID != currentSelectWeaponItemUI.itemID)
        {
            usingWeaponItemID = currentSelectWeaponItemUI.itemID;
            currentUsingWeaponItemUI.usingBackground.SetActive(false);
            currentUsingWeaponItemUI.itemUsing = false;
            currentUsingWeaponItemUI = currentSelectWeaponItemUI;
            currentUsingWeaponItemUI.itemUsing = true;
            currentUsingWeaponItemUI.itemID = usingWeaponItemID;
            currentUsingWeaponItemUI.usingBackground.SetActive(true);
            playerInventory.SetCurrentWeapon(playerInventory.playerWeaponInventory[currentUsingWeaponItemUI.itemID]);
            weaponIcon.sprite = currentUsingWeaponItemUI.fieldIcon.sprite;
        }
    }

    /// <summary>
    /// 使用装备
    /// </summary>
    public void UseEquipmentItem()
    {
        //未装备任何装备的情况下
        if (currentUsingEquipmentItemUI == null)
        {
            usingEquipmentItemID = currentSelectEquipmentItemUI.itemID;
            currentUsingEquipmentItemUI = currentSelectEquipmentItemUI;
            currentUsingEquipmentItemUI.itemUsing = true;
            currentUsingEquipmentItemUI.itemID = usingEquipmentItemID;
            currentUsingEquipmentItemUI.usingBackground.SetActive(true);
            playerInventory.SetCurrentEquipment(playerInventory.playerEquipmentInventory[currentUsingEquipmentItemUI.itemID]);
            equipmentIcon.sprite = currentUsingEquipmentItemUI.fieldIcon.sprite;
        }
        //装备了装备的情况下
        else if (currentUsingEquipmentItemUI.itemID != currentSelectEquipmentItemUI.itemID)
        {
            usingEquipmentItemID = currentSelectEquipmentItemUI.itemID;
            currentUsingEquipmentItemUI.usingBackground.SetActive(false);
            currentUsingEquipmentItemUI.itemUsing = false;
            currentUsingEquipmentItemUI = currentSelectEquipmentItemUI;
            currentUsingEquipmentItemUI.itemUsing = true;
            currentUsingEquipmentItemUI.itemID = usingEquipmentItemID;
            currentUsingEquipmentItemUI.usingBackground.SetActive(true);
            playerInventory.SetCurrentEquipment(playerInventory.playerEquipmentInventory[currentUsingEquipmentItemUI.itemID]);
            equipmentIcon.sprite = currentUsingEquipmentItemUI.fieldIcon.sprite;
        }
    }

    /// <summary>
    /// 使用消耗品
    /// </summary>
    public void UseComsumableItem()
    {
        //未装备任何装备的情况下
        if (currentUsingComsumableItemUI == null)
        {
            usingComsumableItemID = currentSelectComsumableItemUI.itemID;
            currentUsingComsumableItemUI = currentSelectComsumableItemUI;
            currentUsingComsumableItemUI.itemUsing = true;
            currentUsingComsumableItemUI.itemID = usingComsumableItemID;
            currentUsingComsumableItemUI.usingBackground.SetActive(true);
            playerInventory.SetCurrentComsumable(playerInventory.playerComsumableInventory[currentUsingComsumableItemUI.itemID]);
            comsumableIcon.sprite = currentUsingComsumableItemUI.fieldIcon.sprite;
            comsumableCount.text = playerInventory.playerComsumableInventory[currentUsingComsumableItemUI.itemID].count.ToString();
        }
        //装备了装备的情况下
        else if (currentUsingComsumableItemUI.itemID != currentSelectComsumableItemUI.itemID)
        {
            usingComsumableItemID = currentSelectComsumableItemUI.itemID;
            currentUsingComsumableItemUI.usingBackground.SetActive(false);
            currentUsingComsumableItemUI.itemUsing = false;
            currentUsingComsumableItemUI = currentSelectComsumableItemUI;
            currentUsingComsumableItemUI.itemUsing = true;
            currentUsingComsumableItemUI.itemID = usingComsumableItemID;
            currentUsingComsumableItemUI.usingBackground.SetActive(true);
            playerInventory.SetCurrentComsumable(playerInventory.playerComsumableInventory[currentUsingComsumableItemUI.itemID]);
            comsumableIcon.sprite = currentUsingComsumableItemUI.fieldIcon.sprite;
            comsumableCount.text = playerInventory.playerComsumableInventory[currentUsingComsumableItemUI.itemID].count.ToString();
        }
    }

    /// <summary>
    /// 获取所有子物体的ItemUI脚本
    /// </summary>
    void GetAllItemUIs()
    {
        print("GET ALL UI");
        weaponItemUIs = GetComponentsInChildren<WeaponItemUI>(true);
        equipmentItemUIs = GetComponentsInChildren<EquipmentItemUI>(true);
        comsumableItemUIs = GetComponentsInChildren<ComsumableItemUI>(true);
    }

    /// <summary>
    /// 加载玩家拥有的武器图标UI
    /// </summary>
    public void LoadWeaponItemUIs()
    {
        if (weaponItemUIs.Length == 0)
        {
            return;
        }
        ResetWeaponItemUIs();
        int i = 0;
        foreach (var item in playerInventory.playerWeaponInventory.Values)
        {
            weaponItemUIs[i].information.SetParent(informationField);//将具体信息的UI父物体变为信息背景物体
            weaponItemUIs[i].information.anchoredPosition = Vector2.zero;//坐标重置
            weaponItemUIs[i].information.SetParent(weaponItemUIs[i].transform);
            //设置UI具体属性
            weaponItemUIs[i].itemID = item.itemID;
            weaponItemUIs[i].fieldIcon.sprite = Resources.Load("Sprites/Weapon/" + item.iconName, typeof(Sprite)) as Sprite;
            weaponItemUIs[i].informationIcon.sprite = Resources.Load("Sprites/Weapon/" + item.iconName, typeof(Sprite)) as Sprite;
            weaponItemUIs[i].name.text = item.itemName;
            weaponItemUIs[i].level.text = item.level.ToString();
            weaponItemUIs[i].ATK.text = item.baseATK.ToString();
            weaponItemUIs[i].description.text = item.description;
            i++;
        }
        UpdateUsingWeaponItem();
    }

    /// <summary>
    /// 加载玩家拥有的装备图标UI
    /// </summary>
    public void LoadEquipmentItemUIs()
    {
        if (equipmentItemUIs.Length == 0)
        {
            return;
        }
        ResetEquipmentItemUIs();
        int i = 0;
        foreach (var item in playerInventory.playerEquipmentInventory.Values)
        {
            equipmentItemUIs[i].information.SetParent(informationField);//将具体信息的UI父物体变为信息背景物体
            equipmentItemUIs[i].information.anchoredPosition = Vector2.zero;//坐标重置
            equipmentItemUIs[i].information.SetParent(equipmentItemUIs[i].transform);
            //设置UI具体属性
            equipmentItemUIs[i].itemID = item.itemID;
            equipmentItemUIs[i].fieldIcon.sprite = Resources.Load("Sprites/Equipment/" + item.iconName, typeof(Sprite)) as Sprite;
            equipmentItemUIs[i].informationIcon.sprite = Resources.Load("Sprites/Equipment/" + item.iconName, typeof(Sprite)) as Sprite;
            equipmentItemUIs[i].name.text = item.itemName;
            equipmentItemUIs[i].level.text = item.level.ToString();
            equipmentItemUIs[i].gain.text = item.baseGain.ToString();
            equipmentItemUIs[i].description.text = item.description;
            i++;
        }
        UpdateUsingEquipmentItem();
    }

    /// <summary>
    /// 加载玩家拥有的装备图标UI
    /// </summary>
    public void LoadComsumableItemUIs()
    {
        if (comsumableItemUIs.Length == 0)
        {
            return;
        }
        ResetComsumableItemUIs();
        int i = 0;
        foreach (var item in playerInventory.playerComsumableInventory.Values)
        {
            comsumableItemUIs[i].information.SetParent(informationField);//将具体信息的UI父物体变为信息背景物体
            comsumableItemUIs[i].information.anchoredPosition = Vector2.zero;//坐标重置
            comsumableItemUIs[i].information.SetParent(comsumableItemUIs[i].transform);
            //设置UI具体属性
            comsumableItemUIs[i].itemID = item.itemID;
            comsumableItemUIs[i].count.text = item.count.ToString();
            comsumableItemUIs[i].fieldIcon.sprite = Resources.Load("Sprites/Comsumable/" + item.iconName, typeof(Sprite)) as Sprite;
            comsumableItemUIs[i].informationIcon.sprite = Resources.Load("Sprites/Comsumable/" + item.iconName, typeof(Sprite)) as Sprite;
            comsumableItemUIs[i].name.text = item.itemName;
            comsumableItemUIs[i].gain.text = item.baseGain.ToString();
            comsumableItemUIs[i].description.text = item.description;
            i++;
        }
        if (currentUsingComsumableItemUI != null)
        {
            comsumableCount.text = currentUsingComsumableItemUI.count.text;
        }
        UpdateUsingComsumableItem();
    }

    /// <summary>
    /// 设置武器所有栏位的图标和信息为默认值
    /// </summary>
    void ResetWeaponItemUIs()
    {
        foreach (var item in weaponItemUIs)
        {
            item.itemID = 0;
            item.fieldIcon.sprite = transparencySprite;
            item.informationIcon.sprite = transparencySprite;
            item.name.text = "";
            item.level.text = "";
            item.ATK.text = "";
            item.description.text = "";
        }

        if (currentSelectWeaponItemUI == null)
        {
            return;
        }
        if (currentSelectWeaponItemUI.name.text == "")
        {
            currentSelectWeaponItemUI.information.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 设置装备所有栏位的图标和信息为默认值
    /// </summary>
    void ResetEquipmentItemUIs()
    {
        foreach (var item in equipmentItemUIs)
        {
            item.itemID = 0;
            item.fieldIcon.sprite = transparencySprite;
            item.informationIcon.sprite = transparencySprite;
            item.name.text = "";
            item.level.text = "";
            item.gain.text = "";
            item.description.text = "";
        }

        if (currentSelectEquipmentItemUI == null)
        {
            return;
        }
        if (currentSelectEquipmentItemUI.name.text == "")
        {
            currentSelectEquipmentItemUI.information.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 设置装备所有栏位的图标和信息为默认值
    /// </summary>
    void ResetComsumableItemUIs()
    {
        foreach (var item in comsumableItemUIs)
        {
            item.itemID = 0;
            item.fieldIcon.sprite = transparencySprite;
            item.informationIcon.sprite = transparencySprite;
            item.name.text = "";
            item.count.text = "0";
            item.gain.text = "";
            item.description.text = "";
        }

        if (currentSelectComsumableItemUI == null)
        {
            return;
        }
        if (currentSelectComsumableItemUI.name.text == "")
        {
            currentSelectComsumableItemUI.information.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 禁用所有栏位的具体信息显示UI
    /// </summary>
    void DisableInformationUIs()
    {
        //武器栏位
        if (currentSelectWeaponItemUI != null)
        {
            currentSelectWeaponItemUI.useButton.PointExit();
            currentSelectWeaponItemUI.dropButton.PointExit();
        }
        foreach (var item in weaponItemUIs)
        {
            item.information.gameObject.SetActive(false);
        }
        //装备栏位
        if (currentSelectEquipmentItemUI != null)
        {
            currentSelectEquipmentItemUI.useButton.PointExit();
            currentSelectEquipmentItemUI.dropButton.PointExit();
        }
        foreach (var item in equipmentItemUIs)
        {
            item.information.gameObject.SetActive(false);
        }
        //消耗品栏位
        if (currentSelectComsumableItemUI != null)
        {
            currentSelectComsumableItemUI.useButton.PointExit();
            currentSelectComsumableItemUI.dropButton.PointExit();
        }
        foreach (var item in comsumableItemUIs)
        {
            item.information.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 打开背包时显示选中栏位的信息
    /// </summary>
    void EnableInformationUIs()
    {
        //武器栏位
        if (currentSelectWeaponItemUI != null && currentSelectWeaponItemUI.name.text != "")
        {
            currentSelectWeaponItemUI.information.gameObject.SetActive(true);
        }
        //装备栏位
        if (currentSelectEquipmentItemUI != null && currentSelectEquipmentItemUI.name.text != "")
        {
            currentSelectEquipmentItemUI.information.gameObject.SetActive(true);
        }
        //消耗品栏位
        if (currentSelectComsumableItemUI != null && currentSelectComsumableItemUI.name.text != "")
        {
            currentSelectComsumableItemUI.information.gameObject.SetActive(true);
        }
    }
}
