using System;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public static event Action<Zombie> ZombieDied;

    [SerializeField] float attackRange = 1f;
    [SerializeField] int health = 2;
    [SerializeField] float attackDelay = 0.25f;

    int currentHealth;

    NavMeshAgent navMeshAgent;
    Animator animator;

    float nextAttackTime;

    bool Alive => currentHealth > 0;

     void Awake() 
    {
        currentHealth = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        var bullet = collision.collider.GetComponent<Bullet>();
        if (bullet != null)
        {
            currentHealth = currentHealth -= 1;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    // Get rid of collider if zombie dies, disabled nav mesh agent, play death animation and destroy the object after 5 seconds
    void Die()
    {
        GetComponent<Collider>().enabled = false;
        navMeshAgent.enabled = false;
        animator.SetTrigger("Died");
        ZombieDied?.Invoke(this);
        GameManager.zombiesLeftInRound -= 1;
        Destroy(gameObject, 5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            return;
        }

        var Player = FindObjectOfType<PlayerMovement>();
        // Only get player's position if nav mesh is enabled
        if (navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(Player.transform.position);
        }


        // If distance between current object (zombie) and player is less than attack range, then attack
        if (Vector3.Distance(transform.position, Player.transform.position) < attackRange) 
        {
            if (ReadyToAttack())
            {
                Attack();
            }

        }
    }

    bool ReadyToAttack() => Time.time >= nextAttackTime;
    bool InAttackRange => Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position) < attackRange;
        
    // Play attack animation, then disabled nav mesh so the zombie stops moving
    void Attack()
    {
        nextAttackTime = Time.time + attackDelay;
        animator.SetTrigger("Attack");
        navMeshAgent.enabled = false;
    }

    // Animation callback, using animation system events
    public void EnableMovement()
    {
        if(Alive) {
            navMeshAgent.enabled = true;
        }
    }

    // Animation callback, using animation system events
    public void AttackHit()
    {
        if (InAttackRange)
        {
            Debug.Log("Killed Player");
        }
        
    }
}
