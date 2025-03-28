using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : BaseUI
{
    [Header("Wealths")]
    [SerializeField] RectTransform wealth;
    [SerializeField] WealthUIData[] wealthInfos;
    [SerializeField] WealthUI wealthInfoPrefeb;
    Dictionary<WEALTHTYPE, WealthUI> wealthInfoUIs = new();

    [Header("PlayerStat")]
    [SerializeField] RectTransform playerStat;
    [SerializeField] StatusUIData[] statInfos;
    [SerializeField] StatusUI StatusPrefeb;
    Dictionary<STATTYPE, StatusUI> statInfoUIs = new();

    [Header("Upgrade")]
    [SerializeField] RectTransform content;
    [SerializeField] UpgradePannel pannelPrefeb;
    [SerializeField] Upgrade[] upgrades;
    List<UpgradePannel> upgradePannels = new();

    [Header("SelectedUpgrade")]
    [SerializeField] Image selectedIcon;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI upgradeDesc;
    [SerializeField] Button upgradeButton;
    Upgrade selectedUpgrade;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.UPGRADE;

        foreach (var info in wealthInfos)
            wealthInfoUIs[info.wealthType] = Instantiate(wealthInfoPrefeb, wealth).Init(info);

        foreach (var info in statInfos)
            statInfoUIs[info.statType] = Instantiate(StatusPrefeb, playerStat).Init(info);

        foreach (var upgrade in upgrades)
            upgradePannels.Add(Instantiate(pannelPrefeb, content).Init(upgrade, UpgradeSelected, upgradePannels.Count));

        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (var info in wealthInfoUIs)
            if (GameManager.Instance.wealth.wealths.ContainsKey(info.Key))
                info.Value.UpdateInfo(GameManager.Instance.wealth.wealths[info.Key]);

        foreach (var info in statInfoUIs)
            if (GameManager.Instance.player.pStat.status.ContainsKey(info.Key))
                info.Value.UpdateInfo(GameManager.Instance.player.pStat.status[info.Key]);

        foreach (var pannel in upgradePannels)
            pannel.UpdateInfo();

        upgradeName.text = upgradeDesc.text = string.Empty;
        selectedIcon.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(false);
    }

    public void UpgradeSelected(int idx)
    {
        UpdateUI();
        selectedUpgrade = upgrades[idx];

        if (selectedUpgrade == null || selectedUpgrade.upgraded) return;

        selectedIcon.gameObject.SetActive(true);
        upgradeButton.gameObject.SetActive(selectedUpgrade.data.cost <= GameManager.Instance.wealth.wealths[WEALTHTYPE.Gold]);

        selectedIcon.sprite = selectedUpgrade.data.icon;
        upgradeName.text = selectedUpgrade.data.upgradeName;
        if (!upgradeButton.gameObject.activeSelf)
            upgradeName.text += " (Can't Upgrade)";
        upgradeDesc.text = selectedUpgrade.data.description;

        foreach (var upgradeStat in selectedUpgrade.data.upgradeStat)
            statInfoUIs[upgradeStat.type].UpdateUpgradeAmount(upgradeStat.value);
    }

    public void OnUpgrade()
    {
        GameManager.Instance.wealth.PerChase(WEALTHTYPE.Gold, selectedUpgrade.data.cost);
        selectedUpgrade.ApplyUpgrade(GameManager.Instance.player);

        UpdateUI();
    }
}
