using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    [field: SerializeField] public BaseUi[] Uis { get; private set; }

    private void Awake()
    {
        Uis = GetComponentsInChildren<BaseUi>();
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
