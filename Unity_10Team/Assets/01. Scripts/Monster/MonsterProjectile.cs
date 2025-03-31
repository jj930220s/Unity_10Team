using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private Vector3 direction;
    public float damage;
    public float speed = 2f;
    public float lifeTime = 5f;

    private ObjectPool<MonsterProjectile> pool;

    private void Start()
    {
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    public void Initialize(ObjectPool<MonsterProjectile> pool)
    {
        this.pool = pool;
    }

    public void Launch(Vector3 dir, float dmg)
    {
        direction = dir.normalized;
        damage = dmg;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || !other.isTrigger) // < ���߿� ��ֹ�, �� �̷��� �±� �ɾ ���⿡ ����ϸ� �ɵ�
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
