using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class OptionData
{
    public string OptionName;
    public Slider.SliderEvent onValueChanged;

    public SettingOption Init(SettingOption option)
    {
        option.Init(this);

        return option;
    }
}

public class SettingUI : BaseUI
{
    [SerializeField] SettingOption optionPrefeb;
    [SerializeField] OptionData[] datas;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.SETTING;

        foreach (var data in datas)
            data.Init(Instantiate(optionPrefeb, transform));
    }
}
