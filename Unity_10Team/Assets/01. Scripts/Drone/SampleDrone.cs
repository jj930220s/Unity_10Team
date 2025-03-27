using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleDrone : BaseDroneController
{

    // Start is called before the first frame update
    void Start()
    {
        SetAbility();

        StartDrone();
    }

    private void SetAbility()
    {
        delayTime = 3;
        target = this.gameObject;
    }

    protected override void StartDrone()
    {
        // 작동 시작할 때 해야하는 일 있으면 여기에서 하기


        base.StartDrone();
    }


    /// <summary>
    /// 드론이 해야 할 일(ex 공격, 회복 등)
    /// </summary>
    protected override void DroneAction()
    {

    }

}
