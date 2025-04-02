using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

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

    private GameDataManager dataManager; //���ӵ����͸Ŵ���

    public PlayableDirector cDDirector;

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


    }

    private void Start()
    {
        stateMachine = new PlayerStateMachine(this);

        GameObject bulletPoolParent = new GameObject("BulletPool");
        bulletPool = new ObjectPool<Bullet>(bulletPrefab, 100, bulletPoolParent.transform);

        stateMachine.ChangeState(stateMachine.idleState);

        dataManager = FindObjectOfType<GameDataManager>();
        LoadDroneData(); //����� ������ �ҷ�����

        if (cDDirector == null)
        {
            cDDirector = GameObject.Find("Clear&Dead Timeline").GetComponent<PlayableDirector>();
        }
        mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        stateMachine.StateHandleInput();
        stateMachine.StateUpdate();

        // 테스트용
        if (Input.GetKeyDown(KeyCode.F1))
        {
            pStat.TakeDamage(1000f);
            //stateMachine.ChangeState(stateMachine.clearState);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            pStat.Heal(10f);
            stateMachine.ChangeState(stateMachine.idleState);
        }
    }

    private void FixedUpdate()
    {
        stateMachine.StatePhysicsUpdate();
    }

    void LoadDroneData()
    {
        // ��� ����
        LoadDroneData loadDrone = new LoadDroneData();
        loadDrone.LoadDrone();
        droneData = loadDrone.GetSelectedDrons();

        // ������ ��� ������
        if (droneData != null)
        {
            for (int i = 0; i < droneData.list.Count(); i++)
            {
                if (droneData.list[i].idx == -1)
                    continue;
                GameObject drone = Instantiate(droneData.list[i].data.dronPrefeb, dronePoint[i]).gameObject;
                drone.transform.localPosition = Vector3.zero;
            }
        }
    }
}
