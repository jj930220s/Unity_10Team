using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class ButtonInfo
{
    public string buttonName;
    public Button.ButtonClickedEvent onClickEvent;

    public Button InitButton(Button button)
    {
        TextMeshProUGUI buttonName = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonName.text = this.buttonName;

        button.onClick = onClickEvent;

        return button;
    }
}

public class TitleUI : BaseUI
{
    [SerializeField] private RectTransform Title;
    [SerializeField] private string gameSceneName;

    [Header("ButtonInitInfo")]
    [SerializeField] private RectTransform Buttons;
    [SerializeField] private Button titleButtonPrepeb;
    [SerializeField] private ButtonInfo[] buttonsInfo;

    public override void Init()
    {
        UiType = UITYPE.TITLE;

        foreach (var info in buttonsInfo)
            info.InitButton(Instantiate(titleButtonPrepeb, Buttons));

        base.Init();
    }

    public void OnExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
