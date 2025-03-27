using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDefaultData
{
    [field:SerializeField][field: Range(0f, 25f)] public float baseSpeed { get; private set; } = 6f;
    [field:SerializeField][field: Range(0f, 25f)] public float baseRotationDamping { get; private set; } = 10f;

    [field:Header("IdleData")]

    [field:Header("MoveData")]
    [field:SerializeField][field: Range(0f, 2f)] public float moveSpeedModifier { get; private set; } = 1f;

    [field:Header("AttackData")]
    [field:SerializeField][field: Range(0f, 2f)] public float attackSpeedModifier { get; private set; } = 1.5f;
}

[CreateAssetMenu(fileName = "Player")]
public class PlayerSObj : ScriptableObject
{
    [field:SerializeField] public PlayerDefaultData defaultData { get; private set; }
}
