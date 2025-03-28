using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DronPannel : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI dronName;
    [SerializeField] Image typeIcon;
    [SerializeField] Outline builded;
    Button clickButton;
    int idx;
    public bool isSelected
    {
        get => builded.enabled;
        set => builded.enabled = value;
    }

    public DronPannel Init(DronData data, UnityAction<int> onClick, int idx = 0)
    {
        icon.sprite = data.icon;
        dronName.text = data.dronName;

        clickButton = GetComponent<Button>();
        clickButton.onClick.AddListener(() => onClick(this.idx));
        isSelected = false;

        this.idx = idx;
        return this;
    }
}
