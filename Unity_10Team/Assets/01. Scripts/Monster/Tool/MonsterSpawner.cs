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

    public Dictionary<GameObject, ObjectPool<Monster>> monsterPools = new Dictionary<GameObject, ObjectPool<Monster>>();
    private HashSet<Monster> activeMonsters = new HashSet<Monster>();

    void Start()
    {
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogError("���� �������� �������� �ʾҽ��ϴ�");
            return;
        }

        foreach (var prefab in monsterPrefabs)
        {
            Monster monster = prefab.GetComponent<Monster>();
            ObjectPool<Monster> pool = new ObjectPool<Monster>(monster, initialPoolSize, transform);
            monsterPools.Add(prefab, pool); 
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
        for (int i = 0; i < spawnCount; i++)
        {
            if (activeMonsters.Count >= maxMonsterCount) return;

            int attempts = 0;
            Monster monster = null;

            while (attempts < 5)
            {
                GameObject randomPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
                monster = monsterPools[randomPrefab].Get();

                if (monster != null && !activeMonsters.Contains(monster))
                {
                    break;
                }

                attempts++;
            }

            if (monster == null || activeMonsters.Contains(monster))
            {
                Debug.LogWarning("[SpawnMonsters] �ߺ��̰ų� monster�� Null�Դϴ�.");
                break;
            }

            if (monster != null)
            {
                Vector3 randomPos = GetRandomPosition();

                monster.transform.position = randomPos;
                monster.transform.rotation = Quaternion.identity;

                monster.Initialize();

                activeMonsters.Add(monster);
                Debug.Log($"[SpawnMonsters] Ȱ��ȭ�� ���� ��(���� ��): {activeMonsters.Count} {monster.name}");
                monster.OnDeathEvent += DeactivateMonster;
            }
        }
    }

    public Vector3 GetRandomPosition()
    {
        // �� ������ ��ֹ� �� ���� ó�� �߰��ϱ�
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
        if (activeMonsters.Remove(monster))
        {
            Debug.Log($"[Deactivate] Ȱ��ȭ�� ���� ��: {activeMonsters.Count}");
            
            HandleMonsterDeath(monster);
            ReturnMonster(monster);

            monster.isDead = false;
        }
    }

    public void HandleMonsterDeath(Monster monster)
    {
        int experienceGained = 0;
        int goldGained = 0;

        if (monster.monsterType == MonsterType.Normal)
        {
            experienceGained = Random.Range(1, 4);
            goldGained = Random.Range(2, 6);
        }
        else if (monster.monsterType == MonsterType.Boss)
        {
            experienceGained = Random.Range(10, 21);
            goldGained = Random.Range(15, 31);
        }

        MonsterDropItem.Instance.DropItems(monster, experienceGained, goldGained);
    }

    public void ReturnMonster(Monster monster)
    {
        if (monsterPools == null || monsterPools.Count == 0)
        {
            Debug.Log("[ReturnMonster] monsterPools�� ����ְų� null�Դϴ�");
            return;
        }

        if (activeMonsters.Contains(monster))
        {
            activeMonsters.Remove(monster);
            Debug.Log($"[Return] Ȱ��ȭ�� ���� ��: {activeMonsters.Count}");
        }

        monster.ResetStats();

        foreach (var pool in monsterPools.Values)
        {
            if (monster.gameObject.activeSelf)
            {
                pool.Release(monster);
                monster.OnDeathEvent -= DeactivateMonster;
            }
            break;
        }
    }

    public Monster GetRandomMonster()
    {
        GameObject randomPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        return monsterPools[randomPrefab].Get();
    }
}
