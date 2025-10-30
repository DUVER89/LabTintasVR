using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioClip clip;

    public void SetClip()
    {
        ListaDeAudios.instancia.CheckAudio(clip);
    }
}
