using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : BaseUI
{
    [Header("Player Status")]
    [SerializeField] Slider hpBar;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI level;

    [Header("Wealth")]
    [SerializeField] RectTransform wealth;
    [SerializeField] WealthUIInfos wealthInfos;
    [SerializeField] WealthUI wealthInfoPrefeb;
    Dictionary<WEALTHTYPE, WealthUI> wealthInfoUIs = new();

    public override void Init()
    {
        UiType = UITYPE.TITLE;

        foreach (var info in wealthInfos.list)
            wealthInfoUIs[info.wealthType] = Instantiate(wealthInfoPrefeb, wealth).Init(info);

        base.Init();
    }
}
