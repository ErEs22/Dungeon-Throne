using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : PersistentSingleton<EventManager>
{
    /// <summary>
    /// 丢弃武器事件
    /// </summary>
    public event UnityAction dropWeaponItem;

    /// <summary>
    /// 装备武器事件
    /// </summary>
    public event UnityAction useWeaponItem;

    /// <summary>
    /// 丢弃装备事件
    /// </summary>
    public event UnityAction dropEquipmentItem;

    /// <summary>
    /// 装备装备事件
    /// </summary>
    public event UnityAction useEquipmentItem;

    /// <summary>
    /// 丢弃消耗品事件
    /// </summary>
    public event UnityAction dropComsumableItem;

    /// <summary>
    /// 装备消耗品事件
    /// </summary>
    public event UnityAction useComsumableItem;

    /// <summary>
    /// 暂停游戏事件
    /// </summary>
    public event UnityAction pause;

    /// <summary>
    /// 恢复游戏事件
    /// </summary>
    public event UnityAction resume;

    public event UnityAction disableAllInput;

    public event UnityAction enableAllInput;

    public event UnityAction disableGamePauseInput;

    public event UnityAction enableGamePauseInput;

    public event UnityAction resumeScaleTime;

    public event UnityAction onGameFinished;

    public void OnGameFinished()
    {
        onGameFinished.Invoke();
    }

    public void ResumeScaleTime()
    {
        resumeScaleTime.Invoke();
    }

    public void DisableAllInput()
    {
        disableAllInput.Invoke();
    }

    public void EnableAllInput()
    {
        enableAllInput.Invoke();
    }

    public void DisableGamePauseInput()
    {
        disableGamePauseInput.Invoke();
    }

    public void EnableGamePauseInput()
    {
        enableGamePauseInput.Invoke();
    }

    /// <summary>
    /// 触发装备武器事件
    /// </summary>
    public void UseWeaponItem()
    {
        useWeaponItem.Invoke();
    }

    /// <summary>
    /// 触发丢弃武器事件
    /// </summary>
    public void DropWeaponItem()
    {
        dropWeaponItem.Invoke();
    }

    /// <summary>
    /// 触发装备装备事件
    /// </summary>
    public void UseEquipmentItem()
    {
        useEquipmentItem.Invoke();
    }

    /// <summary>
    /// 触发丢弃装备事件
    /// </summary>
    public void DropEquipmentItem()
    {
        dropEquipmentItem.Invoke();
    }

    /// <summary>
    /// 触发装备消耗品事件
    /// </summary>
    public void UseComsumableItem()
    {
        useComsumableItem.Invoke();
    }

    /// <summary>
    /// 触发丢弃消耗品事件
    /// </summary>
    public void DropComsumableItem()
    {
        dropComsumableItem.Invoke();
    }

    /// <summary>
    /// 触发暂停游戏事件
    /// </summary>
    public void Pause()
    {
        pause.Invoke();
    }

    /// <summary>
    /// 触发恢复游戏事件
    /// </summary>
    public void Resume()
    {
        resume.Invoke();
    }
}
