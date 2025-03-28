using UnityEngine;
using UnityEngine.AI; // NavMesh ���

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Monster monster;
    private NavMeshAgent agent; //�׺�Ž� ���

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("NevMesh ����");
            return;
        }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        monster = GetComponent<Monster>();

        if (monster != null)
        {
            agent.speed = monster.moveSpeed;            // �̵��ӵ�
            agent.stoppingDistance = monster.attackRange; // �����Ÿ�
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
