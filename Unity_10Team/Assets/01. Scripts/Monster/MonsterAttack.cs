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
        //    projectile.GetComponent<Projectile>().Launch(direction, monster.attackDamage);  // Projectile Ŭ�������� ���� ó��
        //}
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

    //private Vector3 GetTargetPosition()
    //{
    //    return Player.Instance.transform.position;
    //}

    private IEnumerator PerformMeleeAttackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���⿡ �÷��̾� ������ ����

        Debug.Log($"{monster.monsterName}�� {monster.attackDamage}�� ���ظ� �������ϴ�!");

        monster.SetAttacking(false);
    }
}
