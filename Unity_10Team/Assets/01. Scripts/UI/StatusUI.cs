using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusUIData
{
    public string statName;
    public STATTYPE statType;
    public Sprite icon;
}

public class StatusUI : MonoBehaviour
{
    public StatusUI Init(StatusUIData data)
    {
        return this;
    }

    public void UpdateInfo(int statAmount, int upgradeAmount = 0)
    {

    }
}
