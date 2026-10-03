using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    NavMeshAgent navMeshAgent;

    //new
    float elapsed = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        playerHealth = player.GetComponent < PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemyData.movementSpeed;
    }

    void Update ()
    {
        //dont move if dead
        if (enemyHealth.currentHealth <= 0 || playerHealth.currentHealth<=0)
        {
            if (navMeshAgent.enabled)
            {
                navMeshAgent.isStopped = true;
            }
            return;
        }

        //dont move if disabled
        if(!navMeshAgent.enabled)
        {
            return;
        }

        elapsed += Time.deltaTime;

        //update periodically
        if(elapsed>0.2f)
        {
            navMeshAgent.SetDestination(player.position);
            elapsed = 0f;
        }
    }
}
