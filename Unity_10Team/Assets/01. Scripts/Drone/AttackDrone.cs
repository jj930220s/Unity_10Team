using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackDrone : BaseDroneController
{
    private Player player;

    private int bulletCount;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        // 수정필요
        target = player.GetComponent<PlayerTargeting>().currentTarget;
        SetAbility();
    }

    private void SetAbility()
    {
        delayTime = 1f;
        bulletCount = 1;
    }

    protected override void StartDrone()
    {


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

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.LookRotation(dir);
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }
    }

    public void IncreaseBulletCount(int cnt)
    {
        bulletCount += cnt;
    }
}
