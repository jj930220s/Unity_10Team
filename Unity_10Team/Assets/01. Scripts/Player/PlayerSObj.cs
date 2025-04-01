using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDefaultData
{
    [field: Header("BaseData")]
    [field: SerializeField][field: Range(0f, 1000f)] public float baseHP { get;  set; } = 100f;
    [field: SerializeField][field: Range(0f, 100f)] public float baseAttack { get;  set; } = 10f;
    [field: SerializeField][field: Range(0f, 100f)] public float baseDefence { get;  set; } = 10f;
    [field: SerializeField][field: Range(0.25f, 2f)] public float baseAttackDelay { get; set; } = 1f;
    [field: SerializeField][field: Range(0f, 25f)] public float baseSpeed { get; set; } = 6f;   
    [field: SerializeField][field: Range(0f, 25f)] public float baseRotationDamping { get; set; } = 10f;

    [field: Header("IdleData")]

    [field: Header("MoveData")]
    [field: SerializeField][field: Range(0f, 2f)] public float moveSpeedModifier { get; set; } = 1f;
}

[Serializable]
public class PlayerAttackData
{
    [field: Header("AttackData")]
    [field: SerializeField][field: Range(0f, 2f)] public float attackMoveSpeedModifier { get; set; } = 0.5f;
}

[CreateAssetMenu(fileName = "Player")]
public class PlayerSObj : ScriptableObject
{
    [field: SerializeField] public PlayerDefaultData defaultData { get; set; }
    [field: SerializeField] public PlayerAttackData attackData { get; set; }
}
