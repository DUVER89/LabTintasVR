using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleMultipleWithDelay : MonoBehaviour
{
    [Header("Tiempo de espera antes de cambiar el estado")]
    [SerializeField] private float delaySeconds = 0.5f;

    private HashSet<GameObject> processingTargets = new HashSet<GameObject>();

    // Método público para usar desde el evento OnClick del botón
    public void ToggleUI(GameObject targetUI)
    {
        if (targetUI == null)
        {
            Debug.LogWarning("UIToggleMultipleWithDelay: No hay objeto UI asignado al botón.");
            return;
        }

        // Evita ejecutar varias veces sobre el mismo objeto
        if (processingTargets.Contains(targetUI))
            return;

        StartCoroutine(ToggleCoroutine(targetUI));
    }

    private IEnumerator ToggleCoroutine(GameObject targetUI)
    {
        processingTargets.Add(targetUI);

        // Espera el tiempo configurado antes de cambiar el estado
        yield return new WaitForSeconds(delaySeconds);

        bool newState = !targetUI.activeSelf;
        targetUI.SetActive(newState);

        Debug.Log($"UI '{targetUI.name}' ahora está {(newState ? "ACTIVA" : "INACTIVA")}");

        processingTargets.Remove(targetUI);
    }
}


