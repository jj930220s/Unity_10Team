using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VOLTYPE
{
    MASTER,
    BGM,
    SFX
}

[Serializable]
public class Volume
{
    public VOLTYPE type;
    public float arrange;
}

[Serializable]
public class Volumes
{
    public Volume[] list;
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] Volumes baseVolumes;
    public Dictionary<VOLTYPE, float> volumes = new();

    [SerializeField] string volumeSavePath = "volumes.json";

    private void Awake()
    {
        baseVolumes = DataSave<Volumes>.LoadOrBase(baseVolumes, volumeSavePath);

        foreach (var baseVol in baseVolumes.list)
            volumes[baseVol.type] = baseVol.arrange;
    }

    private void OnDestroy()
    {
        foreach (var baseVol in baseVolumes.list)
            baseVol.arrange = volumes[baseVol.type];

        DataSave<Volumes>.SaveData(baseVolumes, volumeSavePath);
    }
}
