using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPattern : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject[] eliteMonsterPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float safeRadius = 1f;
    [SerializeField] private float patternTime = 0f;

    private List<Obstacle> activeObstacles = new List<Obstacle>();
    private Monster eliteMonster;
    private bool eliteMonsterDefeated = false;

    private ObjectPool<Obstacle> obstaclePool;
    private Dictionary<GameObject, ObjectPool<Monster>> eliteMonsterPools = new Dictionary<GameObject, ObjectPool<Monster>>();

    void Start()
    {
        Obstacle obstacles = obstaclePrefab.GetComponent<Obstacle>();
        obstaclePool = new ObjectPool<Obstacle>(obstacles, spawnCount, transform);

        foreach (var prefab in eliteMonsterPrefab)
        {
            Monster eliteMonsters = prefab.GetComponent<Monster>();
            ObjectPool<Monster> pool = new ObjectPool<Monster>(eliteMonsters, 1, transform);
            eliteMonsterPools.Add(prefab, pool);
        }

        StartCoroutine(PatternLoop());
    }

    IEnumerator PatternLoop()
    {
        yield return new WaitForSeconds(patternTime);

        while (true)
        {
            yield return StartCoroutine(ActivatePatternRoutine());

            yield return new WaitUntil(() => eliteMonsterDefeated);

            if (eliteMonsterDefeated)
            {
                DestroyObstacle();
                eliteMonsterDefeated = false;
                eliteMonster.isDead = false;
            }

            yield return new WaitForSeconds(patternTime);
        }
    }

    IEnumerator ActivatePatternRoutine()
    {
        PlaceObstacleInCircle();
        SpawnEliteMonster();

        yield return null;
    }

    void PlaceObstacleInCircle()
    {
        float angleStep = 360f / spawnCount;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetCirclePosition(i, spawnRadius);
            spawnPosition += Vector3.up * 0.5f;

            Obstacle obstacle = obstaclePool.Get();
            obstacle.Activate(spawnPosition, obstaclePool);
            obstacle.transform.SetParent(transform);

            activeObstacles.Add(obstacle);
        }
    }

    Vector3 GetCirclePosition(int index, float radius)
    {
        float angle = (index * Mathf.PI * 2) / spawnCount;
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        return player.position + offset;
    }

    void DestroyObstacle()
    {
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            activeObstacles[i].ReturnToPool();
            activeObstacles.RemoveAt(i);
        }
    }

    void SpawnEliteMonster()
    {
        Vector3 spawnPosition = GetRandomPositionInObstacleRange();

        Monster spawnedMonster = null;
        GameObject selectedPrefab = null;

        int attempt = 0;
        int maxAttempts = 10;

        while (attempt < maxAttempts)
        {
            int randomIndex = Random.Range(0, eliteMonsterPrefab.Length);
            selectedPrefab = eliteMonsterPrefab[randomIndex];

            ObjectPool<Monster> pool = eliteMonsterPools[selectedPrefab];
            spawnedMonster = pool.Get();

            if (spawnedMonster != null) break;

            attempt++;
        }

        if (spawnedMonster == null)
        {
            Debug.LogError("모든 몬스터 풀에서 가져올 수 있는 몬스터가 없습니다!");
            return;
        }

        eliteMonster = spawnedMonster;
        eliteMonster.transform.position = spawnPosition;
        eliteMonster.transform.SetParent(transform);

        eliteMonster.Initialize();

        float baseHealth = eliteMonster.health;
        float baseAttackDamage = eliteMonster.attackDamage;
        float baseRange = eliteMonster.attackRange;
        float baseSpeed = eliteMonster.moveSpeed;
        AttackType baseType = eliteMonster.attackType;

        if (baseType == AttackType.Melee)
        {
            eliteMonster.SetStats(baseAttackDamage * 3f, baseRange * 1f, MonsterType.Boss, baseHealth * 10f, baseSpeed * 2f);
            eliteMonster.projectilePrefab = null;
        }
        else if (baseType == AttackType.Ranged) 
        {
            eliteMonster.SetStats(baseAttackDamage * 2f, baseRange * 1.5f, MonsterType.Boss, baseHealth * 10f, baseSpeed * 1.5f);
        }
        
        eliteMonster.transform.localScale = new Vector3(3f, 3f, 3f);

        if (eliteMonster != null)
        {
            eliteMonster.OnDeathEvent -= OnEliteMonsterDefeated;
        }
        eliteMonster.OnDeathEvent += OnEliteMonsterDefeated;
    }

    void OnEliteMonsterDefeated(Monster monster)
    {
        eliteMonsterDefeated = true;
        eliteMonster.isDead = true;

        MonsterSpawner.Instance.HandleMonsterDeath(monster);

        monster.ResetStats();

        foreach (var pool in eliteMonsterPools.Values)
        {
            if (monster.gameObject.activeSelf)
            {
                pool.Release(monster);
                monster.OnDeathEvent -= OnEliteMonsterDefeated;
            }
            break;
        }
    }

    Vector3 GetRandomPositionInObstacleRange()
    {
        Vector3 spawnPosition = Vector3.zero;

        do
        {
            float radius = Random.Range(safeRadius, spawnRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            spawnPosition = player.position + offset;

        } while (IsPositionNearPlayer(spawnPosition) && IsPositionInsideObstacles(spawnPosition));

        return spawnPosition;
    }

    bool IsPositionNearPlayer(Vector3 position)
    {
        return Vector3.Distance(position, player.position) < safeRadius;
    }

    bool IsPositionInsideObstacles(Vector3 position)
    {
        foreach (Obstacle obstacle in activeObstacles)
        {
            float distanceToObstacle = Vector3.Distance(position, obstacle.transform.position);
            if (distanceToObstacle < obstacle.radius)
            {
                return true;
            }
        }
        return false;
    }
}
