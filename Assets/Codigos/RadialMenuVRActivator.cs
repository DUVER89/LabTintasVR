using UnityEngine;

public class ToggleCanvasWithButtonB : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Canvas que aparecer�/desaparecer� al presionar B")]
    public Canvas targetCanvas;

    [Tooltip("Mano desde la cual se generar� el Canvas (ej: RightHandAnchor)")]
    public Transform handAnchor;

    [Header("Configuraci�n de posici�n")]
    [Tooltip("Distancia frente a la mano donde aparecer� el Canvas")]
    public float distancia = 0.8f;

    [Tooltip("Altura adicional sobre la mano")]
    public float alturaOffset = 0.0f;

    [Tooltip("Si est� activo, el Canvas mirar� hacia la c�mara principal")]
    public bool mirarHaciaUsuario = true;

    // Estado interno
    private bool menuVisible = false;

    void Start()
    {
        if (targetCanvas != null)
            targetCanvas.enabled = false; // Comienza oculto
    }

    void Update()
    {
        // Detecta si el bot�n B fue presionado
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            menuVisible = !menuVisible; // Alternar estado
            ToggleCanvas(menuVisible);
        }
    }

    void ToggleCanvas(bool mostrar)
    {
        if (targetCanvas == null || handAnchor == null)
            return;

        targetCanvas.enabled = mostrar;

        if (mostrar)
        {
            // Posicionar el Canvas frente a la mano
            Vector3 pos = handAnchor.position + handAnchor.forward * distancia;
            pos.y += alturaOffset;
            targetCanvas.transform.position = pos;

            // Rotar el Canvas
            if (mirarHaciaUsuario && Camera.main != null)
            {
                targetCanvas.transform.LookAt(Camera.main.transform);
                targetCanvas.transform.Rotate(0, 180, 0); // Corregir orientaci�n
            }
            else
            {
                targetCanvas.transform.rotation = Quaternion.LookRotation(handAnchor.forward);
            }
        }
    }
}

