using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class AttackDrone : BaseDroneController
{
    private Player player;

    private int bulletCount;



    [Header("�� Ž�� ����")]
    protected float searchRadius = 10f; //Ž���ݰ�
    protected LayerMask enemyLayer; //�����̾�
    protected LayerMask obstacleLayer; //��ֹ����̾�
    protected GameObject currentTarget; //����Ÿ��

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;

        //�����̾��
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
        // ����

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


    //���� ã�Ƽ� ����Ÿ��
    protected void FindTarget()
    {
        target = FindNearestVisibleEnemy();

        if (target != null)
        {
            Debug.Log("�� Ž��: " + target.name);
        }
        else
        {
            target = null;
        }
    }


    //���� ����� ���� ã���鼭 ���̿� ��ֹ��� ������ ���Ӱ� Ž��
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

            // ��ֹ� üũ
            if (!Physics.Raycast(currentPosition, direction, distance, obstacleLayer))
            {
                visibleEnemies.Add(collider.gameObject);
            }
        }

        //���� ����� �� ã��
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
