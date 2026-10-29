using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Fuentes de audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip crystalSfx;
    [SerializeField] private AudioClip deathSfx;
    [SerializeField] private AudioClip winSfx;

    [Header("Volumen")]
    [Range(0f, 1f)] [SerializeField] private float musicVolume = 0.3f;
    [Range(0f, 1f)] [SerializeField] private float sfxVolume = 0.8f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }

    public void PlayCrystal() { PlaySfx(crystalSfx); }

    public void PlayDeath()
    {
        StopMusic();
        PlaySfx(deathSfx);
    }

    public void PlayWin()
    {
        StopMusic();
        PlaySfx(winSfx);
    }

    private void StopMusic()
    {
        if (musicSource != null) musicSource.Stop();
    }

    private void PlaySfx(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
            sfxSource.PlayOneShot(clip, sfxVolume);
    }
}