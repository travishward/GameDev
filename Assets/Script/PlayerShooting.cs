using System.Linq;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Damage & Range")]
    public int damage = 20;
    public float range = 100f;

    [Header("References")]
    public Camera fpsCamera;
    public Transform muzzlePoint;
    public GameObject muzzleFlashPrefab;
    public GameObject impactEffect;

    [Header("Timing")]
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [Header("Audio")]
    public AudioSource gunAudioSource;
    public AudioClip shootClip;

    [Header("Recoil")]
    [Tooltip("Your MouseLook script (on the camera)")]
    public MouseLook mouseLook;
    [Tooltip("Degrees the camera kicks up per shot")]
    public float verticalRecoilAngle = 2f;
    [Tooltip("Max random horizontal kick (±)")]
    public float horizontalRecoilAngle = 1f;

    void Awake()
    {
        // Auto-assign MouseLook from the camera if you forgot
        if (mouseLook == null && fpsCamera != null)
            mouseLook = fpsCamera.GetComponent<MouseLook>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // 1) Play gunshot SFX
        if (gunAudioSource != null && shootClip != null)
            gunAudioSource.PlayOneShot(shootClip);

        // 2) Add recoil to the camera via MouseLook
        if (mouseLook != null)
        {
            float hr = Random.Range(-horizontalRecoilAngle, horizontalRecoilAngle);
            mouseLook.AddRecoil(verticalRecoilAngle, hr);
        }
        else
        {
            Debug.LogWarning("MouseLook reference missing on PlayerShooting!");
        }

        // … your existing muzzle‐flash & raycast code follows …
        if (muzzleFlashPrefab != null && muzzlePoint != null)
        {
            var flashGO = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
            var systems = flashGO.GetComponentsInChildren<ParticleSystem>();
            if (systems.Length > 0)
            {
                foreach (var ps in systems) ps.Play();
                float maxLife = systems.Select(ps => ps.main.duration + ps.main.startLifetime.constantMax).Max();
                Destroy(flashGO, maxLife + 0.1f);
            }
        }

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out var hit, range))
        {
            var enemy = hit.transform.GetComponent<SimpleEnemyController>();
            if (enemy != null) enemy.TakeDamage(damage);
            var boss = hit.transform.GetComponent<SimpleBossController>();
            if (boss != null) boss.TakeDamage(damage);
            if (impactEffect != null)
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
