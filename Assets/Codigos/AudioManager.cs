using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] factorySounds = new AudioClip[10]; // Tus 10 sonidos
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false; // No repetir sonidos
    }

    public void PlaySound(int index)
    {
        if (index < 0 || index >= factorySounds.Length)
        {
            Debug.LogWarning("Índice de sonido fuera de rango.");
            return;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Detiene el sonido actual
        }

        audioSource.clip = factorySounds[index];
        audioSource.Play(); // Reproduce el nuevo sonido
    }
}
