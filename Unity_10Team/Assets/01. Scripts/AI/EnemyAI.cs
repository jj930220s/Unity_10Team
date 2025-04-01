using UnityEngine;
using UnityEngine.AI; // NavMesh 사용
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Monster monster;
    public NavMeshAgent agent; //네브매쉬 사용
    private Animator animator;

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

        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator 없음");
        }

        StartCoroutine(UpdateAI());
    }

    IEnumerator UpdateAI()
    {
        while (!monster.isDead)
        {
            if (agent != null && player != null && monster.target != null)
            {
                agent.SetDestination(monster.target.position);
                if (monster.isDead) break;

                Vector3 direction = (monster.target.position - transform.position).normalized;
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

            yield return new WaitForSeconds(0.1f);
        }
    }
}
