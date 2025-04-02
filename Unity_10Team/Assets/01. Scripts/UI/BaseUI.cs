using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum UITYPE
{
    TITLE,
    UPGRADE,
    BUILD,
    SETTING,
    GATCHA,
    GAMEOVER
}

public class BaseUI : MonoBehaviour
{
    public UITYPE UiType { get; protected set; }

    public virtual void Init()
    {
        UpdateUI();
    }

    protected virtual void UpdateUI()
    {

    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnGameTitle()
    {
        SceneManager.LoadScene(0);
    }

    public virtual void PopUpAction()
    { 

    }
}
