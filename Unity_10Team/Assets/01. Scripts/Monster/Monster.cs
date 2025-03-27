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
            Debug.LogError(" monsterData�� �������� �ʾҽ��ϴ�");
        }
    }

    private void OnDisable()
    {
        MonsterSpawner spawner = MonsterSpawner.Instance;

        if (spawner == null)
        {
            Debug.LogError("MonsterSpawner�� ã�� �� ����");
            return;
        }
        OnDisableEvent?.Invoke(this);
        spawner.ReturnMonster(this);
        Debug.Log($"{gameObject.name} ��Ȱ��ȭ��");
    }

    // ���� ���� ����
    public void SetAttacking(bool isAttacking)
    {
        animator.SetBool("isAttack", isAttacking);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); //����
        }
    }

    private void Die()
    {
        OnDisableEvent?.Invoke(this);
        gameObject.SetActive(false);
        Debug.Log($"{monsterName}�� ����߽��ϴ�");
    }
}
