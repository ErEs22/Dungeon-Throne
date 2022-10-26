using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家状态基类
/// </summary>
public class PlayerState : ScriptableObject, IState
{
    [SerializeField] protected string enterAnName;//进入状态的动画名称

    protected PlayerManager PM;

    public float StateDuration => Time.time - stateEnterTime;//状态持续时间

    [DisplayOnly] public float stateEnterTime;//状态进入的时间

    /// <summary>
    /// 初始化赋值
    /// </summary>
    /// <param name="playerController">玩家控制器</param>
    /// <param name="playerInputHandler">玩家输入处理器</param>
    /// <param name="playerStateMachine">玩家状态机</param>
    public void Initilize
    (
        PlayerManager PM
    )
    {
        this.PM = PM;
    }

    public virtual void Enter()
    {
        //设置状态进入时间
        stateEnterTime = Time.time;
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
        PM.playerAnimatorHandler.UpdateMovementParameters(Mathf.Abs(PM.playerController.HandleXMoveInput()), 0);
    }

    public virtual void PhysicUpdate()
    {
    }
}
