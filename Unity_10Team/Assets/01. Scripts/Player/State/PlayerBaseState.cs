using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerDefaultData defaultData;
    protected readonly PlayerAttackData attackData;
    protected readonly PlayerStatus playerStatus;

    [Header("�� Ž�� ����")]
    protected float searchRadius = 10f; //Ž���ݰ�
    protected LayerMask enemyLayer; //�����̾�
    protected LayerMask obstacleLayer; //��ֹ����̾�
    protected GameObject currentTarget; //����Ÿ��

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        defaultData = this.stateMachine.player.data.defaultData;
        attackData = this.stateMachine.player.data.attackData;
        playerStatus = this.stateMachine.player.pStat;

        //�����̾��
        enemyLayer = LayerMask.GetMask("Enemy");
        obstacleLayer = LayerMask.GetMask("Obstacle");
    }

    public virtual void StateEnter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void StateUpdate()
    {
        FindTarget();
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

    protected virtual void Move()
    {
        Vector3 movementDirection = GetMovementDirection();

        Move(movementDirection);
        Rotate(movementDirection);
    }

    protected void Move(Vector3 direction)
    {
        float movementSpeed = GetMovementSpeed();

        stateMachine.player.characterController.Move((direction * movementSpeed) * Time.deltaTime);
    }

    protected Vector3 GetMovementDirection()
    {
        Vector3 forward = stateMachine.player.mainCameraTransform.forward;
        Vector3 right = stateMachine.player.mainCameraTransform.right;

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

    protected void Rotate(Vector3 direction)
    {
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Transform playerTransform = stateMachine.player.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, stateMachine.rotationDamping * Time.deltaTime);
        }
    }


    public void Deadcheck()
    {
        if (playerStatus.status[STATTYPE.CHP] <= 0)
        {
            stateMachine.ChangeState(stateMachine.deadState);
        }
    }


    protected virtual void AddInputActionsCallbacks()
    {
        PlayerController input = stateMachine.player.inputController;
        input.playerActions.Move.canceled += OnMoveCanceled;
        input.playerActions.Attack.started += OnAttackStarted;
        input.playerActions.Attack.performed += OnAttackPerformed;
        input.playerActions.Attack.canceled += OnAttackCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerController input = stateMachine.player.inputController;
        input.playerActions.Move.canceled -= OnMoveCanceled;
        input.playerActions.Attack.started -= OnAttackStarted;
        input.playerActions.Attack.performed -= OnAttackPerformed;
        input.playerActions.Attack.canceled -= OnAttackCanceled;
    }

    protected virtual void OnMoveCanceled(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnAttackStarted(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnAttackPerformed(InputAction.CallbackContext context)
    {
    }

    protected virtual void OnAttackCanceled(InputAction.CallbackContext context)
    {
    }

    //���� ã�Ƽ� ����Ÿ��
    protected void FindTarget()
    {
        GameObject nearestEnemy = FindNearestVisibleEnemy();

        if (nearestEnemy != null)
        {
            currentTarget = nearestEnemy;
            Debug.Log("�� Ž��: " + currentTarget.name);
        }
        else
        {
            currentTarget = null;
        }
    }

    
    //���� ����� ���� ã���鼭 ���̿� ��ֹ��� ������ ���Ӱ� Ž��
    protected GameObject FindNearestVisibleEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(stateMachine.player.transform.position, searchRadius, enemyLayer);
        List<GameObject> visibleEnemies = new List<GameObject>();
        float minDistance = Mathf.Infinity;
        GameObject closest = null;
        Vector3 currentPosition = stateMachine.player.transform.position;

        foreach (Collider collider in colliders)
        {
            Vector3 direction = (collider.transform.position - currentPosition).normalized;
            float distance = Vector3.Distance(currentPosition, collider.transform.position);

            // ��ֹ� üũ
            if (!Physics.Raycast(currentPosition, direction, distance, obstacleLayer))
            {
                visibleEnemies.Add(collider.gameObject);
            }
        }

        //���� ����� �� ã��
        foreach (GameObject enemy in visibleEnemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
}
