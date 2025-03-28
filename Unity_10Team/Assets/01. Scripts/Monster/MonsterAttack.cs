using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private Monster monster;
    public Transform handTransform;

    private float lastMeleeAttackTime;
    private float lastRangedAttackTime;

    private void Start()
    {
        monster = GetComponent<Monster>();
    }

    // 근접 공격 메서드
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
                    lastMeleeAttackTime = Time.time;

                    StartCoroutine(HandleAttackAfterAnimation());
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
                    Vector3 spawnPosition = handTransform.position;

                    GameObject projectile = Instantiate(monster.monsterData.projectilePrefab, spawnPosition, Quaternion.identity);

                    Vector3 direction = (monster.target.position - spawnPosition).normalized;

                    projectile.GetComponent<MonsterProjectile>().Launch(direction, monster.attackDamage);

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
                PerformMeleeAttack();  // 근접 공격
            }
            else if (monster.attackType == AttackType.Ranged && !monster.isDead)
            {
                PerformRangedAttack();  // 원거리 공격
            }
        }
    }

    private IEnumerator HandleAttackAfterAnimation()
    {
 
        float attackAnimationLength = monster.animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(attackAnimationLength);

        Collider[] hitEnemies = Physics.OverlapSphere(monster.transform.position, monster.attackRange);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player"))
            {
                // 여기에 플레이어 데미지 적용
               Debug.Log($"{monster.monsterName}가 {monster.attackDamage}의 피해를 입혔습니다!");
            }
        }

        monster.SetAttacking(false);
        monster.animator.SetBool("isAttack", false);
    }
}
