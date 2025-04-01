using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    private Coroutine CshotCoroutine;

    float horizontal = 0f;
    float vertical = 0f;

    public override void StateEnter()
    {
        Debug.Log("Attack State");
        stateMachine.movementSpeedModifier = attackData.attackMoveSpeedModifier;

        base.StateEnter();
        StartAnimation(stateMachine.player.animationData.attackParameterHash);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        Deadcheck();
        AttackMoveAnimationBlend();
        Move();
    }

    public override void StateExit()
    {
        base.StateExit();
        StopAnimation(stateMachine.player.animationData.attackParameterHash);
    }

    public override void StateHandleInput()
    {
        base.StateHandleInput();
    }

    public override void StatePhysicsUpdate()
    {
        base.StatePhysicsUpdate();
    }

    protected override void Move()
    {
        Vector3 movementDirection = GetMovementDirection();
        Vector3 attackDirection = GetAttackDirection(GetAttackPoint());

        Move(movementDirection);
        Rotate(attackDirection);
    }

    private Vector3 GetAttackDirection(Vector3 attackPoint)
    {
        return (attackPoint - stateMachine.player.transform.position).normalized;
    }

    private Vector3 GetAttackPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 groundPoint;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))// 임시 레이어 마스크
        {
            groundPoint = hit.point;
        }
        else
        {
            Debug.Log("지면 검출 실패");
            return Vector3.zero;
        }
        return groundPoint;
    }

    private void AttackMoveAnimationBlend()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 moveInputDir = new Vector3(horizontal, 0f, vertical);

        Vector3 playerLookDir = stateMachine.player.transform.InverseTransformDirection(moveInputDir);

        stateMachine.player.animator.SetFloat(stateMachine.player.animationData.horizontalParameterHash, playerLookDir.x);
        stateMachine.player.animator.SetFloat(stateMachine.player.animationData.verticalParameterHash, playerLookDir.z);
    }

    protected override void OnAttackPerformed(InputAction.CallbackContext context)
    {
        base.OnAttackPerformed(context);
        if (CshotCoroutine == null)
        {
            CshotCoroutine = stateMachine.player.StartCoroutine(RepeatedShot());
        }
    }

    protected override void OnAttackCanceled(InputAction.CallbackContext context)
    {
        base.OnAttackCanceled(context);

        if (CshotCoroutine != null)
        {
            stateMachine.player.StopCoroutine(CshotCoroutine);
            CshotCoroutine = null;
        }

        if (stateMachine.movementInput == Vector2.zero)
        {
            stateMachine.ChangeState(stateMachine.idleState);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.moveState);
        }
    }

    private void ShotBullet()
    {
        Bullet bullet = stateMachine.player.bulletPool.Get();

        if (bullet != null)
        {
            if (bullet.player == null)
            {
                bullet.player = stateMachine.player;
            }

            bullet.transform.position = stateMachine.player.shotPoint.position;
            Vector3 dir = (GetAttackPoint() - stateMachine.player.shotPoint.position).normalized;
            dir.y = 0f;
            bullet.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private IEnumerator RepeatedShot()
    {
        while (true)
        {
            stateMachine.player.animator.SetTrigger("Shot");

            ShotBullet();
            yield return new WaitForSeconds(playerStatus.status[STATTYPE.ATKDELAY]);
        }
    }
}
