using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDroneData : MonoBehaviour
{
    public SelectedDrons selectedDrons;

    string dataSavePath = "selectedDrons.json";

    // Start is called before the first frame update
    void Start()
    {
        LoadDrone();   
    }


    private void LoadDrone()
    {
        SelectedDrons saveData = DataSave<SelectedDrons>.LoadData(dataSavePath);

        if (saveData != default(SelectedDrons))
            selectedDrons = saveData;

        foreach (var slot in selectedDrons.list)
        {
            if (slot.data != null)
            {

            }
        }
    }

    public SelectedDrons GetSelectedDrons()
    {
        return selectedDrons;
    }
}
