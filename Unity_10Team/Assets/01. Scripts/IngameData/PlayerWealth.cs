using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wealth
{
    public WEALTHTYPE type;
    public int amount;
}

[Serializable]
public class PlayerWealth
{
    [SerializeField] Wealth[] baseWealths;
    public Dictionary<WEALTHTYPE, int> wealths { get; private set; } = new();

    public void Init()
    {
        foreach (var stat in baseWealths)
            wealths[stat.type] = stat.amount;
    }
}
