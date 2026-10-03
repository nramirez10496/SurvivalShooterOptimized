using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int maxHealth = 100;
    public float movementSpeed = 3f;
    public float sinkSpeed = 2.5f;
    public int attackDamage = 10;
    public float timeBetweenAttacks = 0.5f;
    public float attackRange = 2f;
    public int scoreValue = 10;
}
