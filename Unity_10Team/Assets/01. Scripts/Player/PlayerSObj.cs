using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDefaultData
{
    [field: Header("BaseData")]
    [field: SerializeField][field: Range(0f, 1000f)] public float baseHP { get; private set; } = 100f;
    [field: SerializeField][field: Range(0f, 100f)] public float baseAttak { get; private set; } = 10f;
    [field: SerializeField][field: Range(0f, 100f)] public float baseDefence { get; private set; } = 10f;
    [field: SerializeField][field: Range(0f, 1f)] public float baseAttackDelay { get; private set; } = 1f;
    [field: SerializeField][field: Range(0f, 25f)] public float baseSpeed { get; private set; } = 6f;
    [field: SerializeField][field: Range(0f, 25f)] public float baseRotationDamping { get; private set; } = 10f;

    [field: Header("IdleData")]

    [field: Header("MoveData")]
    [field: SerializeField][field: Range(0f, 2f)] public float moveSpeedModifier { get; private set; } = 1f;
}

[Serializable]
public class PlayerAttackData
{
    [field: Header("AttackData")]
    [field: SerializeField][field: Range(0f, 2f)] public float attackMoveSpeedModifier { get; private set; } = 0.5f;
}

[CreateAssetMenu(fileName = "Player")]
public class PlayerSObj : ScriptableObject
{
    [field: SerializeField] public PlayerDefaultData defaultData { get; private set; }
    [field: SerializeField] public PlayerAttackData attackData { get; private set; }
}
