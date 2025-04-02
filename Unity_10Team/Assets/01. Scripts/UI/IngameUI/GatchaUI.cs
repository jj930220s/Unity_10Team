using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaUI : BaseUI
{
    [SerializeField] GatchaData[] recentPickuped;

    public override void Init()
    {
        UiType = UITYPE.GATCHA;
        base.Init();
    }

    public override void PopUpAction()
    {
        base.PopUpAction();

        recentPickuped = IngameUpgradeManager.Instance.Gatcha();
    }
}
