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

    public void ApplyUpgrade(Player player)
    {
        data.ApplyUpgrade(player);
        upgraded = true;
    }
}

[Serializable]
public class Status
{
    public STATTYPE type;
    public int value;
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public string description;
    public int cost;
    public Status[] upgradeStat;

    public void ApplyUpgrade(Player player)
    {
        foreach (var stat in upgradeStat)
            Debug.Log(player.pStat.status[stat.type] + stat.value);
    }
}
