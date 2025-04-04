using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingOptionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI optionName;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI value;


    public SettingOptionUI Init(OptionUIData data)
    {
        slider.onValueChanged = data.onValueChanged;
        slider.onValueChanged.AddListener(OnValueChange);

        optionName.text = data.OptionName;
        SetValue(data.initValue);

        return this;
    }

    public void SetValue(float value = 100)
    {
        value = Mathf.Clamp(value, 0, 100);
        slider.value = value / 100f;
        slider.onValueChanged.Invoke(slider.value);
    }

    public void OnValueChange(float changed)
    {
        value.text = (changed * 100).ToString("F0");
    }
}
