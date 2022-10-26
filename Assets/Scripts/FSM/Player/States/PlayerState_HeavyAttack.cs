using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/HeavyAttack", fileName = "PlayerState_HeavyAttack")]
public class PlayerState_HeavyAttack : PlayerState
{
    [SerializeField] float attackEarlyFinishTime = 0.2f;

    public override void Enter()
    {
        base.Enter();
        if (PM.playerAttackHandler.currentWeapon == null)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Idle));
            return;
        }
        if (PM.playerStats.currentStamina < 10)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Idle));
            return;
        }
        PM.playerInputHandler.DisableAllInput();
        PM.weaponSlotManager.weaponSlot.GetComponentInChildren<DamageCollider>().damage = PM.playerStats.currentHeavyATK;
        PM.playerAnimatorHandler.SetAnimatorValue(enterAnName);
        if (PM.playerAttackHandler.currentWeapon != null)
        {
            PM.playerStats.CostStamina((int)(PM.playerAttackHandler.currentWeapon.baseStamina * PM.playerAttackHandler.currentWeapon.baseStaminaHeavyMultiplier));
            PM.playerStats.recoverStaminaCountDown = PM.playerStats.staminaRecoverTime;
        }
    }

    public override void Exit()
    {
        base.Exit();
        PM.playerInputHandler.EnableAllInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (StateDuration >= PM.playerAnimatorHandler.AnimationDuration - attackEarlyFinishTime)
        {
            PM.playerStateMachine.ChangeState(typeof(PlayerState_Idle));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        PM.playerController.DecelerateXVelocity();
    }
}
