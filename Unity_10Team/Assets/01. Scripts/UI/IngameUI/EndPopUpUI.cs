using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPopUpUI : BaseUI
{
    public override void Init()
    {
        UiType = UITYPE.GAMEOVER;
        base.Init();
    }
}
