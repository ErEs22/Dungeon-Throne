using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础状态机，可以修改状态以及存储所有状态
/// </summary>
public class StateMachine : MonoBehaviour
{
    [DisplayOnly][SerializeField] string currentStateName;

    public IState currentState;//状态机的当前状态

    protected Dictionary<System.Type, IState> stateDic = new Dictionary<System.Type, IState>();

    void ChangeState(IState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
        currentStateName = currentState.ToString();
    }

    /// <summary>
    /// 更换状态
    /// </summary>
    /// <param name="newState">要更换的状态</param>
    public void ChangeState(System.Type newState)
    {
        ChangeState(stateDic[newState]);
    }

    /// <summary>
    /// 进行当前状态的逻辑更新
    /// </summary>
    private void Update()
    {
        currentState.LogicUpdate();
    }

    /// <summary>
    /// 进行当前状态的物理更新
    /// </summary>
    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }
}
