using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UpgradeList
{
    public Upgrade[] list;
}

public class UpgradeUI : BaseUI
{
    [Header("Wealths")]
    [SerializeField] PlayerWealth baseWealth;
    [SerializeField] RectTransform wealth;
    [SerializeField] WealthUIInfos wealthInfos;
    [SerializeField] WealthUI wealthInfoPrefeb;
    Dictionary<WEALTHTYPE, WealthUI> wealthInfoUIs = new();

    [Header("PlayerStat")]
    [SerializeField] PlayerSObj baseStat;
    PlayerStatus pStat;
    [SerializeField] RectTransform playerStat;
    [SerializeField] StatusUIData[] statInfos;
    [SerializeField] StatusUI StatusPrefeb;
    Dictionary<STATTYPE, StatusUI> statInfoUIs = new();

    [Header("Upgrade")]
    [SerializeField] RectTransform content;
    [SerializeField] UpgradePannel pannelPrefeb;
    [SerializeField] UpgradeList upgrades;
    List<UpgradePannel> upgradePannels = new();
    string dataSavePath = "upgradeList.json";

    [Header("SelectedUpgrade")]
    [SerializeField] Image selectedIcon;
    [SerializeField] TextMeshProUGUI upgradeName;
    [SerializeField] TextMeshProUGUI upgradeDesc;
    [SerializeField] Button upgradeButton;
    Upgrade selectedUpgrade;

    public override void Init()
    {
        UiType = UITYPE.UPGRADE;

        baseWealth = DataSave<PlayerWealth>.LoadOrBase(baseWealth, "wealthData.json");
        baseWealth.Init();
        foreach (var info in wealthInfos.list)
            wealthInfoUIs[info.wealthType] = Instantiate(wealthInfoPrefeb, wealth).Init(info);

        pStat = new PlayerStatus(baseStat);
        pStat.Init();
        foreach (var info in statInfos)
            statInfoUIs[info.statType] = Instantiate(StatusPrefeb, playerStat).Init(info);

        UpgradeList saveData = DataSave<UpgradeList>.LoadData(dataSavePath);
        if (saveData != default(UpgradeList))
            upgrades = saveData;

        foreach (var upgrade in upgrades.list)
            upgradePannels.Add(Instantiate(pannelPrefeb, content).Init(upgrade, UpgradeSelected, upgradePannels.Count));

        base.Init();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        foreach (var info in wealthInfoUIs)
            if (baseWealth.wealths.ContainsKey(info.Key))
                info.Value.UpdateInfo(baseWealth.wealths[info.Key]);

        foreach (var info in statInfoUIs)
            if (pStat.status.ContainsKey(info.Key))
                info.Value.UpdateInfo((int)pStat.status[info.Key]);

        foreach (var pannel in upgradePannels)
            pannel.UpdateInfo(baseWealth);

        upgradeName.text = upgradeDesc.text = string.Empty;
        selectedIcon.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(false);
    }

    public void UpgradeSelected(int idx)
    {
        UpdateUI();
        selectedUpgrade = upgrades.list[idx];

        if (selectedUpgrade == null || selectedUpgrade.upgraded) return;

        selectedIcon.gameObject.SetActive(true);
        upgradeButton.gameObject.SetActive(selectedUpgrade.data.cost <= baseWealth.wealths[WEALTHTYPE.Gold]);

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
        baseWealth.PerChase(WEALTHTYPE.Gold, selectedUpgrade.data.cost);
        selectedUpgrade.ApplyUpgrade(pStat);

        pStat.SaveStatus();
        DataSave<UpgradeList>.SaveData(upgrades, dataSavePath);
        UpdateUI();
    }

    private void OnDestroy()
    {
        pStat.SaveStatus();
        DataSave<UpgradeList>.SaveData(upgrades, dataSavePath);
    }
}
