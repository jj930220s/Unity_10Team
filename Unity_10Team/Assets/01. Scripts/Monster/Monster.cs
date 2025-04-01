using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using UnityEngine;

public class Monster : Singleton<Monster>
{
    public Animator animator;
    public MonsterData monsterData;
    private MonsterAttack attack;
    public Transform target;

    public string monsterName;
    public MonsterType monsterType;
    public AttackType attackType;
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    public float health;
    public float moveSpeed;
    public bool isDead;
    public GameObject projectilePrefab;

    public delegate void OnDeathDelegate(Monster monster);
    public event OnDeathDelegate OnDeathEvent;

    public bool isInitialized = false;
    public Vector3 dropOffset;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        attack = GetComponent<MonsterAttack>();
    }

    private void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        attack.PerformAttack();
    }

    public void Initialize()
    {
        if (isInitialized) return;

        if (monsterData != null)
        {
            monsterName = monsterData.monsterName;
            monsterType = monsterData.monsterType;
            attackType = monsterData.attackType;
            attackDamage = monsterData.attackDamage;
            attackRange = monsterData.attackRange;
            attackCooldown = monsterData.attackCooldown;
            health = monsterData.health;
            moveSpeed = monsterData.moveSpeed;
            isDead = false;
            projectilePrefab = monsterData.projectilePrefab;
        }
        else
        {
            Debug.LogWarning(" monsterData가 설정되지 않았습니다");
        }

        isInitialized = true;
    }

    public void SetStats(float damage, float range, MonsterType type, float hp, float speed)
    {
        monsterName = monsterData.monsterName;
        monsterType = type;
        attackType = monsterData.attackType;
        attackDamage = damage;
        attackRange = range;
        attackCooldown = monsterData.attackCooldown;
        health = hp;
        moveSpeed = speed;
        isDead = false;
        projectilePrefab = monsterData.projectilePrefab;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    // 공격 상태 변경
    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttack", isAttacking);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    [ContextMenu("Kill Monster")]
    private void KillMonster()
    {
        //테스트용
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        Debug.Log("Die() called, invoking OnDeathEvent");

        animator.SetBool("isDie", true);

        isDead = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        OnDeathEvent?.Invoke(this); // <이걸로 경험치도 획득하게 하면 될듯?
        Debug.Log($"{monsterName}가 사망했습니다");
    }
}