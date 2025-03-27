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
    protected UITYPE uiType;
    public UITYPE UiType => uiType;
}
