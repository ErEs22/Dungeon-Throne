using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Roll", fileName = "PlayerState_Roll")]
public class PlayerState_Roll : PlayerState
{
    [SerializeField] float stateEarlyFinishTime = 0.2f;

    [SerializeField] int staminaCost = 20;

    public override void Enter()
    {
        base.Enter();
        if (PM.playerStats.currentStamina < 10)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Idle));
            return;
        }
        PM.playerController.StopPlayer();
        PM.playerAnimatorHandler.SetAnimatorValue(enterAnName);
        PM.playerController.HandleRoll();
        PM.playerInputHandler.DisableAllInput();
        PM.playerStats.CostStamina(staminaCost);
        PM.playerStats.recoverStaminaCountDown = 2f;
    }

    public override void Exit()
    {
        base.Exit();
        PM.playerInputHandler.EnableAllInput();
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
