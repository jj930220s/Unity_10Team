using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator animator;
    public MonsterData monsterData;
    private MonsterAttack attack;

    public string monsterName;
    public MonsterType monsterType;
    public AttackType attackType;
    public float attackDamage;
    public float attackRange;
    public float attackCooldown;
    public float health;
    public float moveSpeed;

    public delegate void OnDisableDelegate(Monster monster);
    public event OnDisableDelegate OnDisableEvent;

    public delegate void OnDeathDelegate(Monster monster);
    public event OnDeathDelegate OnDeathEvent;

    private void Start()
    {
        Initialize();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        attack = GetComponent<MonsterAttack>();
    }

    private void Update()
    {
        attack.PerformAttack();
    }

    public void Initialize()
    {
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
        }
        else
        {
            Debug.LogError(" monsterData가 설정되지 않았습니다");
        }
    }

    public void SetStats(int damage, int range, int cooldown, int hp, int speed)
    {
        attackDamage = damage;
        attackRange = range;
        attackCooldown = cooldown;
        health = hp;
        moveSpeed = speed;
        
    }

    private void OnDisable()
    {
        MonsterSpawner spawner = MonsterSpawner.Instance;

        if (spawner == null)
        {
            Debug.LogError("MonsterSpawner를 찾을 수 없음");
            return;
        }
        OnDisableEvent?.Invoke(this);
        spawner.ReturnMonster(this);
        Debug.Log($"{gameObject.name} 비활성화됨");
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
            Die(); //죽음
        }
    }

    [ContextMenu("Kill Monster")]
    private void Die()
    {
        Debug.Log("Die() called, invoking OnDeathEvent");
        OnDeathEvent?.Invoke(this);
        gameObject.SetActive(false);
        Debug.Log($"{monsterName}가 사망했습니다");
    }
}