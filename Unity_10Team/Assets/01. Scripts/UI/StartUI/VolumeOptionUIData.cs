using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VolOption", menuName = "Data/VolumeOption")]
public class VolumeOptionUIData : OptionUIData
{
    public VOLTYPE type;
    public override float initValue { get => SoundManager.Instance.volumes[type] * 100; }

    public void OnValueChange(float changed)
    {
        SoundManager.Instance.ChangeSound(type, changed);
    }
}
