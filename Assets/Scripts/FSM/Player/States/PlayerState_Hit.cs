using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Hit", fileName = "PlayerState_Hit")]
public class PlayerState_Hit : PlayerState
{
    [SerializeField] float stateEarlyFinishTime = 0.2f;

    public override void Enter()
    {
        base.Enter();
        PM.playerAnimatorHandler.SetAnimatorValue("Hit");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (StateDuration >= PM.playerAnimatorHandler.AnimationDuration - stateEarlyFinishTime)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Idle));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
