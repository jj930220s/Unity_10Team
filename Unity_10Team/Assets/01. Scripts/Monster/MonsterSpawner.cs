using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private int initialPoolSize = 10;
    [SerializeField] private int maxMonsterCount = 10;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private float safeRadius = 3f;

    private List<ObjectPool<Monster>> monsterPools = new List<ObjectPool<Monster>>();
    private List<Monster> activeMonsters = new List<Monster>();

    void Start()
    {
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogError("[MonsterSpawner] ���� �������� �������� �ʾҽ��ϴ�!");
            return;
        }

        // �� ���� �����տ� ���� Ǯ�� ����
        foreach (var prefab in monsterPrefabs)
        {
            Monster monster = prefab.GetComponent<Monster>();
            ObjectPool<Monster> pool = new ObjectPool<Monster>(monster, initialPoolSize, transform);
            monsterPools.Add(pool);
        }

        StartCoroutine(SpawnMonstersRoutine());
    }

    IEnumerator SpawnMonstersRoutine()
    {
        while (true)
        {
            if (activeMonsters.Count < maxMonsterCount)
            {
                SpawnMonsters();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    void SpawnMonsters()
    {
        if (activeMonsters.Count >= maxMonsterCount) return;

        for (int i = 0; i < spawnCount; i++)
        {
            if (activeMonsters.Count >= maxMonsterCount) return;

            int attempts = 0;
            Monster monster = null;

            while (attempts < 5)
            {
                int randomIndex = Random.Range(0, monsterPools.Count);
                monster = monsterPools[randomIndex].Get();

                if (monster != null && !activeMonsters.Contains(monster))
                {
                    break;
                }

                attempts++;
            }

            if (monster == null || activeMonsters.Contains(monster))
            {
                Debug.LogWarning("�ߺ��̰ų� monster�� Null�Դϴ�.");
                break;
            }

            if (monster != null)
            {
                Vector3 randomPos = GetRandomPosition();

                monster.transform.position = randomPos;
                monster.transform.rotation = Quaternion.identity;

                monster.Initialize();

                activeMonsters.Add(monster);
                Debug.Log($"Ȱ��ȭ�� ���� ��(���� ��): {activeMonsters.Count} {monster.name}");
                monster.OnDisableEvent += DeactivateMonster;
            }
        }
    }

    public Vector3 GetRandomPosition()
    {
        float radius = 0f;

        while (radius < safeRadius)
        {
            radius = Random.Range(safeRadius, spawnRadius);
        }

        float angle = Random.Range(0, 2 * Mathf.PI);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

        return player.position + offset;
    }

    public void DeactivateMonster(Monster monster)
    {
        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
            Debug.Log($"[Deactivate] Ȱ��ȭ�� ���� ��: {activeMonsters.Count}");
        }

        ReturnMonster(monster);
    }

    public void ReturnMonster(Monster monster)
    {
        if (monsterPools == null || monsterPools.Count == 0)
        {
            Debug.Log("monsterPools�� ����ְų� null�Դϴ�");
            return;
        }

        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
            Debug.Log($"[Return] Ȱ��ȭ�� ���� ��: {activeMonsters.Count}");
        }

        foreach (var pool in monsterPools)
        {
            monster.OnDisableEvent -= DeactivateMonster;
            pool.Release(monster);
            break;
        }
    }

    public Monster GetRandomMonster()
    {
        int randomIndex = Random.Range(0, monsterPools.Count);
        return monsterPools[randomIndex].Get();
    }
}
