using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;

    public string monsterName;
    public MonsterType monsterType;
    public AttackType attackType;
    public float health;
    public float attackDamage;
    public float moveSpeed;

    public delegate void OnDisableDelegate(Monster monster);
    public event OnDisableDelegate OnDisableEvent;

    public void Initialize()
    {
        if (monsterData != null)
        {
            monsterName = monsterData.monsterName;
            monsterType = monsterData.monsterType;
            attackType = monsterData.attackType;
            health = monsterData.health;
            attackDamage = monsterData.attackDamage;
            moveSpeed = monsterData.moveSpeed;
        }
        else
        {
            Debug.LogError("[Monster] monsterData가 설정되지 않았습니다!");
        }
    }

    private void OnDisable()
    {
        MonsterSpawner spawner = MonsterSpawner.Instance;

        if (spawner == null)
        {
            Debug.LogError("[Monster] MonsterSpawner를 찾을 수 없음!");
            return;
        }
        OnDisableEvent?.Invoke(this);
        spawner.ReturnMonster(this);
        Debug.Log($"{gameObject.name} 비활성화됨");
    }
}
