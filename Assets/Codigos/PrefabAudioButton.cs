using UnityEngine;
using UnityEngine.UI;

public class PrefabAudioButton : MonoBehaviour
{
    [Header("Índice del sonido que reproducirá o pausará este botón")]
    public int soundIndex = 0;

    private Button miBoton;

    void Start()
    {
        miBoton = GetComponent<Button>();

        if (miBoton != null)
            miBoton.onClick.AddListener(AlPresionarBoton);
        else
            Debug.LogWarning($"No se encontró componente Button en {name}");
    }

    void AlPresionarBoton()
    {
        if (AudioManagerGlobal.Instance == null)
        {
            Debug.LogError("No se encontró AudioManagerGlobal en la escena.");
            return;
        }

        // Si el sonido ya se está reproduciendo → pausar
        if (AudioManagerGlobal.Instance.EstaReproduciendo(soundIndex))
        {
            AudioManagerGlobal.Instance.PausarSonido(soundIndex);
        }
        else
        {
            // Si no se está reproduciendo → reproducir
            AudioManagerGlobal.Instance.ReproducirSonido(soundIndex);
        }
    }
}
