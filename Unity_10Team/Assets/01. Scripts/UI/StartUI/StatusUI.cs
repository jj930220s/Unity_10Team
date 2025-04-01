using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class StatusUIData
{
    public string statName;
    public STATTYPE statType;
    public Sprite icon;
}

public class StatusUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI StatLabel;
    public TextMeshProUGUI StatValue;
    public TextMeshProUGUI UpgradeValue;

    public StatusUI Init(StatusUIData data)
    {
        icon.sprite = data.icon;
        StatLabel.text = data.statName;

        return this;
    }

    public void UpdateInfo(int statAmount, int upgradeAmount = 0)
    {
        StatValue.text = statAmount.ToString();

        UpdateUpgradeAmount(upgradeAmount);
    }

    public void UpdateUpgradeAmount(int upgradeAmount)
    {
        if (upgradeAmount > 0)
            UpgradeValue.text = "(+ {0})";
        else if (upgradeAmount < 0)
            UpgradeValue.text = "(- {0})";
        else
            UpgradeValue.text = "";
        UpgradeValue.text = string.Format(UpgradeValue.text, Mathf.Abs(upgradeAmount));
    }
}
