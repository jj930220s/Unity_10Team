using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void PerChase(WEALTHTYPE type, int amount)
    {
        wealths[type] -= amount;

        foreach (var baseWealth in baseWealths)
            baseWealth.amount = wealths[baseWealth.type];
        DataSave<PlayerWealth>.SaveData(this, "wealthData.json");
    }

    public void AddWealth(WEALTHTYPE type, int amount)
    {
        if (!wealths.ContainsKey(type))
        {
            wealths[type] = 0;
        }

        wealths[type] += amount;

        bool found = false;
        foreach (var baseWealth in baseWealths)
        {
            if (baseWealth.type == type)
            {
                baseWealth.amount = wealths[type];
                found = true;
                break;
            }
        }

        if (!found)
        {
            var tempList = baseWealths.ToList();
            tempList.Add(new Wealth { type = type, amount = wealths[type] });
            baseWealths = tempList.ToArray();
        }

        DataSave<PlayerWealth>.SaveData(this, "wealthData.json");
    }
}
