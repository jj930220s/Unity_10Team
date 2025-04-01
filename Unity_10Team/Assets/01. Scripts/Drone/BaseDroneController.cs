using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDroneController : MonoBehaviour
{
    protected GameObject target;

    protected float delayTime = 1f;

    /// <summary>
    /// ��� �۵� ����
    /// </summary>
    protected virtual void StartDrone()
    {
        AutoAction().Forget();
    }

    /// <summary>
    /// delayTime �������� DroneAction ȣ��
    /// </summary>
    /// <returns></returns>
    protected async UniTaskVoid AutoAction()
    {
        while(true)
        {
            await UniTask.WaitUntil(() => target != null);

            // ��� ����
            DroneAction();

            await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
        }
    }


    /// <summary>
    /// ���� ����� �ϴ� �ൿ
    /// </summary>
    protected abstract void DroneAction();
}
