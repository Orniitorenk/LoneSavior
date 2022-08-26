using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySpawningEnemies : MonoBehaviour, IDamageable
{
    [Header("Agent and Player variables")]
    public NavMeshAgent Agent;
    public Transform player;
    public float enemyHealth;
    public float enemyMaxHealth;
    public Image healthBar;
    public GameObject damageText;
    public TankHealth tankHealth;

    public LayerMask whatIsGround, whatIsPlayer;

    [Header("Other Variables")]
    private RaycastHit Hit;
    private LayerMask Mask;
    public bool isDead = false;
    public float onY;

    Vector3 target;

    //Attacking
    [Header("Attacking Variables")]
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject tankBullet;
    public GameObject tankTop;
    public Transform shootPoint;

    //States
    [Header("States")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Death Effect Variables")]
    public Transform explosionPoint;
    public GameObject fireParticleEffect, tankTopExplosion, explosionEffect;

    private void Awake()
    {
        player = GameObject.Find("PlayerTank").transform;
        Agent = GetComponent<NavMeshAgent>();
        tankHealth = GameObject.Find("PlayerTank").GetComponent<TankHealth>();
    }

    private void Start()
    {
        enemyHealth = enemyMaxHealth;
        ChasePlayer();
    }

    private void FixedUpdate()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            if (!GameManager.Instance.isGameOver)
            {
                AttackPlayer();
                return;
            }
            
            
        }

        if (enemyHealth <= 0)
        {
            isDead = true;
            Death();
        }
        
    }

    public void TakeDamage(float Damage)
    {
        enemyHealth -= Damage;
        healthBar.fillAmount = enemyHealth / enemyMaxHealth;
        DamageIndicator indicator = Instantiate(damageText, transform.position + new Vector3(0f,2f,0f), Quaternion.identity).GetComponent<DamageIndicator>();
        if (!isDead)
        {
            indicator.SetDamageText(Damage);
        }      
        if(enemyHealth <= 0 && !isDead)
        {
            Death();
            isDead = true;
        }
    }

    private void ChasePlayer()
    {
        Agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (!isDead)
        {          
            Agent.SetDestination(player.position);

            tankTop.transform.LookAt(player.position + new Vector3(0f, onY, 0f));


            if (!alreadyAttacked)
            {
                Rigidbody rb = Instantiate(tankBullet, shootPoint.position, tankTop.transform.rotation * Quaternion.Euler(-90f, 0f, 0f)).GetComponent<Rigidbody>();

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }
        
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Death()
    {
        Agent.enabled = false;
        Instantiate(explosionEffect, explosionPoint.transform.position, Quaternion.Euler(-90f,0f,0f));
        Instantiate(fireParticleEffect, explosionPoint.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Instantiate(tankTopExplosion, explosionPoint.transform.position, Quaternion.identity);
              

        Destroy(tankTop);
        GetComponent<EnemySpawningEnemies>().enabled = false;
    }

    
}
