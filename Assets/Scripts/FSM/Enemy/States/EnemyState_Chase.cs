using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Chase : EnemyState
{
    public EnemyState_Chase(string enterStateName)
    {
        enemyState = eEnemyState.Chase;
        stateName = enterStateName;
    }
    public override void Enter()
    {
        base.Enter();
        EM.enemyAI.SetTarget();
        EM.enemyAI.EnableAIMove();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (EM.enemyAI.CheckPlayerAround_Chase())
        {
            if (EM.enemyAI.CheckRadiusForAttack())
            {
                EM.enemyStateMachine.ChangeState(eEnemyState.Attack);
            }
        }
        else
        {
            EM.enemyStateMachine.ChangeState(eEnemyState.Guard);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
