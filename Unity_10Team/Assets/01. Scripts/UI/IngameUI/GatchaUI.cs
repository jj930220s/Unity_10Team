using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatchaUI : BaseUI
{
    [SerializeField] GatchaData[] recentPickuped;
    [SerializeField] GatchaSlotUI[] slotUIs;

    public override void Init()
    {
        UiType = UITYPE.GATCHA;
        base.Init();
    }

    public override void PopUpAction()
    {
        base.PopUpAction();

        recentPickuped = IngameUpgradeManager.Instance.Gatcha();
        UpdateUI();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        for (int i = 0; i < recentPickuped.Length; i++)
            slotUIs[i].Init(recentPickuped[i]);
    }
}
