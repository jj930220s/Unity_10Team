using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : BaseUI
{
    [SerializeField] TextMeshProUGUI timeTxt;

    [Header("Player Status")]
    [SerializeField] Slider hpBar;
    [SerializeField] Slider expBar;
    [SerializeField] TextMeshProUGUI level;
    Player player;

    [Header("Wealth")]
    [SerializeField] RectTransform wealth;
    [SerializeField] WealthUIInfos wealthInfos;
    [SerializeField] WealthUI wealthInfoPrefeb;
    Dictionary<WEALTHTYPE, WealthUI> wealthInfoUIs = new();

    public override void Init()
    {
        UiType = UITYPE.TITLE;

        player = GameManager.Instance.player;

        foreach (var info in wealthInfos.list)
            wealthInfoUIs[info.wealthType] = Instantiate(wealthInfoPrefeb, wealth).Init(info);

        base.Init();
    }

    protected override void UpdateUI()
    {
        base.UpdateUI();

        hpBar.value = player.pStat.status[STATTYPE.CHP] / player.pStat.status[STATTYPE.HP];
        expBar.value = (float)player.pLevel.exp / player.pLevel.maxExp;
        level.text = player.pLevel.level.ToString();

        int playTime = (int)(Time.time - GameManager.Instance.gameStartTime);
        timeTxt.text = $"{playTime / 60:D2}:{playTime % 60:D2}";

        foreach (var wealth in GameManager.Instance.wealth.wealths)
        {
            if (wealthInfoUIs.TryGetValue(wealth.Key, out var ui))
            {
                ui.UpdateInfo(wealth.Value);
            }
        }
    }

    private void Update()
    {
        UpdateUI();
    }
}
