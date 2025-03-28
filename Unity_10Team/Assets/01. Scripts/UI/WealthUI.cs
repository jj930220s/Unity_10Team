using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

public class WealthUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountTxt;

    public void UpdateInfo(int amount)
    {
        amountTxt.text = amount.ToString();
    }
}
