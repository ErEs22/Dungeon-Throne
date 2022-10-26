using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    /// <summary>
    /// 状态进入
    /// </summary>
    public void Enter();

    /// <summary>
    /// 状态退出
    /// </summary>
    public void Exit();

    /// <summary>
    /// 处理逻辑更新
    /// </summary>
    public void LogicUpdate();

    /// <summary>
    /// 处理物理更新
    /// </summary>
    public void PhysicUpdate();
}
