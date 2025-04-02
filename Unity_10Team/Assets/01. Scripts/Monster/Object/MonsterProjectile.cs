using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private Vector3 direction;
    public float damage;
    public float speed = 1f;
    public float lifeTime = 2f;

    private ObjectPool<MonsterProjectile> pool;
    private bool hasCollided = false;

    public void Initialize(ObjectPool<MonsterProjectile> pool)
    {
        this.pool = pool;
        direction = Vector3.zero;
        CancelInvoke();
        hasCollided = false;
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
        if (hasCollided) return;

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.player.pStat.TakeDamage(damage);
            hasCollided = true;
            ReturnToPool();
        }
        else if (other.CompareTag("Wall"))
            ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Release(this);
        }
    }
}
