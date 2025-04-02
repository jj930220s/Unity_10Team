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
    Status[] upgradeStats;

    public GatchaSlotUI Init(GatchaData data)
    {
        icon.sprite = data.icon;
        labelTxt.text = data.upgaradeName;
        upgradeStats = data.upgradeStats;

        string des = "";
        foreach (var stat in upgradeStats)
            des += stat.type.ToString() + " + " + stat.value.ToString() +"\n";
        description.text = des;

        return this;
    }

    public void OnClick()
    {
        foreach (var stat in upgradeStats)
            GameManager.Instance.pStat.UpgradStat(stat.type, stat.value);
        UIManager.Instance.PopUpUI(UITYPE.GATCHA);
    }
}
