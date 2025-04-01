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
        status[STATTYPE.HP] = player.data.defaultData.baseHP;
        status[STATTYPE.CHP] = status[STATTYPE.HP];
        status[STATTYPE.ATK] = player.data.defaultData.baseAttack;
        status[STATTYPE.DEF] = player.data.defaultData.baseDefence;
        status[STATTYPE.SPEED] = player.data.defaultData.baseSpeed;
        status[STATTYPE.ATKDELAY] = player.data.defaultData.baseAttackDelay;
    }

    public void TakeDamage(float damage)
    {
        status[STATTYPE.CHP] -= damage;
        if (status[STATTYPE.CHP] <= 0)
        {
            status[STATTYPE.CHP] = 0;
        }
    }

    public void Heal(float heal)
    {
        status[STATTYPE.CHP] += heal;
        if (status[STATTYPE.CHP] >= status[STATTYPE.HP])
        {
            status[STATTYPE.CHP] = status[STATTYPE.HP];
        }
    }

    public float PlayerDamage()
    {
        return status[STATTYPE.ATK];
    }
}
