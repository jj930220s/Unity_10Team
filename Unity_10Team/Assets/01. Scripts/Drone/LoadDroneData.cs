using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDroneData : MonoBehaviour
{
    public SelectedDrons selectedDrons;

    string dataSavePath = "selectedDrons.json";

    public void LoadDrone()
    {
        SelectedDrons saveData = DataSave<SelectedDrons>.LoadData(dataSavePath);

        if (saveData != default(SelectedDrons))
            selectedDrons = saveData;
    }

    public SelectedDrons GetSelectedDrons()
    {
        return selectedDrons;
    }
}
