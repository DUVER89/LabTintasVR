using UnityEngine;

public class AudioManagerGlobal : MonoBehaviour
{
    public static AudioManagerGlobal Instance { get; private set; }

    [Header("Lista de sonidos disponibles")]
    public AudioClip[] sonidos;

    private AudioSource fuenteAudio;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        fuenteAudio = gameObject.AddComponent<AudioSource>();
    }

    public void ReproducirSonido(int indice)
    {
        if (indice < 0 || indice >= sonidos.Length)
        {
            Debug.LogWarning("Índice de sonido fuera de rango.");
            return;
        }

        // Si ya se está reproduciendo, reanudarlo
        if (fuenteAudio.clip == sonidos[indice] && fuenteAudio.isPlaying == false)
        {
            fuenteAudio.UnPause();
        }
        else
        {
            fuenteAudio.clip = sonidos[indice];
            fuenteAudio.Play();
        }
    }

    public void PausarSonido(int indice)
    {
        if (fuenteAudio.clip == sonidos[indice] && fuenteAudio.isPlaying)
        {
            fuenteAudio.Pause();
        }
    }

    public bool EstaReproduciendo(int indice)
    {
        return fuenteAudio.clip == sonidos[indice] && fuenteAudio.isPlaying;
    }
}
