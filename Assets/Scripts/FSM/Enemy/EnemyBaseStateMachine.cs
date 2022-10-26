using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseStateMachine : MonoBehaviour
{
    [DisplayOnly][SerializeField] string currentStateName;

    public string CurrentStateName => currentStateName;

    /// <summary>
    /// 当前状态
    /// </summary>
    protected IState currentState;

    /// <summary>
    /// 存储该敌人所有状态的字典
    /// </summary>
    protected Dictionary<eEnemyState, IState> stateDic;
    private void Update()
    {
        currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        currentState.PhysicUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        currentState = newState;
        currentState.Enter();
        currentStateName = currentState.ToString();
    }

    public void SwitchState(IState newState)
    {
        currentState.Exit();
        SwitchOn(newState);
    }
    public void ChangeState(eEnemyState stateType)
    {
        SwitchState(stateDic[stateType]);
    }
}
