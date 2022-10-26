using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Idle", fileName = "PlayerState_Idle")]
public class PlayerState_Idle : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        PM.playerController.velocityChangeDuration = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (PM.playerInputHandler.Moving)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Move));
        }
        if (PM.playerInputHandler.LightAttack)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_LightAttack));
        }
        if (PM.playerInputHandler.HeavyAttack)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_HeavyAttack));
        }
        if (PM.playerInputHandler.Roll)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Roll));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        PM.playerController.DecelerateXVelocity();
    }
}
