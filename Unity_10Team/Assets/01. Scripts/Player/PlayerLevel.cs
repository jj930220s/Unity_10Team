using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel
{
    public int level { get; private set; }
    public int exp { get; private set; }
    public int maxExp { get; private set; }

    public PlayerLevel(int level)
    {
        this.level = level;
        exp = 0;
        SetMaxExp();
    }

    public void AddExp(int exp)
    {
        this.exp += exp;
        if (this.exp >= maxExp)
        {
            this.exp -= maxExp;
            level++;
            SetMaxExp();
        }
    }

    private void SetMaxExp()
    {
        maxExp = level * 10;
    }
}
