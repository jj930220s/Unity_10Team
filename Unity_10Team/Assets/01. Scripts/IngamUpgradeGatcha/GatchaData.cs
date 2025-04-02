using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gatcha", menuName = "Data/GatchaData")]
public class GatchaData : ScriptableObject
{
    public string upgaradeName;
    public Sprite icon;

    public Status[] upgradeStats;
}
