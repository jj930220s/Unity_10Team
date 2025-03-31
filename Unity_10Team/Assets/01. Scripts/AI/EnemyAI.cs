using UnityEngine;
using UnityEngine.AI; // NavMesh ���

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Monster monster;
    public NavMeshAgent agent; //�׺�Ž� ���
    private Animator animator;

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

        animator = GetComponent<Animator>(); 

        if (animator == null)
        {
            Debug.LogError("Animator ����");
        }

    }

    void Update()
    {
        if (agent != null && player != null && !monster.isDead)
        {
            agent.SetDestination(player.position);

            Vector3 direction = (player.position - transform.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * monster.moveSpeed);
            }

            bool isMoving = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
