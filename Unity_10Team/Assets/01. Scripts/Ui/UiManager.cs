using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    [field: SerializeField] public BaseUI[] Uis { get; private set; }

    private void Awake()
    {
        Uis = GetComponentsInChildren<BaseUI>();
        foreach (var ui in Uis)
            ui.Init();
        OnUi(UITYPE.TITLE);
    }

    protected override void Init()
    {
        base.Init();
        _instance = this;
    }

    public void OnUi(UITYPE type)
    {
        foreach (var ui in Uis)
            ui.gameObject.SetActive(ui.UiType == type);
    }
}
