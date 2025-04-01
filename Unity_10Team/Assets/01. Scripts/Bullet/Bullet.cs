using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player player { get; set; }

    float bulletSpeed = 20f;

    private void Awake()
    {
    }

    void Update()
    {
        Move();
    }

    void OnEnable()
    {
        StartCoroutine(ReleaseDelay());
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().TakeDamage(player.pStat.PlayerDamage());

            player.bulletPool.Release(this);
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private IEnumerator ReleaseDelay()
    {
        yield return new WaitForSeconds(5f);
        player.bulletPool.Release(this);
    }
}
