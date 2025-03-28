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
        
    }
}
