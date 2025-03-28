using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPattern : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject eliteMonsterPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private int spawnCount = 5;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float safeRadius = 1f;

    private List<Obstacle> activeObstacles = new List<Obstacle>();
    private Monster eliteMonster;
    private bool eliteMonsterDefeated = false;

    private ObjectPool<Obstacle> obstaclePool;
    private ObjectPool<Monster> eliteMonsterPool;

    void Start()
    {
        Obstacle obstacles = obstaclePrefab.GetComponent<Obstacle>();
        obstaclePool = new ObjectPool<Obstacle>(obstacles, spawnCount, transform);

        Monster eliteMonsters = eliteMonsterPrefab.GetComponent<Monster>();
        eliteMonsterPool = new ObjectPool<Monster>(eliteMonsters, 1, transform);

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
            }

            yield return new WaitForSeconds(180f);
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

            Obstacle obstacle = obstaclePool.Get(); // 풀에서 가져옴
            obstacle.Initialize(spawnPosition);
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
        foreach (Obstacle obstacle in activeObstacles)
        {
            obstaclePool.Release(obstacle);
        }

        activeObstacles.Clear();
    }

    void SpawnEliteMonster()
    {
        Vector3 spawnPosition;

        spawnPosition = GetRandomPositionInObstacleRange();

        eliteMonster = eliteMonsterPool.Get();
        eliteMonster.transform.position = spawnPosition;
        eliteMonster.transform.SetParent(transform);

        eliteMonster.SetStats(10, 5, 5, 1000, 5);
        eliteMonster.transform.localScale = new Vector3(3f, 3f, 3f);

        NavMeshAgent agent = eliteMonster.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = eliteMonster.GetMoveSpeed(); // 몬스터의 이동 속도를 NavMeshAgent에 반영
        }

        eliteMonster.OnDeathEvent += OnEliteMonsterDefeated;
    }

    void OnEliteMonsterDefeated(Monster monster)
    {
        Debug.Log("Elite Monster Defeated!");
        eliteMonsterDefeated = true;
        Debug.Log($"eliteMonsterDefeated : {eliteMonsterDefeated}");
        eliteMonsterPool.Release(monster);
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
