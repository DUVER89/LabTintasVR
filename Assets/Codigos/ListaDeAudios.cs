using System.Collections;
using UnityEngine;

public class ListaDeAudios : MonoBehaviour
{
    public static ListaDeAudios instancia;

    [SerializeField] private AudioSource fuenteDeAudio;

    private bool enCooldown = false;
    private float tiempoCooldown = 0.5f;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        if (fuenteDeAudio == null)
            fuenteDeAudio = GetComponent<AudioSource>();
    }

    public void CheckAudio(AudioClip clip)
    {
        if (enCooldown) return;

        StartCoroutine(Cooldown());

        if (fuenteDeAudio.isPlaying)
            fuenteDeAudio.Pause();
        else
        {
            fuenteDeAudio.clip = clip;
            fuenteDeAudio.Play();
        }
    }
    private IEnumerator Cooldown()
    {
        enCooldown = true;
        yield return new WaitForSeconds(tiempoCooldown);
        enCooldown = false;
    }
    public void DetenerAudio()
    {
        fuenteDeAudio.Stop();
    }
    public void DetenerConRetraso() { StartCoroutine(CorDetenerAudioConRetraso()); }

    private IEnumerator CorDetenerAudioConRetraso()
    {
        yield return new WaitForSeconds(2f);
        fuenteDeAudio.Stop();
    }
}

