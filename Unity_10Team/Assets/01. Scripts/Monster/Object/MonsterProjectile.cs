using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private Vector3 direction;
    public float damage;
    public float speed = 1f;
    public float lifeTime = 2f;

    private ObjectPool<MonsterProjectile> pool;

    public void Initialize(ObjectPool<MonsterProjectile> pool)
    {
        this.pool = pool;
        direction = Vector3.zero;
        CancelInvoke();
    }

    public void Launch(Vector3 dir, float dmg)
    {
        transform.SetParent(null);
        direction = dir.normalized;
        damage = dmg;

        Invoke("ReturnToPool", lifeTime);
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || !other.isTrigger) // < 나중에 장애물, 벽 이런거 태그 걸어서 여기에 사용하면 될듯
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
    }
}
