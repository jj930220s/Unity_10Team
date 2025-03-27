using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float health = 100f;

    public delegate void OnDisableDelegate(Monster monster);
    public event OnDisableDelegate OnDisableEvent;

    public void Initialize()
    {
        health = 100f;  // 체력 초기화
    }

    private void OnDisable()
    {
        MonsterSpawner spawner = FindObjectOfType<MonsterSpawner>();

        if (spawner == null)
        {
            Debug.LogError("[Monster] MonsterSpawner를 찾을 수 없음!");
            return;
        }
        OnDisableEvent?.Invoke(this);
        spawner.ReturnMonster(this);
    }
}
