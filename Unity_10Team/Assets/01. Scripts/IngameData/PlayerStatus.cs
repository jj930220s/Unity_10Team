using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    [SerializeField] Status[] baseStat;
    public Dictionary<STATTYPE, int> status { get; private set; } = new();

    public void Init()
    {
        foreach (var stat in baseStat)
            status[stat.type] = stat.value;
    }
}
