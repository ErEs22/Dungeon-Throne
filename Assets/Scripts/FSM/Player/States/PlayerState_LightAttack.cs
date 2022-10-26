using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/LightAttack", fileName = "PlayerState_LightAttack")]
public class PlayerState_LightAttack : PlayerState
{
    [SerializeField] float attackEarlyFinishTime = 0.2f;

    public override void Enter()
    {
        base.Enter();
        if (PM.playerAttackHandler.currentWeapon.itemName == "")
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
        PM.weaponSlotManager.weaponSlot.GetComponentInChildren<DamageCollider>().damage = PM.playerStats.currentLightATK;
        PM.playerAnimatorHandler.SetAnimatorValue(enterAnName);
        if (PM.playerAttackHandler.currentWeapon != null)
        {
            PM.playerStats.CostStamina((int)(PM.playerAttackHandler.currentWeapon.baseStamina * PM.playerAttackHandler.currentWeapon.baseStaminaLightMultiplier));
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
