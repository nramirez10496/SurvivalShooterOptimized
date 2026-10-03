using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    [SerializeField] AudioClip deathClip;

    //new
    Rigidbody enemyRB;
    NavMeshAgent navMeshAgent;
    EnemyPool pool;
    bool inPool;
    AudioClip resetDeathClip;

    [SerializeField] EnemyData enemyData;
    int id_dead = Animator.StringToHash("Dead");
    Transform cTransform;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        //new
        enemyRB = GetComponent <Rigidbody> ();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();

        resetDeathClip = enemyAudio.clip;
        cTransform = transform;
    }
    void Update ()
    {
        if(isSinking)
        {
            cTransform.Translate (-Vector3.up * enemyData.sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger (id_dead);

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        if(isSinking)
        {
            return;
        }
        navMeshAgent.enabled = false;
        enemyRB.isKinematic = true;
        isSinking = true;
        ScoreManager.AddScore(enemyData.scoreValue);

        StartCoroutine(ReturnToPool());
    }

    private void OnEnable()
    {
        ResetEnemy();
    }

    private void OnDisable()
    {
        if (pool != null && !inPool)
        {
            inPool = true;
            pool.AddToQueue(this);
        }
    }

    public void SetPool(EnemyPool enemyPool)
    {
        pool = enemyPool;
    }

    public void SetInPool(bool value)
    {
        inPool = value;
    }

    void ResetEnemy()
    {
        currentHealth = enemyData.maxHealth;
        isDead = false;
        isSinking = false;

        capsuleCollider.isTrigger = false;
        enemyRB.isKinematic = false;
        navMeshAgent.enabled = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.ResetPath();

        anim.Rebind();
        anim.Update(0f);
        enemyAudio.Stop();
        enemyAudio.clip = resetDeathClip;

        hitParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}
