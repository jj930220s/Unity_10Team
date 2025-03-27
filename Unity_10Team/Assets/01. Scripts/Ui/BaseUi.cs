using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UITYPE
{
    TITLE,
    UPGRADE,
    BUILD,
    SETTING
}

public class BaseUi : MonoBehaviour
{
    public UITYPE UiType { get; protected set; }

    public virtual void Init()
    {

    }
}
