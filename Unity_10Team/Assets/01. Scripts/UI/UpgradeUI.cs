using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEALTHTYPE
{
    Gold,
    Dia
}

[Serializable]
public class WealthUIData
{
    public WEALTHTYPE wealthType;
    public Sprite icon;
    public Color textColor;

    public WealthUI Init(WealthUI ui)
    {
        ui.icon.sprite = icon;
        ui.amountTxt.color = textColor;

        return ui;
    }
}

public class UpgradeUI : BaseUI
{
    [Header("Wealths")]
    [SerializeField] RectTransform wealth;
    [SerializeField] WealthUIData[] wealthInfos;
    [SerializeField] WealthUI wealthInfoPrefebs;
    Dictionary<WEALTHTYPE, WealthUI> infoUIs = new();

    [Header("Upgrade")]
    [SerializeField] RectTransform content;
    [SerializeField] UpgradePannel PannelPrefeb;
    [SerializeField] Upgrade[] upgrades;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.UPGRADE;

        foreach (var info in wealthInfos)
            infoUIs[info.wealthType] = info.Init(Instantiate(wealthInfoPrefebs, wealth));

        foreach (var upgrade in upgrades)
            Instantiate(PannelPrefeb, content);

        UpdateUI();
    }

    void UpdateUI()
    {
        //foreach (var info in infoUIs)
        //    info = player.wealth[info.Key];
    }
}
