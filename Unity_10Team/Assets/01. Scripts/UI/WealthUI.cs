using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WealthUI : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI amountTxt;

    public void UpdateInfo(int amount)
    {
        amountTxt.text = amount.ToString();
    }
}
