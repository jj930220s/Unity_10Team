using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : BaseUI
{
    [SerializeField] RectTransform content;
    [SerializeField] DronPannel pannelPrefeb;
    [SerializeField] DronData[] dronDatas;
    List<DronPannel> dronPannels = new();

    public DronData[] selectedDrons;

    public override void Init()
    {
        UiType = UITYPE.BUILD;

        foreach (var data in dronDatas)
            dronPannels.Add(Instantiate(pannelPrefeb, content).Init(data, SetDron, dronPannels.Count));

        base.Init();
    }

    void SetDron(int idx)
    {
        if (dronPannels[idx].isSelected)
        {
            selectedDrons[FindSelectedSlot(idx)] = null;
            dronPannels[idx].isSelected = false;
        }
        else if (FindEmptySlot(out int i))
        {
            selectedDrons[i] = dronDatas[idx];
            dronPannels[idx].isSelected = true;
        }
    }

    bool FindEmptySlot(out int idx)
    {
        for (int i = 0; i < selectedDrons.Length; i++)
            if (selectedDrons[i] == null)
            {
                idx = i;
                return true;
            }

        idx = -1;

        return false;
    }

    int FindSelectedSlot(int idx)
    {
        for (int i = 0; i < selectedDrons.Length; i++)
            if (selectedDrons[i] == dronDatas[idx])
                return i;
        return -1;
    }
}
