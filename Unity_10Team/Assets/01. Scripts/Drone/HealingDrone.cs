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
    }

    private void SetAbility()
    {
        delayTime = 5f;
        heal = 10;
    }

    protected override void DroneAction()
    {
        Healing();
    }

    private void Healing()
    {
        // 플레이어 체력 회복
        //player

    }
}
