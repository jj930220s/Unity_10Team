using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUIData : ScriptableObject
{
    public string OptionName;
    public Slider.SliderEvent onValueChanged;
    public virtual float initValue { get; }
}

public class SettingUI : BaseUI
{
    [SerializeField] RectTransform optionArea;
    [SerializeField] SettingOptionUI optionPrefeb;
    [SerializeField] OptionUIData[] datas;
    Dictionary<OptionUIData, SettingOptionUI> optionUIs = new();

    public override void Init()
    {
        UiType = UITYPE.SETTING;

        foreach (var data in datas)
            optionUIs[data] = Instantiate(optionPrefeb, optionArea).Init(data);

        base.Init();
    }
}
