using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Monster monster;
    private EnemyAI ai;
    public Transform handTransform;
    public Player player;

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

        player = FindObjectOfType<Player>();
    }

    private void PerformMeleeAttack()
    {
        if (Vector3.Distance(monster.transform.position, monster.target.position) <= ai.agent.stoppingDistance)
        {
            if (Time.time - lastMeleeAttackTime >= monster.attackCooldown && monster.target != null)
            {
                if (CheckWall()) return;

                monster.SetAttacking(true);
                lastMeleeAttackTime = Time.time;

                StartCoroutine(HandleAttackAfterAnimation());
            }
        }
    }

    private void PerformRangedAttack()
    {
        if (Vector3.Distance(monster.transform.position, monster.target.position) <= ai.agent.stoppingDistance)
        {
            if (Time.time - lastRangedAttackTime >= monster.attackCooldown && monster.target != null)
            {
                if (CheckWall()) return;

                LaunchProjectile();

                monster.SetAttacking(true);
                lastRangedAttackTime = Time.time;

                StartCoroutine(HandleAttackAfterAnimation());
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
        //Debug.Log($"{monster.monsterName}�� {monster.attackDamage}�� ���ظ� �������ϴ�!");
        player.pStat.TakeDamage(monster.attackDamage);

        monster.SetAttacking(false);
        monster.animator.SetBool("isAttack", false);
    }

    private void LaunchProjectile()
    {
        Vector3 spawnPosition = handTransform.position;

        MonsterProjectile projectile = projectilePool.Get();
        projectile.Initialize(projectilePool);
        projectile.transform.position = spawnPosition;
        projectile.transform.rotation = Quaternion.identity;

        Vector3 direction = (monster.target.position - spawnPosition).normalized;

        projectile.Launch(direction, monster.attackDamage);
    }

    private bool CheckWall()
    {
        Vector3 directionToTarget = monster.target.position - monster.transform.position;

        RaycastHit hit;

        if (Physics.Raycast(monster.transform.position, directionToTarget.normalized, out hit, directionToTarget.magnitude))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                return true;
            }
        }
        return false;
    }
}
