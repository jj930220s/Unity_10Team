using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : BaseUI
{
    [SerializeField] RectTransform content;
    [SerializeField] DronPannel pannelPrefeb;
    [SerializeField] DronData[] dronDatas;
    List<DronPannel> dronPannels = new();

    public override void Init()
    {
        UiType = UITYPE.BUILD;

        foreach (var data in dronDatas)
            dronPannels.Add(Instantiate(pannelPrefeb, content).Init(data,SetDron,dronPannels.Count));

        base.Init();
    }

    void SetDron(int idx)
    {
        Debug.Log($"dron{idx} is selected");
    }
}
