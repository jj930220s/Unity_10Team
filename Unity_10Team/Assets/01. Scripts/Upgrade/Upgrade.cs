using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Upgrade", menuName = "Data/Upgrade")]
[Serializable]
public class Upgrade
{
    public string name;
    public Sprite icon;
    public int cost;
    public bool upgraded;
    public Status[] upgradeStat;
}
