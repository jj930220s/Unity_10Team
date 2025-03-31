using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
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

    public void SetStats(float damage, float range, float cooldown, float hp, float speed)
    {
        attackDamage = damage;
        attackRange = range;
        attackCooldown = cooldown;
        health = hp;
        moveSpeed = speed;

        Debug.Log($"SetStats 호출: Damage={attackDamage}, Range={attackRange}, Cooldown={attackCooldown}, Health={health}, Speed={moveSpeed}");
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

        OnDeathEvent?.Invoke(this);
        Debug.Log($"{monsterName}가 사망했습니다");
    }
}