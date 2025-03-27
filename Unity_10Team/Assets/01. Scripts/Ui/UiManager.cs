using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public BaseUi[] uis;

    public void OnUi(UITYPE type)
    {
        foreach (var ui in uis)
            ui.gameObject.SetActive(ui.UiType == type);
    }
}
