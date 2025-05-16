using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip intenseClip;    // assign in Inspector
    [Range(0, 1)] public float volume = 0.7f;

    private static MusicManager _instance;
    private AudioSource _src;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        _src = gameObject.AddComponent<AudioSource>();
        _src.clip = intenseClip;
        _src.loop = true;
        _src.playOnAwake = false;
        _src.spatialBlend = 0f;     // ensure it’s a 2D (non-3D) sound
        _src.volume = volume;
        _src.Play();
    }
}
