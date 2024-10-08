using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    // Components
    private NavMeshAgent agent;
    public Transform player;
    public LayerMask groundCheck, playerCheck;
    public Animator animator;
    public Transform gun;
    public Health playerHealth;

    // Patrolling Variables
    /*
     * walkPointRange = how large patrol radius is
     * wc = script on enemy's gun
     */
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public Weapon wc;

    // Attacking Variables
    /*
     * attackCooldown = how long it takes for enemy to attack again (lower = faster)
     * hitChance = how likely enemy is to deal damage with raycast weapon (higher = more accurate)
     * damage = how much damage raycast type enemies deal each shot
     */
    public float attackCooldown;
    public float hitChance;
    public float damage = 10;
    public bool canAttack = true;
    bool alreadyAttacked;

    // Range Variables
    /*
     * visionRange = how far away they can see player from
     * attackRange = how far away they start attacking player from
     */
    public float visionRange, attackRange;
    public bool playerInSight, playerInRange;

    // cost = how much enemy costs to be spawned by wave system
    public int cost;

    private void Awake()
    {
        // Set nav mesh agent and player transforms
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        playerHealth = player.gameObject.GetComponent<Health>();
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
        if (playerInRange)
        {
            wc.playerInRange = true;
        }
        else
        {
            wc.playerInRange = false;
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
        transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        gun.LookAt(player);

        // If enemy hasn't attacked yet, attack, then call ResetAttack to reset cooldown
        if (!alreadyAttacked)
        {
            //Debug.Log("I'm Attacking!");

            // If weapon type is raycast, shoot and use random chance to see if damage is taken
            if (wc.type == WeaponType.Raycast)
            {
                wc.Fire();
                float hitRoll = Random.Range(0, 10);
                if (hitRoll < hitChance)
                {
                    playerHealth.currentHealth -= damage;
                }
            }
            // If weapon type is projectile, launch projectile (explosion will handle damage dealing)
            else if (wc.type == WeaponType.Projectile)
            {
                wc.Launch();
            }

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
