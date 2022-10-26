using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    Animator anim;

    PlayerManager PM;

    public float AnimationDuration => anim.GetCurrentAnimatorClipInfo(0).Length;//获取当前动画状态的时长

    private void Awake()
    {
        InitilizeObject();
    }

    void InitilizeObject()
    {
        anim = GetComponent<Animator>();
        PM = GetComponentInParent<PlayerManager>();
    }

    public void StartRolling()
    {
        PM.playerController.ChangeToRollingLayer();
    }

    public void EndRolling()
    {
        PM.playerController.ChangeToPlayerLayer();
    }

    /// <summary>
    /// 更新动画器移动参数
    /// </summary>
    /// <param name="x">X轴</param>
    /// <param name="y">Y轴</param>
    public void UpdateMovementParameters(float x, float y)
    {
        anim.SetFloat("MoveSpeed", x);
    }

    /// <summary>
    /// 设置动画器参数值（触发器类参数）
    /// </summary>
    /// <param name="stateName">动画状态触发器名称</param>
    public void SetAnimatorValue(string stateName)
    {
        anim.SetTrigger(stateName);
    }

    /// <summary>
    /// 设置动画器参数（布尔类参数）
    /// </summary>
    /// <param name="stateName">动画状态名称</param>
    /// <param name="value">目标值</param>
    public void SetAnimatorValue(string stateName, bool value)
    {
        anim.SetBool(stateName, value);
    }
}
