using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class EnemyBehavior : MonoBehaviour
{
    // Components
    private NavMeshAgent agent;
    public Transform player;
    public LayerMask groundCheck, playerCheck;
    public Animator animator;
    // Patrolling Variables
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking Variables
    public float attackCooldown;
    public bool canAttack = true;
    bool alreadyAttacked;

    // Range Variables
    public float visionRange, attackRange;
    public bool playerInSight, playerInRange;

    private void Awake()
    {
        // Set nav mesh agent and player transforms
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (canAttack)
        {
            // Check sight and attack range for player
            playerInSight = Physics.CheckSphere(transform.position, visionRange, playerCheck);
            playerInRange = Physics.CheckSphere(transform.position, attackRange, playerCheck);

            // Set state between these 3 depending on player position
            if (!playerInSight && !playerInRange) Patrolling();
            if (playerInSight && !playerInRange) Chasing();
            if (playerInRange) Attacking();
        }
    }

    // Patrol between random points
    private void Patrolling()
    {
        // If no walk point is set, create a new one
        if (!walkPointSet) CreateWalkPoint();

        // Use nav mesh agent to set destination to new point
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Check if point has been reached, if it has, find new point by setting walkPointSet to false
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    // Chase after player
    private void Chasing()
    {
        // Set destination to player's position
        agent.SetDestination(player.position);
    }

    // Attack player
    private void Attacking()
    {
        // Stop moving during attack
        agent.SetDestination(transform.position);
        // Look towards player
        transform.LookAt(player);

        // If enemy hasn't attacked yet, attack, then call ResetAttack to reset cooldown
        if (!alreadyAttacked)
        {
            /**
             * ATTACK CODE GOES HERE 
             **/
            Debug.Log("I'm Attacking!");

            // Reset cooldown using cooldown variable as timer
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    // Create new point to walk to during patrol
    private void CreateWalkPoint()
    {
        // Generate coordinates for new point
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        // Create point by adding random coords to current position
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check with raycast to ensure new point is valid
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundCheck))
            walkPointSet = true;
    }

    // Reset attack cooldown
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
