// PlayerFootsteps.cs
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("Audio")]
    [Tooltip("AudioSource for footsteps—3D, Play On Awake off")]
    public AudioSource footAudioSource;
    [Tooltip("Define one entry per surface tag with its clips")]
    public SurfaceSound[] surfaceSounds;

    [Header("Movement")]
    [Tooltip("Distance (world units) per footstep sound")]
    public float stepDistance = 2f;

    private CharacterController cc;
    private float accumulated = 0f;

    void Awake() => cc = GetComponent<CharacterController>();

    void Update()
    {
        if (!cc.isGrounded) return;
        accumulated += cc.velocity.magnitude * Time.deltaTime;
        if (accumulated >= stepDistance)
        {
            PlayFootstep();
            accumulated = 0f;
        }
    }

    void PlayFootstep()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out var hit, 1.5f)) return;
        string tag = hit.collider.tag;
        foreach (var ss in surfaceSounds)
        {
            if (ss.tag == tag && ss.clips.Length > 0)
            {
                var clip = ss.clips[Random.Range(0, ss.clips.Length)];
                footAudioSource.PlayOneShot(clip);
                return;
            }
        }
    }

    [System.Serializable]
    public struct SurfaceSound
    {
        public string tag;        // e.g. "Metal", "Concrete", etc.
        public AudioClip[] clips; // footstep clips for that surface
    }
}
