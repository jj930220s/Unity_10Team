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

    public ButtonInfo(string buttonName, UnityAction onclickEvent)
    {
        this.buttonName = buttonName;
        this.onclickEvent = onclickEvent;
    }

    public Button InitButton(Button button)
    {
        TextMeshProUGUI buttonName = button.GetComponentInChildren<TextMeshProUGUI>();
        buttonName.text = this.buttonName;

        button.onClick.AddListener(onclickEvent);

        return button;
    }
}

public class TitleUI : BaseUI
{
    [SerializeField] private RectTransform Title;
    [SerializeField] private RectTransform Buttons;
    [SerializeField] private Button titleButton;
    [SerializeField] private string gameSceneName;

    private ButtonInfo[] buttonInfos;

    public override void Init()
    {
        base.Init();
        UiType = UITYPE.TITLE;
        ButtonInfo[] buttonInfos =
        {
            new ButtonInfo("GameStart", () => SceneManager.LoadScene(gameSceneName)),
            new ButtonInfo("Upgrade", () => UIManager.Instance.OnUI(UITYPE.UPGRADE)),
            new ButtonInfo("Drons", () => UIManager.Instance.OnUI(UITYPE.BUILD)),
            new ButtonInfo("Setting", () => UIManager.Instance.OnUI(UITYPE.SETTING)),
#if UNITY_EDITOR
            new ButtonInfo("Exit", EditorApplication.ExitPlaymode)
#else
            new ButtonInfo("Exit", Application.Quit)
#endif
        };
        this.buttonInfos = buttonInfos;

        foreach (var info in buttonInfos)
            info.InitButton(Instantiate(titleButton, Buttons));
    }
}
