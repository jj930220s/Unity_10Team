using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string defaultParameterName = "@Default";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string moveParameterName = "Move";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string shotParameterName = "Shot";
    [SerializeField] private string verticalParameterName = "Vertical";
    [SerializeField] private string horizontalParameterName = "Horizontal";

    [SerializeField] private string deadParameterName = "Dead";

    [SerializeField] private string cleatParameterName = "Clear";

    public int defaultParameterHash { get; private set; }
    public int idleParameterHash { get; private set; }
    public int moveParameterHash { get; private set; }

    public int attackParameterHash { get; private set; }
    public int shotParameterHash { get; private set; }
    public int verticalParameterHash { get; private set; }
    public int horizontalParameterHash { get; private set; }

    public int deadParameterHash { get; private set; }

    public int clearParameterHash { get; private set; }

    public void Initialize()
    {
        defaultParameterHash = Animator.StringToHash(defaultParameterName);
        idleParameterHash = Animator.StringToHash(idleParameterName);
        moveParameterHash = Animator.StringToHash(moveParameterName);

        attackParameterHash = Animator.StringToHash(attackParameterName);
        shotParameterHash = Animator.StringToHash(shotParameterName);
        verticalParameterHash = Animator.StringToHash(verticalParameterName);
        horizontalParameterHash = Animator.StringToHash(horizontalParameterName);

        deadParameterHash = Animator.StringToHash(deadParameterName);

        clearParameterHash = Animator.StringToHash(cleatParameterName);
    }
}
