using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private Vector3 direction;
    private float damage;
    public float speed = 10f;
    public float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
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
        if (other.CompareTag("Player"))
        {
            Debug.Log($"�÷��̾ {damage}�� �������� �Ծ����ϴ�!");

            // �÷��̾� ü�� ó��

            Destroy(gameObject);
        }
        else if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }
}
