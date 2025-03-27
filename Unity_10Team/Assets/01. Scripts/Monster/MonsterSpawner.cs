using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Monster monsterPrefab;
    public int initialPoolSize = 10;
    public int maxMonsterCount = 10;
    public Transform player;
    public float spawnRadius = 10f;
    public int spawnCount = 5;

    public ObjectPool<Monster> monsterPool;
    private int activeMonsterCount = 0;

    void Start()
    {
        if (monsterPrefab == null)
        {
            Debug.LogError("[MonsterSpawner] monsterPrefab이 설정되지 않음!");
            return;
        }

        monsterPool = new ObjectPool<Monster>(monsterPrefab, initialPoolSize, transform);

        if (monsterPool == null)
        {
            Debug.LogError("[MonsterSpawner] monsterPool 생성 실패!");
        }
        else
        {
            Debug.Log("[MonsterSpawner] monsterPool 생성 성공!");
        }

        StartCoroutine(SpawnMonstersRoutine());
    }

    IEnumerator SpawnMonstersRoutine()
    {
        while (true)
        {
            if (activeMonsterCount < maxMonsterCount)
            {
                SpawnMonsters();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            if (activeMonsterCount >= maxMonsterCount) return;

            Vector3 randomPos = GetRandomPosition();
            Monster monster = monsterPool.Get();
            if (monster != null)
            {
                monster.transform.position = randomPos;
                monster.transform.rotation = Quaternion.identity;
                monster.Initialize();

                activeMonsterCount++;
                Debug.Log($"활성화된 몬스터 수: {activeMonsterCount}");
                monster.OnDisableEvent += DeactivateMonster;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float radius = Random.Range(3f, spawnRadius);
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        return player.position + offset;
    }

    public void DeactivateMonster(Monster monster)
    {
        activeMonsterCount--;
        Debug.Log($"활성화된 몬스터 수: {activeMonsterCount}");
    }

    public void ReturnMonster(Monster monster)
    {
        if (monsterPool == null)
        {
            Debug.Log("ReturnMonster: monsterPool이 null입니다!");
            return;
        }

        monster.OnDisableEvent -= DeactivateMonster;
        monster.gameObject.SetActive(false);

        monsterPool.Release(monster);
    }
}
