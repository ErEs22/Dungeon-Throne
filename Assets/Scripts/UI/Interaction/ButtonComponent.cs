using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// 交互的文本组件
    /// </summary>
    [SerializeField] TextMeshProUGUI text;

    public void PointExit()
    {
        text.color = Color.black;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        text.color = new Color(255, 216, 76);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
    }
}
