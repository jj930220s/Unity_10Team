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

    public void SetPannel(Upgrade upgrade)
    {
        icon.sprite = upgrade.icon;
        cost.text = upgrade.cost.ToString();
        saledMask.gameObject.SetActive(!upgrade.upgraded);
    }
}
