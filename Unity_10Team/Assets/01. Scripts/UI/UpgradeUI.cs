using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : BaseUI
{
    [Header("Wealths")]
    [SerializeField] RectTransform wealth;
    [SerializeField] WealthUIData[] wealthInfos;
    [SerializeField] WealthUI wealthInfoPrefebs;
    Dictionary<WEALTHTYPE, WealthUI> wealthInfoUIs = new();

    [Header("PlayerStat")]

    [Header("Upgrade")]
    [SerializeField] RectTransform content;
    [SerializeField] UpgradePannel PannelPrefeb;
    [SerializeField] Upgrade[] upgrades;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.UPGRADE;

        foreach (var info in wealthInfos)
            wealthInfoUIs[info.wealthType] = Instantiate(wealthInfoPrefebs, wealth).Init(info);

        foreach (var upgrade in upgrades)
            Instantiate(PannelPrefeb, content);

        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (var info in wealthInfoUIs)
            if (GameManager.Instance.wealth.wealths.ContainsKey(info.Key))
                info.Value.UpdateInfo(GameManager.Instance.wealth.wealths[info.Key]);
    }
}
