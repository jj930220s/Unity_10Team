using UnityEngine;
using UnityEngine.AI; // NavMesh 사용

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Monster monster;
    private NavMeshAgent agent; //네브매쉬 사용

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NevMesh 없음");
            return;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        monster = GetComponent<Monster>();

        if (monster != null)
        {
            agent.speed = monster.moveSpeed;            // 이동속도
            agent.stoppingDistance = monster.attackRange; // 사정거리
            monster.target = player;
        }
    }

    void Update()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
