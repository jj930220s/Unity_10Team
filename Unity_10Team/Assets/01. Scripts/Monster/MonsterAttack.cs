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
                    StartCoroutine(PerformMeleeAttackAfterDelay(1.2f));
                    lastMeleeAttackTime = Time.time;
                }
            }
        }
    }

    private void PerformRangedAttack()
    {
        //if (monster.attackType == AttackType.Ranged)
        //{
        //    GameObject projectile = GameObject.Instantiate(monster.monsterData.projectilePrefab, monster.transform.position, Quaternion.identity);
        //    Vector3 direction = (GetTargetPosition() - monster.transform.position).normalized;
        //    projectile.GetComponent<Projectile>().Launch(direction, monster.attackDamage);  // Projectile 클래스에서 공격 처리
        //}
    }

    public void PerformAttack()
    {
        if (monster != null && monster.gameObject.activeInHierarchy)
        {
            if (monster.attackType == AttackType.Melee)
            {
                PerformMeleeAttack();  // 근접 공격
            }
            else if (monster.attackType == AttackType.Ranged)
            {
                PerformRangedAttack();  // 원거리 공격
            }
        }
    }

    //private Vector3 GetTargetPosition()
    //{
    //    return Player.Instance.transform.position;
    //}

    private IEnumerator PerformMeleeAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 여기에 플레이어 데미지 적용

        Debug.Log($"{monster.monsterName}가 {monster.attackDamage}의 피해를 입혔습니다!");

        monster.SetAttacking(false);
    }
}
