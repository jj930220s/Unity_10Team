using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATTYPE
{
    HP,
    CHP,
    ATK,
    DEF,
    SPEED,
    ATKDELAY
}

[Serializable]
public class PlayerStatus
{
    public PlayerStatus(Player player)
    {
        this.player = player;
    }

    private Player player;

    public Dictionary<STATTYPE, float> status { get; private set; } = new();

    public void Init()
    {
        status.Add(STATTYPE.HP, player.data.defaultData.baseHP);
        status.Add(STATTYPE.CHP, status[STATTYPE.HP]);
        status.Add(STATTYPE.ATK, player.data.defaultData.baseAttack);
        status.Add(STATTYPE.DEF, player.data.defaultData.baseDefence);
        status.Add(STATTYPE.SPEED, player.data.defaultData.baseSpeed);
        status.Add(STATTYPE.ATKDELAY, player.data.defaultData.baseAttackDelay);
    }

    public void TakeDamage(float damage)
    {
        status[STATTYPE.CHP] -= damage;
        if (status[STATTYPE.CHP] <= 0)
        {
            status[STATTYPE.CHP] = 0;
        }
    }
}
