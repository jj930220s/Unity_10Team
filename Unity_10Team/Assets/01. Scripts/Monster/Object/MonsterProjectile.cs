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

    public void Launch(Vector3 dir, float dmg, Transform parent)
    {
        transform.SetParent(parent); 
        transform.position = parent.position;
        transform.rotation = Quaternion.identity;

        direction = dir.normalized;
        damage = dmg;

        StartCoroutine(MoveToBulletContainer());

        Invoke("ReturnToPool", lifeTime);
    }

    private void Update()
    {
        if (!hasCollided)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasCollided) return;

        hasCollided = true;

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.player.pStat.TakeDamage(damage);
            
            ReturnToPool();
        }
        else if (other.CompareTag("Wall"))
            ReturnToPool();
    }

    private IEnumerator MoveToBulletContainer()
    {
        yield return null; 

        Transform bulletContainer = GameObject.Find("MonsterProjectilePool")?.transform;
        if (bulletContainer != null)
        {
            transform.SetParent(bulletContainer);
        }
        else
        {
            transform.SetParent(null);
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
