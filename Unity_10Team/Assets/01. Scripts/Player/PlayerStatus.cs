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
    public PlayerStatus(PlayerSObj baseStat)
    {
        this.baseStat = baseStat;
    }

    PlayerSObj baseStat;

    public Dictionary<STATTYPE, float> status { get; private set; } = new();

    public void Init()
    {
        PlayerSaveData savedata = DataSave<PlayerSaveData>.LoadData("statData.json");
        if(savedata != null)
        {
            status[STATTYPE.HP] = savedata.hp;
            status[STATTYPE.CHP] = status[STATTYPE.HP];
            status[STATTYPE.ATK] = savedata.attack;
            status[STATTYPE.DEF] = savedata.defence;
            status[STATTYPE.SPEED] = savedata.speed;
            status[STATTYPE.ATKDELAY] = savedata.attackDelay;

            return;
        }

        status[STATTYPE.HP] = baseStat.defaultData.baseHP;
        status[STATTYPE.CHP] = status[STATTYPE.HP];
        status[STATTYPE.ATK] = baseStat.defaultData.baseAttack;
        status[STATTYPE.DEF] = baseStat.defaultData.baseDefence;
        status[STATTYPE.SPEED] = baseStat.defaultData.baseSpeed;
        status[STATTYPE.ATKDELAY] = baseStat.defaultData.baseAttackDelay;
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

    public void UpgradStat(STATTYPE type, float value)
    {
        status[type] += value;
    }

    public void SaveStatus()
    {
        PlayerSaveData savedata = new PlayerSaveData(this);

        DataSave<PlayerSaveData>.SaveData(savedata, "statData.json");
    }
}
