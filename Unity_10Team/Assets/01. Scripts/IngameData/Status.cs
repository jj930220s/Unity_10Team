using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATTYPE
{
    HP,
    ATK,
    DEF,
    HPGEN
}

[Serializable]
public class Status
{
    public STATTYPE type;
    public int value;
}
