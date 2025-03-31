using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildSlot
{
    public int? idx;
    public DronData data;
}

public class BuildUI : BaseUI
{
    [SerializeField] RectTransform content;
    [SerializeField] DronPannel pannelPrefeb;
    [SerializeField] DronData[] dronDatas;
    List<DronPannel> dronPannels = new();

    public BuildSlot[] selectedDrons;

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
            int i = FindSelectedSlot(idx);
            selectedDrons[i].data = null;
            selectedDrons[i].idx = null;
            dronPannels[idx].isSelected = false;
        }
        else if (FindEmptySlot(out int i))
        {
            selectedDrons[i].data = dronDatas[idx];
            selectedDrons[i].idx = idx;
            dronPannels[idx].isSelected = true;
        }
    }

    bool FindEmptySlot(out int idx)
    {
        for (int i = 0; i < selectedDrons.Length; i++)
            if (selectedDrons[i].data == null)
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
            if (selectedDrons[i].idx == idx)
                return i;
        return -1;
    }
}
