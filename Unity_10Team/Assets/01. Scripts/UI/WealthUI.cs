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
}

public class WealthUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountTxt;

    public WealthUI Init(WealthUIData data)
    {
        icon.sprite = data.icon;
        amountTxt.color = data.textColor;

        return this;
    }

    public void UpdateInfo(int amount)
    {
        amountTxt.text = amount.ToString();
    }
}
