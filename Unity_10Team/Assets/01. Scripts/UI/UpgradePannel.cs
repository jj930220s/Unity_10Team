using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePannel : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] Image saledMask;
    [SerializeField] Color costEnabled;
    [SerializeField] Color costDisabled;
    Upgrade upgrade;
    int idx;

    public UpgradePannel Init(Upgrade upgrade, int idx = 0)
    {
        this.upgrade = upgrade;
        icon.sprite = upgrade.data.icon;
        cost.text = upgrade.data.cost.ToString();

        this.idx = idx;

        return this;
    }

    public void UpdateInfo()
    {
        if (!upgrade.upgraded)
            cost.color = upgrade.data.cost > GameManager.Instance.wealth.wealths[WEALTHTYPE.Gold] ? costDisabled : costEnabled;
        else
            cost.text = "";

        saledMask.gameObject.SetActive(upgrade.upgraded);
    }
}
