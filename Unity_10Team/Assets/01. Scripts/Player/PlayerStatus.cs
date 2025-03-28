using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATTYPE
{
    HP,
    ATK,
    DEF,
    HPGEN,
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
        status.Add(STATTYPE.ATK, player.data.defaultData.baseAttack);
        status.Add(STATTYPE.DEF, player.data.defaultData.baseDefence);
        status.Add(STATTYPE.HPGEN, 0f); // 나중에 구현
        status.Add(STATTYPE.SPEED, player.data.defaultData.baseSpeed);
        status.Add(STATTYPE.ATKDELAY, player.data.defaultData.baseAttackDelay);
    }
}
