using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ComsumableItemUI : ItemUI, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI gain;//增益值显示文本组件

    public TextMeshProUGUI count;//物品数量显示文本

    public TextMeshProUGUI description;//物品描述文本组件

    public GameObject interactionFrame;//物品交互框对象

    public GameObject usingBackground;//使用中物品背景对象，如果物品是使用中的，则会显示背景

    public ButtonComponent useButton;//使用按钮

    public ButtonComponent dropButton;//丢弃按钮

    ItemUIManager itemUIManager;

    [DisplayOnly] public bool selected = false;//消耗品是否选中状态

    [DisplayOnly] public int itemID;

    [DisplayOnly] public bool itemUsing;//消耗品是否使用状态

    private void Awake()
    {
        InitilizeObject();
    }

    void InitilizeObject()
    {
        itemUIManager = GetComponentInParent<ItemUIManager>();
    }

    /// <summary>
    /// 选中物品
    /// </summary>
    public void Selected()
    {
        //设置当前栏位为选中状态
        selected = true;
        itemUIManager.currentSelectComsumableItemUI = this;
    }

    /// <summary>
    /// 取消选中物品
    /// </summary>
    public void UnSelected()
    {
        //设置当前栏位为取消选中状态
        selected = false;
        itemUIManager.currentSelectComsumableItemUI = null;
    }

    /// <summary>
    /// 启用信息显示UI（选中）
    /// </summary>
    public void EnableInformationUI()
    {
        if (name.text != "")
        {
            information.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 鼠标进入时显示信息UI
    /// </summary>
    public void EnablePointEnterInformationUI()
    {
        if (name.text != "")
        {
            //禁用当前选中的武器信息对象，启用当前鼠标进入信息显示对象
            if (itemUIManager.currentSelectComsumableItemUI != null)
            {
                itemUIManager.currentSelectComsumableItemUI.information.gameObject.SetActive(false);
            }
            information.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 鼠标移出时禁用信息UI
    /// </summary>
    public void DisablePointExitInformationUI()
    {
        if (name.text != "")
        {
            if (itemUIManager.currentSelectComsumableItemUI != null)
            {
                //如果鼠标移出时选中的武器是空的，则不显示信息，否则显示信息
                if (itemUIManager.currentSelectComsumableItemUI.name.text != "")
                {
                    itemUIManager.currentSelectComsumableItemUI.information.gameObject.SetActive(true);
                }
                else
                {
                    itemUIManager.currentSelectComsumableItemUI.information.gameObject.SetActive(false);
                }
            }
            if (selected)
            {
                return;
            }
            information.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 禁用信息显示UI（取消选中）
    /// </summary>
    public void DisableInformationUI()
    {
        if (name.text != "")
        {
            if (selected)
            {
                return;
            }
            information.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnablePointEnterInformationUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DisablePointExitInformationUI();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Selected();
        EnableInformationUI();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        UnSelected();
        DisableInformationUI();
    }
}
