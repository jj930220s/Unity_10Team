using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngameUpgradeManager : Singleton<IngameUpgradeManager>
{
    [SerializeField] GatchaData[] gatchaList;

    //3°³ <=gatcha
    public GatchaData[] Gatcha()
    {
        GatchaData[] pickuped = gatchaList.OrderBy(x => Random.Range(1, 100)).Take(3).ToArray();

        return pickuped;
    }
}
