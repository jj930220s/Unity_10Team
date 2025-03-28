using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerSObj data { get; private set; }

    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData animationData { get; private set; }

    public Animator animator { get; private set; }
    public PlayerController inputController { get; private set; }
    public CharacterController characterController { get; private set; }
    private PlayerStateMachine stateMachine;
    public Transform mainCameraTransform { get; set; }

    [field: SerializeField]public PlayerStatus pStat { get; private set; }

    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();
        inputController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        stateMachine = new PlayerStateMachine(this);
        pStat = new PlayerStatus(this);

        stateMachine.ChangeState(stateMachine.idleState);
    }

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        stateMachine.StateHandleInput();
        stateMachine.StateUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.StatePhysicsUpdate();
    }
}
