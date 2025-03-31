using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public float searchRadius = 10f; //탐지반경
    public LayerMask enemyLayer; //적레이어
    public LayerMask obstacleLayer; //장애물 레이어
    public GameObject currentTarget; //현재타겟

    private void Update()
    {
        currentTarget = FindNearestVisibleEnemy();
        if (currentTarget != null)
        {
            Debug.Log("적 탐지");
        }
    }

    //가장 가까운 적을 찾는데, 장애물에 안가려진 것 우선
    GameObject FindNearestVisibleEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, enemyLayer);
        List<GameObject> visibleEnemies = new List<GameObject>();
        float minDistance = Mathf.Infinity;
        GameObject closest = null;
        Vector3 currentPosition = transform.position;

        foreach (Collider collider in colliders)
        {
            Vector3 direction = (collider.transform.position - currentPosition).normalized;
            float distance = Vector3.Distance(currentPosition, collider.transform.position);

            if (!Physics.Raycast(currentPosition, direction, distance, obstacleLayer))
            {
                visibleEnemies.Add(collider.gameObject);
            }
        }

        //가장 가까운 적 찾기
        foreach (GameObject enemy in visibleEnemies)
        {
            float distance = Vector3.Distance(currentPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
