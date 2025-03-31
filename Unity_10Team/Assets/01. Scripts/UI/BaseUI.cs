using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum UITYPE
{
    TITLE,
    UPGRADE,
    BUILD,
    SETTING,
    GATCHA,
    GAMEOVER
}

public class BaseUI : MonoBehaviour
{
    public UITYPE UiType { get; protected set; }

    public virtual void Init()
    {
        UpdateUI();
    }

    protected virtual void UpdateUI()
    {

    }
}
