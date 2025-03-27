using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPattern : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject eliteMonsterPrefab;
    public int spawnCount = 5;
    public float spawnRadius = 10f;
    public float patternDuration = 10f;
    public float eliteMonsterSpawnRadius = 2f;

    private List<GameObject> obstacles = new List<GameObject>();
    private Monster eliteMonster;
    private MonsterSpawner monsterSpawner;
    private bool eliteMonsterDefeated = false;

    void Start()
    {
        monsterSpawner = MonsterSpawner.Instance;
        StartCoroutine(PatternLoop());
    }

    IEnumerator PatternLoop()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            StartCoroutine(ActivatePatternRoutine());

            yield return new WaitUntil(() => eliteMonsterDefeated);
            eliteMonsterDefeated = false;

            yield return new WaitForSeconds(180f);
        }
    }

    IEnumerator ActivatePatternRoutine()
    {
        PlaceObstacleInCircle();

        SpawnEliteMonster();

        yield return new WaitForSeconds(patternDuration);

        if (eliteMonsterDefeated)
        {
            DestroyObstacle();
        }

        //SpawnMonstersInCircle();
    }

    void PlaceObstacleInCircle()
    {
        float angleStep = 360f / spawnCount;
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetCirclePosition(i, spawnRadius);
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            newObstacle.transform.SetParent(transform);
            obstacles.Add(newObstacle);
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
        foreach (GameObject obstacle in obstacles)
        {
            Debug.Log("Obstacle destroyed!");
            Destroy(obstacle); // 모든 장애물 삭제
        }

        obstacles.Clear();
    }

    void SpawnEliteMonster()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * eliteMonsterSpawnRadius;
        eliteMonster = Instantiate(eliteMonsterPrefab, spawnPosition, Quaternion.identity).GetComponent<Monster>();
        eliteMonster.OnDeathEvent += OnEliteMonsterDefeated;
        Debug.Log("OnDeathEvent has been subscribed to the elite monster.");
        eliteMonster.transform.SetParent(transform);
    }

    void OnEliteMonsterDefeated(Monster monster)
    {
        Debug.Log("Elite Monster Defeated!");
        eliteMonsterDefeated = true;
        DestroyObstacle();
    }

    void SpawnMonstersInCircle()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Monster monster = monsterSpawner.GetRandomMonster();

            if (monster != null)
            {
                Vector3 spawnPos = GetCirclePosition(i, spawnRadius);
                monster.transform.position = spawnPos;
                monster.transform.rotation = Quaternion.identity;
                monster.gameObject.SetActive(true);
                Debug.Log($"원형 안에 배치된 몬스터: {monster.name}");
            }
        }
    }
}
