using System.Collections;
using System.Collections.Generic;
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
        pStat = new PlayerStatus(this);
        pStat.Init();

        stateMachine = new PlayerStateMachine(this);

        bulletPool = new ObjectPool<Bullet>(bulletPrefab, 100);

        stateMachine.ChangeState(stateMachine.idleState);

        dataManager = FindObjectOfType<GameDataManager>();
        LoadPlayerData(); //저장된 데이터 불러오기

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

    private void OnApplicationQuit()
    {
        SavePlayerData(); //종료 시 데이터 자동저장
    }

    public void SavePlayerData()
    {
        if ( dataManager != null)
        {
            dataManager.SavePlayerData(data);
        }
    }

    public void LoadPlayerData()
    {
        if ( dataManager != null)
        {

            PlayerSaveData saveData = dataManager.LoadPlayerData();
            if ( saveData != null )
            {
                //data.defaultData = new PlayerDefaultData()
                {
                    //베이스 데이터들
                }

                
            }
           
        }
    }
}
