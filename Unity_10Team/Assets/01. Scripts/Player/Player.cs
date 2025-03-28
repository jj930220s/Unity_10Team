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
    [field: SerializeField] public PlayerStatus pStat { get; private set; }

    [field: SerializeField] public Bullet bulletPrefab { get; private set; }
    public ObjectPool<Bullet> bulletPool { get; private set; }
    public Transform shotPoint { get; private set; }

    private void Awake()
    {
        shotPoint = transform.Find("Character/ShotPoint");

        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();

        inputController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();

        pStat = new PlayerStatus(this);
        pStat.Init();

        stateMachine = new PlayerStateMachine(this);

        bulletPool = new ObjectPool<Bullet>(bulletPrefab, 100, transform);

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
