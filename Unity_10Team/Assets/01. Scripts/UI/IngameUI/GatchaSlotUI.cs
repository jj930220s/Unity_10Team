using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GatchaSlotUI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI labelTxt;
    [SerializeField] TextMeshProUGUI description;
    //IngameUpgradeData

    public GatchaSlotUI Init()
    {
        return this;
    }

    public void OnClick()
    {
        //ingameUpgrade.Apply();
    }
}
