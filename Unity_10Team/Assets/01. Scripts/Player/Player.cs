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

    private GameDataManager dataManager; //게임데이터매니저

    private void Awake()
    {
        animationData.Initialize();
        animator = GetComponentInChildren<Animator>();

        inputController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();

        pStat = new PlayerStatus(this);
        pStat.Init();

        stateMachine = new PlayerStateMachine(this);
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
