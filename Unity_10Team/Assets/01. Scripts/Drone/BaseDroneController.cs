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
    /// 드론 작동 시작
    /// </summary>
    protected virtual void StartDrone()
    {
        AutoAction().Forget();
    }

    /// <summary>
    /// delayTime 간격으로 DroneAction 호출
    /// </summary>
    /// <returns></returns>
    protected async UniTaskVoid AutoAction()
    {
        while(true)
        {
            await UniTask.WaitUntil(() => target != null);

            // 드론 동작
            DroneAction();

            await UniTask.Delay(TimeSpan.FromSeconds(delayTime));
        }
    }


    /// <summary>
    /// 실제 드론이 하는 행동
    /// </summary>
    protected abstract void DroneAction();
}
