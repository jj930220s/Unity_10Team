using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Upgrade
{
    public UpgradeData data;
    public bool upgraded;

    public void ApplyUpgrade(PlayerStatus player)
    {
        data.ApplyUpgrade(player);
        upgraded = true;
    }
}

[Serializable]
public class Status
{
    public STATTYPE type;
    public float value;
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public string description;
    public int cost;
    public Status[] upgradeStat;

    public void ApplyUpgrade(PlayerStatus pStat)
    {
        foreach (var stat in upgradeStat)
            pStat.UpgradStat(stat.type, stat.value);
    }
}
