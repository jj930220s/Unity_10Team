using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public float searchRadius = 10f; //Ž���ݰ�
    public LayerMask enemyLayer; //�����̾�
    public LayerMask obstacleLayer; //��ֹ� ���̾�
    public GameObject currentTarget; //����Ÿ��

    private void Update()
    {
        currentTarget = FindNearestVisibleEnemy();
        if (currentTarget != null)
        {
            Debug.Log("�� Ž��");
        }
    }

    //���� ����� ���� ã�µ�, ��ֹ��� �Ȱ����� �� �켱
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

        //���� ����� �� ã��
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
