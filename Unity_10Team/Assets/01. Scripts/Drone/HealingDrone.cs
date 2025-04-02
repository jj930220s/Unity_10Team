using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingDrone : BaseDroneController
{
    float heal;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.player;

        SetAbility();
        StartDrone();
    }

    private void SetAbility()
    {
        delayTime = 5f;
        heal = 10;
        target = player.gameObject;
    }

    protected override void DroneAction()
    {
        Healing();
    }

    private void Healing()
    {
        // 플레이어 체력 회복
        GameManager.Instance.pStat.Heal(heal);

    }
}
