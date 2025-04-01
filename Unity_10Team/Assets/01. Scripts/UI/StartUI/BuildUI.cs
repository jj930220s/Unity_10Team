using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildSlot
{
    public int idx;
    public DronData data;
}

[Serializable]
public class SelectedDrons
{
    public BuildSlot[] list;
}

public class BuildUI : BaseUI
{
    [SerializeField] RectTransform content;
    [SerializeField] DronPannel pannelPrefeb;
    [SerializeField] DronData[] dronDatas;
    List<DronPannel> dronPannels = new();

    public SelectedDrons selectedDrons;
    string dataSavePath = "selectedDrons.json";

    public override void Init()
    {
        UiType = UITYPE.BUILD;

        SelectedDrons saveData = DataSave<SelectedDrons>.LoadData(dataSavePath);
        if (saveData != default(SelectedDrons))
            selectedDrons = saveData;

        foreach (var data in dronDatas)
            dronPannels.Add(Instantiate(pannelPrefeb, content).Init(data, SetDron, dronPannels.Count));

        foreach(var slot in selectedDrons.list)
        {
            if (slot.data != null)
                dronPannels[slot.idx].isSelected = true;
        }

        base.Init();
    }

    void SetDron(int idx)
    {
        if (dronPannels[idx].isSelected)
        {
            int i = FindSelectedSlot(idx);
            selectedDrons.list[i].data = null;
            selectedDrons.list[i].idx = -1;
            dronPannels[idx].isSelected = false;
        }
        else if (FindEmptySlot(out int i))
        {
            selectedDrons.list[i].data = dronDatas[idx];
            selectedDrons.list[i].idx = idx;
            dronPannels[idx].isSelected = true;
        }

        DataSave<SelectedDrons>.SaveData(selectedDrons, dataSavePath);
    }

    bool FindEmptySlot(out int idx)
    {
        for (int i = 0; i < selectedDrons.list.Length; i++)
            if (selectedDrons.list[i].data == null)
            {
                idx = i;
                return true;
            }

        idx = -1;

        return false;
    }

    int FindSelectedSlot(int idx)
    {
        for (int i = 0; i < selectedDrons.list.Length; i++)
            if (selectedDrons.list[i].idx == idx)
                return i;
        return -1;
    }
}
