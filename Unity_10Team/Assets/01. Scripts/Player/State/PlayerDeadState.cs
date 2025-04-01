using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void StateEnter()
    {
        Debug.Log("Dead State");
        stateMachine.movementSpeedModifier = 0f;

        base.StateEnter();
        StartAnimation(stateMachine.player.animationData.deadParameterHash);
        UIManager.Instance.PopUpUI(UITYPE.GAMEOVER);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StateExit()
    {
        base.StateExit();
        StopAnimation(stateMachine.player.animationData.deadParameterHash);
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
