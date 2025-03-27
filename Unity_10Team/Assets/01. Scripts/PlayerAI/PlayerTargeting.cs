using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public float searchRadius = 10f; //탐지반경
    public LayerMask enemyLayer; //적레이어
    public GameObject currentTarget; //현재타겟

    private void Update()
    {
        currentTarget = FindNearestEnemy();
    }

    //적찾기

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
