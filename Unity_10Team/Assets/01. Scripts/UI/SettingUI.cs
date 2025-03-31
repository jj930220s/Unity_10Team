using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class OptionUIData
{
    public string OptionName;
    public Slider.SliderEvent onValueChanged;

    public SettingOptionUI Init(SettingOptionUI option)
    {
        option.Init(this);

        return option;
    }
}

public class SettingUI : BaseUI
{
    [SerializeField] RectTransform optionArea;
    [SerializeField] SettingOptionUI optionPrefeb;
    [SerializeField] OptionUIData[] datas;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.SETTING;

        foreach (var data in datas)
            data.Init(Instantiate(optionPrefeb, optionArea));
    }
}
