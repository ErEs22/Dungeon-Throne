using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Idle : EnemyState
{
    public EnemyState_Idle(string enterStateName)
    {
        enemyState = eEnemyState.Idle;
        stateName = enterStateName;
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (EM.enemyAI.CheckRadiusForAttack())
        {
            EM.enemyStateMachine.ChangeState(eEnemyState.Attack);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
