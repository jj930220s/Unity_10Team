using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingOption : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI optionName;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI value;

    public void Init(string name, int value = 100)
    {
        optionName.text = name;
        value = Mathf.Clamp(value, 0, 100);
        slider.value = value / 100f;
        slider.onValueChanged.Invoke(slider.value);
    }

    public void Init(OptionData data)
    {
        slider.onValueChanged = data.onValueChanged;
        slider.onValueChanged.AddListener(OnValueChange);
        Init(data.OptionName);
    }

    public void OnValueChange(float changed)
    {
        value.text = (changed * 100).ToString("F0");
    }
}
