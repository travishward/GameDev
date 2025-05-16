using UnityEngine;
using UnityEngine.AI;

public class SimpleBossController : MonoBehaviour
{
    public int health = 500;          // Boss's health
    public int xpReward = 200;        // XP awarded upon defeat
    public float speed = 2.5f;        // Boss's movement speed
    public float attackRange = 3f;    // Range within which the boss will attack
    public float attackCooldown = 3f; // Time between attacks
    public int attackDamage = 25;     // Damage per attack

    private Transform player;
    private NavMeshAgent agent;
    public float warpSearchRadius = 5f; // Radius to find a valid NavMesh position
    private float attackTimer = 0f;

    void Start()
    {
        // Locate the player by tag (ensure your player is tagged "Player")
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Get the NavMeshAgent component and set its speed
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }

        // Force the boss onto the nearest NavMesh position
        TryWarpToNavMesh();
    }

    void Update()
    {
        if (player != null && agent != null && agent.isOnNavMesh)
        {
            // Continuously update the destination to the player's position
            agent.SetDestination(player.position);
        }

        // If the player is within attack range, attack (if cooldown has expired)
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange && attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackCooldown;
            }
        }

        // Reduce the attack cooldown timer
        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void TryWarpToNavMesh()
    {
        // Attempt to find a valid position on the NavMesh near the current position
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, warpSearchRadius, NavMesh.AllAreas))
        {
            // Warp the agent to that position to ensure it's on the NavMesh
            agent.Warp(hit.position);
        }
        else
        {
            Debug.LogWarning("[SimpleBossController] Could not find NavMesh within " + warpSearchRadius + " units of " + transform.position);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void AttackPlayer()
    {
        // Since we don't have a PlayerHealth script, simply log the attack.
        Debug.Log("Boss attacked the player for " + attackDamage + " damage.");
    }

    void Die()
    {
        // Award XP to the player using the PlayerXP script, if available.
        PlayerXP playerXP = player.GetComponent<PlayerXP>();
        if (playerXP != null)
        {
            playerXP.AddXP(xpReward);
        }
        Destroy(gameObject);
    }
}
