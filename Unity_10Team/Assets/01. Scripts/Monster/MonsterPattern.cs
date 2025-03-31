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

    private List<Obstacle> activeObstacles = new List<Obstacle>();
    private Monster eliteMonster;
    private bool eliteMonsterDefeated = false;

    private ObjectPool<Obstacle> obstaclePool;
    private Dictionary<GameObject, ObjectPool<Monster>> eliteMonsterPools = new Dictionary<GameObject, ObjectPool<Monster>>();


    void Start()
    {
        Obstacle obstacles = obstaclePrefab.GetComponent<Obstacle>();
        obstaclePool = new ObjectPool<Obstacle>(obstacles, spawnCount, transform);

        //Monster eliteMonsters = eliteMonsterPrefab.GetComponent<Monster>();
        //eliteMonsterPool = new ObjectPool<Monster>(eliteMonsters, 1, transform);

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
        yield return new WaitForSeconds(2f);

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

            yield return new WaitForSeconds(2f);
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
        return transform.position + offset;
    }

    void DestroyObstacle()
    {
        Debug.Log("장애물 파괴");
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            activeObstacles[i].ReturnToPool();
            activeObstacles.RemoveAt(i);
        }
    }

    void SpawnEliteMonster()
    {
        Vector3 spawnPosition;

        spawnPosition = GetRandomPositionInObstacleRange();

        int randomIndex = Random.Range(0, eliteMonsterPrefab.Length);
        GameObject selectedPrefab = eliteMonsterPrefab[randomIndex];

        eliteMonster = selectedPrefab.GetComponent<Monster>();

        eliteMonster = eliteMonsterPools[selectedPrefab].Get();
        eliteMonster.transform.position = spawnPosition;
        eliteMonster.transform.SetParent(transform);

        eliteMonster.Initialize();

        float baseHealth = eliteMonster.health;
        float baseAttackDamage = eliteMonster.attackDamage;
        float baseCooldown = eliteMonster.attackCooldown;
        float baseRange = eliteMonster.attackRange;
        float baseSpeed = eliteMonster.moveSpeed;

        if (eliteMonster.attackType == AttackType.Melee)
        {
            eliteMonster.SetStats(baseAttackDamage * 3f, baseRange * 2f, baseCooldown * 1f, baseHealth * 10f, baseSpeed * 3f);
        }
        else if (eliteMonster.attackType == AttackType.Ranged) 
        {
            eliteMonster.SetStats(baseAttackDamage * 2f, baseRange * 1.5f, baseCooldown * 1f, baseHealth * 10f, baseSpeed * 2f);
        }
        
        eliteMonster.transform.localScale = new Vector3(3f, 3f, 3f);

        eliteMonster.OnDeathEvent -= OnEliteMonsterDefeated;
        eliteMonster.OnDeathEvent += OnEliteMonsterDefeated;
    }

    void OnEliteMonsterDefeated(Monster monster)
    {
        Debug.Log("Elite Monster Defeated!");
        eliteMonsterDefeated = true;
        eliteMonster.isDead = true;
        Debug.Log($"eliteMonsterDefeated : {eliteMonsterDefeated}");
        //eliteMonsterPools[monster.gameObject].Release(monster);
        //eliteMonster.OnDeathEvent -= OnEliteMonsterDefeated;

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

        // 나중에 임시큐브 말고 다른걸로 바꾸면 이거 쓸듯

        //foreach (Obstacle obstacle in activeObstacles)
        //{
        //    MeshCollider collider = obstacle.GetComponent<MeshCollider>();
        //    if (collider != null)
        //    {
        //
        //        Bounds bounds = collider.bounds;

        //
        //        if (bounds.Contains(position))
        //        {
        //            return true;
        //        }
        //    }
        //}
        //return false;
    }
}
