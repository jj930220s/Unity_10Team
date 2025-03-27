using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotState : PlayerAttackState
{
    public PlayerShotState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void StateEnter()
    {
        Debug.Log("Shot State");

        base.StateEnter();
        stateMachine.player.animator.SetTrigger("Shot");
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StateExit()
    {
        base.StateExit();
    }

    public override void StateHandleInput()
    {
        base.StateHandleInput();
    }

    public override void StatePhysicsUpdate()
    {
        base.StatePhysicsUpdate();
    }
}
