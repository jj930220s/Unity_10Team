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
        // �۵� ������ �� �ؾ��ϴ� �� ������ ���⿡�� �ϱ�


        base.StartDrone();
    }


    /// <summary>
    /// ����� �ؾ� �� ��(ex ����, ȸ�� ��)
    /// </summary>
    protected override void DroneAction()
    {

    }

}
