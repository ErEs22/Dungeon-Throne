using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State/PlayerState/Death", fileName = "PlayerState_Death")]
public class PlayerState_Death : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        PM.playerAnimatorHandler.SetAnimatorValue("Death");
        PM.playerStats.DieLater();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
