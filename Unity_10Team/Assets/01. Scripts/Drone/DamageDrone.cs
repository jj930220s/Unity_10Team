using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDrone : BaseDroneController
{
    private Player player;
    private float damage;
    protected LayerMask enemyLayer; //적레이어

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        SetAbility();
        StartDrone();
    }

    private void SetAbility()
    {
        delayTime = 15;
        damage = 30;
        enemyLayer = LayerMask.GetMask("Enemy");

    }

    protected override void StartDrone()
    {
        target = player.gameObject;
        base.StartDrone();
    }

    protected override void DroneAction()
    {
        DealAllEnemy().Forget();
    }

    private async UniTaskVoid DealAllEnemy()
    {
        while (true)
        {
            Collider[] col = Physics.OverlapSphere(player.transform.position, 5,enemyLayer);
            foreach (var c in col)
            {
                c.GetComponent<Monster>().TakeDamage(damage);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
        }
    }

    private void IncreasDamage(float dmg)
    {
        damage += dmg;
    }

}
