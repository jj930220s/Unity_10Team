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
    private List<string> monsterTypes = new List<string>();

    void Start()
    {
        if (monsterPrefabs.Length == 0)
        {
            Debug.LogError("[MonsterSpawner] 몬스터 프리팹이 설정되지 않았습니다!");
            return;
        }

        foreach (var prefab in monsterPrefabs)
        {
            Monster monster = prefab.GetComponent<Monster>();
            ObjectPool<Monster> pool = new ObjectPool<Monster>(monster, initialPoolSize, transform);
            monsterPools.Add(pool);
            monsterTypes.Add(monster.monsterData.monsterType.ToString());
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
                Debug.LogWarning("중복이거나 monster가 Null입니다.");
                break;
            }

            if (monster != null)
            {
                Vector3 randomPos = GetRandomPosition();

                monster.transform.position = randomPos;
                monster.transform.rotation = Quaternion.identity;

                monster.Initialize();

                activeMonsters.Add(monster);
                Debug.Log($"활성화된 몬스터 수(생성 후): {activeMonsters.Count} {monster.name}");
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
            Debug.Log($"[Deactivate] 활성화된 몬스터 수: {activeMonsters.Count}");
        }

        ReturnMonster(monster);
    }

    public void ReturnMonster(Monster monster)
    {
        if (monsterPools == null || monsterPools.Count == 0)
        {
            Debug.Log("ReturnMonster: monsterPools가 비어있거나 null입니다!");
            return;
        }

        string monsterType = monster.monsterData.monsterType.ToString();
        int poolIndex = monsterTypes.IndexOf(monsterType);

        if (poolIndex != -1)
        {
            monster.OnDisableEvent -= DeactivateMonster;
            monster.gameObject.SetActive(false);

            monsterPools[poolIndex].Release(monster);
        }
    }

    public Monster GetRandomMonster()
    {
        int randomIndex = Random.Range(0, monsterPools.Count);
        return monsterPools[randomIndex].Get();
    }
}
