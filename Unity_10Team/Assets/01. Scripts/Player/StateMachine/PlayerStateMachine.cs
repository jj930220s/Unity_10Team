using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public Player player { get; }

    public PlayerIdleState idleState { get; }
    public PlayerMoveState moveState { get; }

    public PlayerAttackState attackState { get; }

    public PlayerDeadState deadState { get; }

    public PlayerClearState clearState { get; }

    public Vector2 movementInput { get; set; }
    public float movementSpeed { get; private set; }
    public float rotationDamping { get; private set; }
    public float movementSpeedModifier { get; set; } = 1f;
    public float attackMovementSpeedModifier { get; set; } = 0.5f;

    public PlayerStateMachine(Player player)
    {
        this.player = player;

        idleState = new PlayerIdleState(this);
        moveState = new PlayerMoveState(this);
        attackState = new PlayerAttackState(this);
        deadState = new PlayerDeadState(this);
        clearState = new PlayerClearState(this);

        movementSpeed = player.pStat.status[STATTYPE.SPEED];
        attackMovementSpeedModifier = player.data.attackData.attackMoveSpeedModifier;
        rotationDamping = player.data.defaultData.baseRotationDamping;
    }
}
