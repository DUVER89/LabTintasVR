using UnityEngine;
using UnityEngine.UI;

public class PrefabAudioButton : MonoBehaviour
{
    [Header("�ndice del sonido que reproducir� este bot�n")]
    public int soundIndex = 0;

    private Button myButton;

    void Start()
    {
        myButton = GetComponent<Button>();

        if (myButton != null)
            myButton.onClick.AddListener(OnButtonPressed);
        else
            Debug.LogWarning($"No se encontr� componente Button en {name}");
    }

    void OnButtonPressed()
    {
        if (AudioManagerGlobal.Instance != null)
            AudioManagerGlobal.Instance.PlaySound(soundIndex);
        else
            Debug.LogError("No se encontr� AudioManagerGlobal en la escena.");
    }
}
