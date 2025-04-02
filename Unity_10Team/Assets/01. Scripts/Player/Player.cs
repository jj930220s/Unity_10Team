using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSObj data { get; private set; }
    [field: SerializeField] public EnforceData enforceData { get; private set; }

    public List<IEnforce> enforceList { get; private set; } = new List<IEnforce>();

    [field:Header("Animations")]
    [field:SerializeField] public PlayerAnimationData animationData { get; private set; }

    public Animator animator { get; private set; }
    public PlayerController inputController { get; private set; }
    public CharacterController characterController { get; private set; }
    private PlayerStateMachine stateMachine;
    public Transform mainCameraTransform { get; set; }

    [field: SerializeField] public PlayerLevel pLevel { get; private set; }
    [field: SerializeField] public PlayerStatus pStat { get; private set; }

    [field: SerializeField] public Bullet bulletPrefab { get; private set; }
    public ObjectPool<Bullet> bulletPool { get; private set; }
    public Transform shotPoint { get; private set; }

    private GameDataManager dataManager; //게임데이터매니저

    private SelectedDrons droneData;

    public Transform[] dronePoint;
    private void Awake()
    {
        foreach (IEnforce enforce in enforceData.EnforceStatusList)
        {
            enforceList.Add(enforce);
        }

        shotPoint = transform.Find("Character/ShotPoint");

        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();

        inputController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();

        pLevel = new PlayerLevel(1);
        pStat = new PlayerStatus(data);
        pStat.Init();

        stateMachine = new PlayerStateMachine(this);

        GameObject bulletPoolParent = new GameObject("BulletPool");
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, 100, bulletPoolParent.transform);

        stateMachine.ChangeState(stateMachine.idleState);

        dataManager = FindObjectOfType<GameDataManager>();
        //LoadPlayerData(); //저장된 데이터 불러오기

    }

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        stateMachine.StateHandleInput();
        stateMachine.StateUpdate();

        // 테스트용
        if (Input.GetKeyDown(KeyCode.F1))
        {
            pStat.TakeDamage(100);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.StatePhysicsUpdate();
    }
}
