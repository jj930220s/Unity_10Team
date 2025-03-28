using UnityEngine;
using UnityEngine.AI; // NavMesh ���

public class EnemyAI : MonoBehaviour
{
    public Transform player;
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
    }

    void Update()
    {
        if (agent != null && player != null)
        {
            agent.SetDestination(player.position);
        }
    }
}
