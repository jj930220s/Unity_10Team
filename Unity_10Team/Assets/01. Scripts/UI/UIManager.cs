using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private RectTransform menuUI;
    [field: SerializeField] public BaseUI[] UIList { get; private set; }

    private void Awake()
    {
        UIList = GetComponentsInChildren<BaseUI>();
        foreach (var ui in UIList)
            ui.Init();
        OnUI(UITYPE.TITLE);
    }

    protected override void Init()
    {
        base.Init();
        _instance = this;
    }

    public void OnUI(UITYPE type)
    {
        foreach (var ui in UIList)
            ui.gameObject.SetActive(ui.UiType == type);

        menuUI.gameObject.SetActive(type != UITYPE.TITLE);
    }

    public void OnUI(BaseUI onUI)
    {
        OnUI(onUI.UiType);
    }
}
