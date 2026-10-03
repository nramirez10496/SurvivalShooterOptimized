using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    //new
    public EnemyPool enemyPool;


    void Start ()
    {
        InvokeRepeating (nameof(Spawn), spawnTime, spawnTime);
    }

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[spawnPointIndex];

        //requestt from pool
        enemyPool.SpawnEnemyAtLocation(spawnPoint.position,spawnPoint.rotation);
    }
}
