using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Guard : EnemyState
{
    public EnemyState_Guard(string enterStateName)
    {
        enemyState = eEnemyState.Guard;
        stateName = enterStateName;
    }

    public override void Enter()
    {
        base.Enter();
        EM.enemyAI.EnableAIMove();
        EM.enemyAI.StartPatrolCoroutine();
    }

    public override void Exit()
    {
        base.Exit();
        EM.enemyAI.StopPatrolCoroutine();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (EM.enemyAI.CheckPlayerAround_Guard())
        {
            EM.enemyStateMachine.ChangeState(eEnemyState.Chase);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

}
