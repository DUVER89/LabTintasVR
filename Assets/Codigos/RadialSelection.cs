using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RadialSelection : MonoBehaviour
{
    [Header("Configuración")]
    public OVRInput.Button spawnButton = OVRInput.Button.PrimaryIndexTrigger;

    [Range(2, 10)]
    public int numberOfRadialPart = 6;
    public GameObject RadialPartPrefab;
    public Transform radialPartCanvas;
    public float angleBetweenParts = 10;
    public Transform handTransform; // Aquí arrastras RightHandAnchor
    public float menuDistance = 1.0f; // Distancia del menú frente a la mano

    [Header("Eventos")]
    public UnityEvent<int> onPartSelected;

    private List<GameObject> spawnedParts = new List<GameObject>();
    private int currentSelectedRadialPart = -1;

    void Update()
    {
        // Mostrar el menú al presionar el botón
        if (OVRInput.GetDown(spawnButton))
        {
            SpawnRadialParts();
        }

        // Mientras se mantiene presionado, actualizar selección
        if (OVRInput.Get(spawnButton))
        {
            GetSelectedRadialPart();
        }

        // Al soltar, confirmar selección y ocultar
        if (OVRInput.GetUp(spawnButton))
        {
            HideAndTriggerSelected();
        }
    }

    void HideAndTriggerSelected()
    {
        onPartSelected.Invoke(currentSelectedRadialPart);
        radialPartCanvas.gameObject.SetActive(false);
    }

    void GetSelectedRadialPart()
    {
        Vector3 centertoHand = handTransform.position - radialPartCanvas.position;
        Vector3 centertoHandProjected = Vector3.ProjectOnPlane(centertoHand, radialPartCanvas.forward);
        float angle = Vector3.SignedAngle(radialPartCanvas.up, centertoHandProjected, -radialPartCanvas.forward);

        if (angle < 0)
            angle += 360;

        currentSelectedRadialPart = Mathf.FloorToInt(angle * numberOfRadialPart / 360f);

        for (int i = 0; i < spawnedParts.Count; i++)
        {
            var img = spawnedParts[i].GetComponent<Image>();

            if (i == currentSelectedRadialPart)
            {
                img.color = Color.yellow;
                spawnedParts[i].transform.localScale = Vector3.one * 1.1f;
            }
            else
            {
                img.color = Color.white;
                spawnedParts[i].transform.localScale = Vector3.one;
            }
        }
    }

    void SpawnRadialParts()
    {
        radialPartCanvas.gameObject.SetActive(true);

        // Posición a 1 metro delante de la mano
        Vector3 forwardPosition = handTransform.position + handTransform.forward * menuDistance;
        radialPartCanvas.position = forwardPosition;

        // Orienta el menú hacia la cámara principal
        if (Camera.main != null)
        {
            radialPartCanvas.LookAt(Camera.main.transform);
            // Corrige la rotación para que quede plano hacia el usuario
            radialPartCanvas.Rotate(0, 180, 0);
        }

        // Limpia los sectores anteriores
        foreach (var item in spawnedParts)
        {
            Destroy(item);
        }
        spawnedParts.Clear();

        float stepAngle = 360f / numberOfRadialPart;
        float fill = (stepAngle - angleBetweenParts) / 360f;

        for (int i = 0; i < numberOfRadialPart; i++)
        {
            float angle = -i * stepAngle;
            GameObject part = Instantiate(RadialPartPrefab, radialPartCanvas);
            part.transform.localPosition = Vector3.zero;
            part.transform.localEulerAngles = new Vector3(0, 0, angle);

            var img = part.GetComponent<Image>();
            img.fillAmount = fill;

            spawnedParts.Add(part);
        }
    }
}
