using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDrone : BaseDroneController
{
    private float damage;

    private Player player;


    private void Start()
    {
        player = GameManager.Instance.player;

        SetAbility();
    }

    private void SetAbility()
    {
        delayTime = 0.03f;
        damage = 0.5f;
    }
    protected override void StartDrone()
    {
        base.StartDrone();
    }

    protected override void DroneAction()
    {
        DealDamage().Forget();
    }

    private async UniTaskVoid DealDamage()
    {
        while (true)
        {
            Collider[] col = Physics.OverlapSphere(player.transform.position, 3);
            foreach(var c in col)
            {
                // µñ¼Å³Ê¸®·Î ÆÄ¾Ç ÈÄ µ¥¹ÌÁö ÀÔÈ÷±â

            }
            await UniTask.Delay(TimeSpan.FromMilliseconds(30));
        }
    }


    private void IncreaseDamage(float dmg)
    {
        damage += dmg;
    }



}
