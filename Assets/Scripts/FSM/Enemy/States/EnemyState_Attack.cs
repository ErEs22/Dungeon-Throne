using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Attack : EnemyState
{

    public EnemyState_Attack(string enterAnimName)
    {
        enemyState = eEnemyState.Attack;
        stateName = enterAnimName;
    }
    public override void Enter()
    {
        base.Enter();
        EM.enemyAI.DisableAIMove();
        EM.enemyAI.HandleOrientation();
    }

    public override void Exit()
    {
        base.Exit();
        EM.enemyAI.EnableAIMove();
        EM.enemyAttackHandler.DisableDamageCollider();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (StateDuration >= (currentAnimationTime + EM.enemyAI.waitAfterAttackTime))
        {
            if (EM.enemyAI.CheckRadiusForAttack())
            {
                EM.enemyAI.SetTarget();
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
