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
        health = 100f;  // ü�� �ʱ�ȭ
    }

    private void OnDisable()
    {
        MonsterSpawner spawner = FindObjectOfType<MonsterSpawner>();

        if (spawner == null)
        {
            Debug.LogError("[Monster] MonsterSpawner�� ã�� �� ����!");
            return;
        }
        OnDisableEvent?.Invoke(this);
        spawner.ReturnMonster(this);
    }
}
