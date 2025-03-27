using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public float searchRadius = 10f; //Ž���ݰ�
    public LayerMask enemyLayer; //�����̾�
    public GameObject currentTarget; //����Ÿ��

    private void Update()
    {
        currentTarget = FindNearestEnemy();
    }

    //��ã��

    GameObject FindNearestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, enemyLayer);
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPostion = transform.position;

        foreach(Collider collider in colliders)
        {
            float distance = Vector3.Distance(currentPostion, collider.transform.position);
            
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = collider.gameObject;

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
