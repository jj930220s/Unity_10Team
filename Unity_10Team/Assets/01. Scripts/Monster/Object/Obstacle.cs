using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float radius = 1f;

    private ObjectPool<Obstacle> pool;

    public void Activate(Vector3 position, ObjectPool<Obstacle> pool)
    {
        this.pool = pool;
        transform.position = position;
        transform.rotation = Quaternion.identity;   
        gameObject.SetActive(true);
    }

    public void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
    }
}
