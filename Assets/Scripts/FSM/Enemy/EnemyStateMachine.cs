using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : EnemyBaseStateMachine
{
    /// <summary>
    /// 该敌人的所有状态
    /// </summary>
    [SerializeField] protected List<EnemyState> states;

    /// <summary>
    /// 将为该敌人添加的所有状态
    /// </summary>
    [SerializeField] List<eEnemyState> allStates;

    /// <summary>
    /// 敌人
    /// </summary>
    EnemyManager EM;

    protected virtual void Awake()
    {
        InitializeObject();
        InitializeStates();
    }

    void InitializeObject()
    {
        EM = GetComponentInChildren<EnemyManager>();
        states = new List<EnemyState>();
        stateDic = new Dictionary<eEnemyState, IState>();
    }

    private void Start()
    {
        SwitchOn(stateDic[eEnemyState.Guard]);
    }

    /// <summary>
    /// 初始化，将所有状态添加到字典中
    /// </summary>
    protected virtual void InitializeStates()
    {
        //将枚举列表中的状态类型创建并加入到状态传递列表中
        foreach (var state in allStates)
        {
            states.Add(CreateEnemyState(state));
        }
        //将状态传递列表中的状态加入到该敌人的状态字典中
        foreach (var state in states)
        {
            state.Initialize(EM);
            stateDic.Add(state.enemyState, state);
        }
    }

    /// <summary>
    /// 创建状态
    /// </summary>
    /// <param name="enemyState">创建的目标状态</param>
    /// <returns>创建的状态</returns>
    EnemyState CreateEnemyState(eEnemyState enemyState)
    {
        switch (enemyState)
        {
            case eEnemyState.Idle:
                return new EnemyState_Idle(EM.enemyAnimatorHandler.enterAnimName_Idle);
            case eEnemyState.Guard:
                return new EnemyState_Guard(EM.enemyAnimatorHandler.enterAnimName_Guard);
            case eEnemyState.Chase:
                return new EnemyState_Chase(EM.enemyAnimatorHandler.enterAnimName_Chase);
            case eEnemyState.Attack:
                return new EnemyState_Attack(EM.enemyAnimatorHandler.enterAnimName_Attack);
            case eEnemyState.Hit:
                return new EnemyState_Hit(EM.enemyAnimatorHandler.enterAnimName_Hit);
            case eEnemyState.Death:
                return new EnemyState_Death(EM.enemyAnimatorHandler.enterAnimName_Death);
            default: return null;
        }
    }
}
