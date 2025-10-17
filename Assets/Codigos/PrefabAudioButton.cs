using UnityEngine;
using UnityEngine.UI;

public class PrefabAudioButton : MonoBehaviour
{
    [Header("Índice del sonido que reproducirá este botón")]
    public int soundIndex = 0;

    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();

        if (myButton != null)
            myButton.onClick.AddListener(OnButtonPressed);
        else
            Debug.LogWarning($"No se encontró componente Button en {name}");
    }

    void OnButtonPressed()
    {
        if (AudioManagerGlobal.Instance != null)
            AudioManagerGlobal.Instance.PlaySound(soundIndex);
        else
            Debug.LogError("No se encontró AudioManagerGlobal en la escena.");
    }
}
