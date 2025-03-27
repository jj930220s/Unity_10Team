using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerDefaultData defaultData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        defaultData = this.stateMachine.player.data.defaultData;
    }
    public virtual void StateEnter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void StateUpdate()
    {
        Move();
    }

    public virtual void StateExit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void StateHandleInput()
    {
        ReadMovementInput();
    }

    public virtual void StatePhysicsUpdate()
    {
    }

    protected void StartAnimation(int animationHash)
    {
        stateMachine.player.animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        stateMachine.player.animator.SetBool(animationHash, false);
    }

    private void ReadMovementInput()
    {
        stateMachine.movementInput = stateMachine.player.inputController.playerActions.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);
        Rotate(movementDirection);
    }
    private void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.player.characterController.Move((direction * movementSpeed) * Time.deltaTime);
    }

    private Vector3 GetMovementDirection()
    {
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.movementInput.y + right * stateMachine.movementInput.x;
    }

    private float GetMovementSpeed()
    {
        float moveSpeed = stateMachine.movementSpeed * stateMachine.movementSpeedModifier;
        return moveSpeed;
    }

    private void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.rotationDamping * Time.deltaTime);
        }
    }

    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.player.inputController;
        input.playerActions.Move.canceled += OnMoveCanceled;
        //input.playerActions.Attack.started += OnAttackStarted;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.player.inputController;
        input.playerActions.Move.canceled -= OnMoveCanceled;
        //input.playerActions.Attack.started -= OnAttackStarted;
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void OnAttackStarted(InputAction.CallbackContext context)
    {

    }
}
