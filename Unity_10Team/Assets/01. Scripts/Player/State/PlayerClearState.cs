using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClearState : PlayerBaseState
{
    public PlayerClearState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void StateEnter()
    {
        Debug.Log("Clear State");
        stateMachine.movementSpeedModifier = 0f;

        base.StateEnter();
        StartAnimation(stateMachine.player.animationData.clearParameterHash);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StateExit()
    {
        base.StateExit();
        StopAnimation(stateMachine.player.animationData.clearParameterHash);
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
