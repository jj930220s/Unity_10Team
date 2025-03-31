using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDrone : BaseDroneController
{
    private Player player;
    private float damage;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;
        SetAbility();
    }

    private void SetAbility()
    {
        delayTime = 15;
        damage = 30;
    }

    protected override void StartDrone()
    {
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
            Collider[] col = Physics.OverlapSphere(player.transform.position, 5);
            foreach (var c in col)
            {
                // µñ¼Å³Ê¸®·Î ÆÄ¾Ç ÈÄ ÀüÃ¼ µ¥¹ÌÁö
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
