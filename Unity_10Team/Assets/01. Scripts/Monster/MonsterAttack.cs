using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Monster monster;

    private float lastMeleeAttackTime;
    private float lastRangedAttackTime;

    private void Start()
    {
        monster = GetComponent<Monster>();
    }

    // ���� ���� �޼���
    private void PerformMeleeAttack()
    { 
        if (Time.time - lastMeleeAttackTime >= monster.attackCooldown)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(monster.transform.position, monster.attackRange);

            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Player"))
                {
                    monster.SetAttacking(true);
                    float attackAnimationLength = monster.animator.GetCurrentAnimatorStateInfo(0).length;
                    StartCoroutine(PerformMeleeAttackAfterDelay(attackAnimationLength));
                    lastMeleeAttackTime = Time.time;
                }
            }
        }
    }

    private void PerformRangedAttack()
    {
        if (Time.time - lastRangedAttackTime >= monster.attackCooldown)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(monster.transform.position, monster.attackRange);
            foreach (var enemy in hitEnemies)
            {
                if (enemy.CompareTag("Player"))
                {
                    Vector3 spawnPosition = monster.transform.position + Vector3.up * 1.5f; // ��¦ ������ �߻�

                    GameObject projectile = Instantiate(monster.monsterData.projectilePrefab, spawnPosition, Quaternion.identity);

                    Vector3 direction = (monster.target.position - spawnPosition).normalized;

                    projectile.GetComponent<MonsterProjectile>().Launch(direction, monster.attackDamage);
                    lastRangedAttackTime = Time.time;
                }
            }
        }  
    }

    public void PerformAttack()
    {
        if (monster != null && monster.gameObject.activeInHierarchy)
        {
            if (monster.attackType == AttackType.Melee)
            {
                PerformMeleeAttack();  // ���� ����
            }
            else if (monster.attackType == AttackType.Ranged)
            {
                PerformRangedAttack();  // ���Ÿ� ����
            }
        }
    }

    private IEnumerator PerformMeleeAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���⿡ �÷��̾� ������ ����

        Debug.Log($"{monster.monsterName}�� {monster.attackDamage}�� ���ظ� �������ϴ�!");

        monster.SetAttacking(false);
    }
}
