using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Upgrade
{
    public UpgradeData data;
    public bool upgraded;
}

[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public Sprite icon;
    public string description;
    public int cost;
    public Status[] upgradeStat;
}
