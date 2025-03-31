using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class EndPopUpUI : BaseUI
{
    [SerializeField] TextMeshProUGUI resultTxt;
    public override void Init()
    {
        UiType = UITYPE.GAMEOVER;
        base.Init();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        int playTime = (int)(Time.time - GameManager.Instance.gameStartTime);
        string playTimeTxt = $"{playTime / 60:D2}:{playTime % 60:D2}";

        resultTxt.text = playTimeTxt + $"\n{GameManager.Instance.score}";
    }

    private void OnEnable()
    {
        UpdateUI();
    }
}
