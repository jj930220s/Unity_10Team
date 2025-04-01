using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEnforceSystem
{
    public PlayerEnforceSystem(Player player)
    {
        this.player = player;
    }

    Player player;

    public List<IEnforce> ShowSelectableEnforce()
    {
        List<IEnforce> selectTable = new List<IEnforce>(player.enforceList);

        for (int i = 0; i < selectTable.Count; i++)
        {
            int randomIndex = Random.Range(i, selectTable.Count);

            IEnforce temp = selectTable[i];
            selectTable[i] = selectTable[randomIndex];
            selectTable[randomIndex] = temp;
        }

        return selectTable.Take(3).ToList();
    }
}
