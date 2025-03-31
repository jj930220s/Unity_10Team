using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DRONTYPE
{
    DEAL,
    HEAL
}

[CreateAssetMenu(fileName = "Dron", menuName = "Data/Dron")]
public class DronData : ScriptableObject
{
    public string dronName;
    public BaseDroneController dronPrefeb;
    public DRONTYPE type;
    public Sprite icon;
}
