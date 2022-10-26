using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IState
{
    /// <summary>
    /// 动画状态名称
    /// </summary>
    protected string stateName;

    /// <summary>
    /// 动画过渡时间
    /// </summary>
    protected float transitionDuration = 0.1f;

    public eEnemyState enemyState;

    protected EnemyManager EM;

    /// <summary>
    /// 动画是否播放完成
    /// </summary>
    /// <returns></returns>
    protected bool IsAnimationFinished => StateDuration >= EM.enemyAnimatorHandler.animator.GetCurrentAnimatorStateInfo(0).length;

    protected float currentAnimationTime => EM.enemyAnimatorHandler.animator.GetCurrentAnimatorClipInfo(0).Length;

    /// <summary>
    /// 该状态的持续时间
    /// </summary>
    protected float StateDuration => Time.time - stateStartTime;

    /// <summary>
    /// 该状态的进入时间
    /// </summary>
    float stateStartTime;

    public void Initialize(EnemyManager EM)
    {
        this.EM = EM;
    }
    public virtual void Enter()
    {
        EM.enemyAnimatorHandler.animator.CrossFade(stateName, transitionDuration);
        stateStartTime = Time.time;
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicUpdate()
    {
    }

}
