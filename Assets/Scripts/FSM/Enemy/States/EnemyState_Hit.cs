using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Hit : EnemyState
{
    public EnemyState_Hit(string enterStateName)
    {
        this.stateName = enterStateName;
        enemyState = eEnemyState.Hit;
    }
    public override void Enter()
    {
        base.Enter();
        EM.enemyAttackHandler.DisableDamageCollider();
        EM.enemyAI.DisableAIMove();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (IsAnimationFinished)
        {
            if (EM.enemyAI.CheckRadiusForAttack())
            {
                EM.enemyStateMachine.ChangeState(eEnemyState.Attack);
            }
            else if (EM.enemyAI.CheckPlayerAround_Chase())
            {
                EM.enemyStateMachine.ChangeState(eEnemyState.Chase);
            }
            else
            {
                EM.enemyStateMachine.ChangeState(eEnemyState.Guard);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
