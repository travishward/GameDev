using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damage = 20;
    public float range = 100f;
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;  // Drag your MuzzleFlash here in the Inspector
    public GameObject impactEffect;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Play muzzle flash effect if assigned.
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log("Hit: " + hit.transform.name);

            // Check if the hit object has an enemy script and apply damage.
            SimpleEnemyController enemy = hit.transform.GetComponent<SimpleEnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            SimpleBossController boss = hit.transform.GetComponent<SimpleBossController>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
