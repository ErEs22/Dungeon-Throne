using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家的状态机，具有单一性，不适用于敌人这种数量众多的对象
/// </summary>
public class PlayerStateMachine : StateMachine
{
    [SerializeField] PlayerState[] states;///玩家的所有状态

    PlayerManager PM;

    private void Awake()
    {
        InitilizeObject();
    }

    private void Start()
    {
        InitilizeState();
    }

    /// <summary>
    /// 初始化类中的对象
    /// </summary>
    void InitilizeObject()
    {
        PM = GetComponent<PlayerManager>();

        foreach (PlayerState state in states)
        {
            state.Initilize(PM);
            stateDic.Add(state.GetType(), state);
        }
    }

    /// <summary>
    /// 初始化状态
    /// </summary>
    void InitilizeState()
    {
        currentState = stateDic[typeof(PlayerState_Idle)];
        currentState.Enter();
    }
}
