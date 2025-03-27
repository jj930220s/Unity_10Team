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
    public UnityAction onclickEvent;
    public Button.ButtonClickedEvent onClickEvent;

    public ButtonInfo(string buttonName, UnityAction onclickEvent)
    {
        this.buttonName = buttonName;
        this.onclickEvent = onclickEvent;
    }

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
    [SerializeField] private RectTransform Buttons;
    [SerializeField] private Button titleButton;
    [SerializeField] private string gameSceneName;

    [SerializeField] private ButtonInfo[] buttonInfos;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.TITLE;

        foreach (var info in buttonInfos)
            info.InitButton(Instantiate(titleButton, Buttons));
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(gameSceneName);
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
