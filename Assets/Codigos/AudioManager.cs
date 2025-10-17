using UnityEngine;

public class AudioManagerGlobal : MonoBehaviour
{
    public static AudioManagerGlobal Instance; // Patrón Singleton
    [Header("Lista de sonidos disponibles")]
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    void Awake()
    {
        // Asegurar que solo haya un AudioManager en la escena
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Opcional, si cambias de escena

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.spatialBlend = 0f; // 0 = 2D
    }

    public void PlaySound(int index)
    {
        if (index < 0 || index >= audioClips.Length)
        {
            Debug.LogWarning($"Índice {index} fuera de rango. Total clips: {audioClips.Length}");
            return;
        }

        AudioClip clip = audioClips[index];
        if (clip == null)
        {
            Debug.LogWarning($"El clip {index} no está asignado en AudioManagerGlobal.");
            return;
        }

        if (audioSource.isPlaying)
            audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();

        Debug.Log($"🎵 Reproduciendo clip: {clip.name}");
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
