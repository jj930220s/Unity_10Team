using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AttackDrone : BaseDroneController
{
    private Player player;

    private int bulletCount;



    [Header("적 탐색 설정")]
    protected float searchRadius = 10f; //탐지반경
    protected LayerMask enemyLayer; //적레이어
    protected LayerMask obstacleLayer; //장애물레이어
    protected GameObject currentTarget; //현재타겟

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;

        //적레이어설정
        enemyLayer = LayerMask.GetMask("Enemy");
        obstacleLayer = LayerMask.GetMask("Obstacle");

        SetAbility();
        StartDrone();
    }

    private void SetAbility()
    {
        delayTime = 2.5f;
        bulletCount = 1;
    }

    protected override void StartDrone()
    {
        FindTarget();
        base.StartDrone();
    }

    protected override void DroneAction()
    {
        // 공격

        ShootBullet().Forget();

    }

    private async UniTaskVoid ShootBullet()
    {
        Vector3 dir = (target.transform.position - transform.position).normalized;
        dir.y = 0;

        for (int i = 0; i < bulletCount; i++)
        {
            Bullet bullet = player.bulletPool.Get();
            if (bullet.player == null)
            {
                bullet.player = GameManager.Instance.player;
            }

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(dir);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
        FindTarget();
    }

    public void IncreaseBulletCount(int cnt)
    {
        bulletCount += cnt;
    }


    //적을 찾아서 현재타겟
    protected void FindTarget()
    {
        target = FindNearestVisibleEnemy();

        if (target != null)
        {
            Debug.Log("적 탐지: " + target.name);
        }
        else
        {
            target = null;
        }
    }


    //가장 가까운 적을 찾으면서 사이에 장애물이 있으면 새롭게 탐색
    protected GameObject FindNearestVisibleEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, searchRadius, enemyLayer);
        List<GameObject> visibleEnemies = new List<GameObject>();
        float minDistance = Mathf.Infinity;
        GameObject closest = null;
        Vector3 currentPosition = player.transform.position;

        foreach (Collider collider in colliders)
        {
            Vector3 direction = (collider.transform.position - currentPosition).normalized;
            float distance = Vector3.Distance(currentPosition, collider.transform.position);

            // 장애물 체크
            if (!Physics.Raycast(currentPosition, direction, distance, obstacleLayer))
            {
                visibleEnemies.Add(collider.gameObject);
            }
        }

        //가장 가까운 적 찾기
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
}
