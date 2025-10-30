using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarEscenaBoton : MonoBehaviour
{
    [Header("Nombre de la escena a recargar (déjalo vacío para recargar la actual)")]
    [SerializeField] private string nombreEscena = "";

    // Este método se puede asignar directamente al botón desde el inspector
    public void ReiniciarEscena()
    {
        if (!string.IsNullOrEmpty(nombreEscena))
        {
            SceneManager.LoadScene(nombreEscena);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
