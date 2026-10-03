using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyPool : MonoBehaviour
{
    [SerializeField] EnemyHealth enemyPrefab;
    [SerializeField] int enemyAmount;

    Queue<EnemyHealth>remainingEnemies = new Queue<EnemyHealth>();

    void Awake()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            //create enemy 
            EnemyHealth enemy = Instantiate(enemyPrefab);

            //set snemy to pool 
            enemy.SetPool(this);
            //inactive till needed 
            enemy.gameObject.SetActive(false);
        }
    }

    public void SpawnEnemyAtLocation(Vector3 location, Quaternion rotation)
    {        
        //avoid spawn if empty
        if (remainingEnemies.Count==0)
        {
            return;
        }

        //get enemy drom queue 
        EnemyHealth current = remainingEnemies.Dequeue();

        //position
        current.transform.SetPositionAndRotation(location, rotation);

        //no longer in pool
        current.SetInPool(false);

        //acivate enemy 
        current.gameObject.SetActive(true);
    }

    public void AddToQueue(EnemyHealth enemy)
    {
        remainingEnemies.Enqueue(enemy);
    }
}
