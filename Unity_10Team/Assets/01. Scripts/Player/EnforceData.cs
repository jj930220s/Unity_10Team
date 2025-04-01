using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnforce
{
    public void EnforceToPlayer(Player player);
}

[Serializable]
public class EnforceStatusData : IEnforce
{
    public STATTYPE enforceStatType;
    public float enforceValue;

    public void EnforceToPlayer(Player player)
    {
        if (enforceStatType == STATTYPE.HP)
        {
            player.pStat.status[enforceStatType] += enforceValue;
            player.pStat.status[STATTYPE.CHP] += enforceValue;
        }
        else if (enforceStatType == STATTYPE.ATKDELAY)
        {
            player.pStat.status[enforceStatType] += enforceValue;

            if (player.pStat.status[STATTYPE.ATKDELAY] <= 0.25f)
            {
                player.enforceList.RemoveAll(enforce => 
                enforce is EnforceStatusData data && 
                data.enforceStatType == STATTYPE.ATKDELAY);
            }
        }
        else
        {
            player.pStat.status[enforceStatType] += enforceValue;
        }
    }
}

[Serializable]
public class EnforceDroneData : IEnforce
{
    public void EnforceToPlayer(Player player)
    { 
    }
}

[CreateAssetMenu(fileName = "Enforce")]
public class EnforceData : ScriptableObject
{
    public EnforceStatusData[] EnforceStatusList;
}
