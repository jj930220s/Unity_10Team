using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Monster monster;
    private EnemyAI ai;
    public Transform handTransform;

    private float lastMeleeAttackTime;
    private float lastRangedAttackTime;

    public ObjectPool<MonsterProjectile> projectilePool;

    private void Start()
    {
        monster = GetComponent<Monster>();
        ai = GetComponent<EnemyAI>();

        if(monster.attackType == AttackType.Ranged)
        {
            MonsterProjectile projectiles = monster.projectilePrefab.GetComponent<MonsterProjectile>();
            projectilePool = new ObjectPool<MonsterProjectile>(projectiles, 5, transform);
        }
    }

    private void PerformMeleeAttack()
    {
        if (Vector3.Distance(monster.transform.position, monster.target.position) <= ai.agent.stoppingDistance)
        {
            if (Time.time - lastMeleeAttackTime >= monster.attackCooldown)
            {
                if (monster.target != null)
                {
                    monster.SetAttacking(true);
                    lastMeleeAttackTime = Time.time;

                    StartCoroutine(HandleAttackAfterAnimation());
                }
            }
        }
    }

    private void PerformRangedAttack()
    {
        if (Vector3.Distance(monster.transform.position, monster.target.position) <= ai.agent.stoppingDistance)
        {
            if (Time.time - lastRangedAttackTime >= monster.attackCooldown)
            {
                if (monster.target != null)
                {
                    Vector3 spawnPosition = monster.transform.TransformPoint(handTransform.localPosition);

                    MonsterProjectile projectile = projectilePool.Get();

                    projectile.Initialize(projectilePool);
                    projectile.transform.position = spawnPosition;
                    projectile.transform.rotation = Quaternion.identity;

                    Vector3 direction = (monster.target.position - spawnPosition).normalized;
                    projectile.Launch(direction, monster.attackDamage);

                    monster.SetAttacking(true);
                    lastRangedAttackTime = Time.time;

                    StartCoroutine(HandleAttackAfterAnimation());
                }
            }
        }
    }

    public void PerformAttack()
    {
        if (monster != null && monster.gameObject.activeInHierarchy)
        {
            if (monster.attackType == AttackType.Melee && !monster.isDead)
            {
                PerformMeleeAttack();  // ���� ����
            }
            else if (monster.attackType == AttackType.Ranged && !monster.isDead)
            {
                PerformRangedAttack();  // ���Ÿ� ����
            }
        }
    }

    private IEnumerator HandleAttackAfterAnimation()
    {
        float attackAnimationLength = monster.animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(attackAnimationLength);

        // ���⼭ �÷��̾� ������ ����
        Debug.Log($"{monster.monsterName}�� {monster.attackDamage}�� ���ظ� �������ϴ�!");

        monster.SetAttacking(false);
        monster.animator.SetBool("isAttack", false);
    }
}
