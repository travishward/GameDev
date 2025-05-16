using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyController : MonoBehaviour
{
    [Header("Stats")]
    public int health = 50;
    public int xpReward = 20;
    public float speed = 3.5f;

    [Header("Attack Settings")]
    public int attackDamage = 10;       // Damage per hit
    public float attackRange = 2f;      // Distance at which the enemy can attack
    public float attackCooldown = 2f;   // Time between attacks
    private float attackTimer = 0f;     // Counts down each frame

    [Header("NavMesh Settings")]
    public float warpSearchRadius = 5f; // Used to warp onto the NavMesh if needed

    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        // Locate the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }

        // Attempt to warp onto the NavMesh
        TryWarpToNavMesh();
    }

    void Update()
    {
        // Move towards the player if valid
        if (player != null && agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }

        // Attack logic: if close enough and cooldown is over, attack
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange && attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown;
            }
        }

        // Reduce the attack timer
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void TryWarpToNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, warpSearchRadius, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning($"[SimpleEnemyController] Could not find NavMesh within {warpSearchRadius} units of {transform.position}.");
        }
    }

    void AttackPlayer()
    {
        // Get the player's health script (assuming a PlayerHealth script exists on the Player)
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        Debug.Log($"Enemy attacked player for {attackDamage} damage.");
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Award XP to the player
        if (player != null)
        {
            PlayerXP playerXP = player.GetComponent<PlayerXP>();
            if (playerXP != null)
            {
                playerXP.AddXP(xpReward);
            }
        }
        Destroy(gameObject);
    }
}
