using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private float attackRange = 1f;
    NavMeshAgent navMeshAgent;
    Animator animator;
    
     void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var Player = FindObjectOfType<PlayerMovement>();
        // Only get player's position if nav mesh is enabled
        if (navMeshAgent.enabled == true)
        {
            navMeshAgent.SetDestination(Player.transform.position);
        }


        // If distance between current object (zombie) and player is less than attack range, play attack animation
        if (Vector3.Distance(transform.position, Player.transform.position) < attackRange) 
        {
            Attack();
        }
    }
    // Play attack animation, then disabled nav mesh so the zombie stops moving
    void Attack()
    {
        animator.SetTrigger("Attack");
        navMeshAgent.enabled = false;
    }
    
    // Animation callback, using animation system events
    public void AttackComplete()
    {
        navMeshAgent.enabled = true;
    }

    // Animation callback, using animation system events
    public void AttackHit()
    {
        Debug.Log("Killed Player");
    }
}
