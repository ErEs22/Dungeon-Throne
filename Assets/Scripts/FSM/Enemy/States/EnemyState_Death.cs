using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState_Death : EnemyState
{
    public EnemyState_Death(string enterAnimName)
    {
        enemyState = eEnemyState.Death;
        stateName = enterAnimName;
    }
    public override void Enter()
    {
        base.Enter();
        EM.enemyStats.Dead = true;
        EM.enemyAI.DisableAIMove();
        EM.enemyAI.ShutDownPhysics();
        EM.enemyStats.DieLater();
        EM.enemyDropHandler.DropWeapons();
        EM.enemyDropHandler.DropEquipments();
        EM.enemyDropHandler.DropComsumables();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        EM.enemyAI.DisableAIMove();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
