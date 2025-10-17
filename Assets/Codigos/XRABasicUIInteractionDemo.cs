using System.Collections.Generic;
using UnityEngine;

public class XRABasicUIInteractionDemo : MonoBehaviour
{
    [Header("Tipos de primitivas a generar (opcional)")]
    [SerializeField] private PrimitiveType[] primitiveTypes = { PrimitiveType.Cube, PrimitiveType.Sphere, PrimitiveType.Cylinder };

    [Header("Prefabs personalizados a generar (opcional)")]
    [SerializeField] private GameObject[] customPrefabs;

    [Header("Parámetros de spawn por defecto (si no hay spawnPoint)")]
    [SerializeField] private Vector3 spawnPosition = new Vector3(1, 1, 2);
    [SerializeField] private Vector3 spawnRotation = new Vector3(45, 45, 0);
    [SerializeField] private Vector3 spawnScale = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("Referencia opcional de spawn")]
    [SerializeField] private Transform spawnPoint; // Objeto de referencia en escena

    private int currentIndex = -1;
    private bool hasBeenGenerated;
    private List<GameObject> objs = new List<GameObject>();

    // Genera los objetos (primitivas y prefabs personalizados)
    public void GenerateObjects()
    {
        if (currentIndex >= 0)
            return;

        GameObject container = new GameObject("ObjectsContainer");

        // Crear primitivas si existen
        foreach (PrimitiveType type in primitiveTypes)
        {
            GameObject obj = GameObject.CreatePrimitive(type);
            InitializeObject(obj, container);
        }

        // Crear prefabs personalizados si existen
        foreach (GameObject prefab in customPrefabs)
        {
            if (prefab == null) continue;
            GameObject obj = Instantiate(prefab);
            InitializeObject(obj, container);
        }

        if (objs.Count == 0)
        {
            Debug.LogWarning("No hay objetos configurados para generar.");
            return;
        }

        currentIndex = 0;
        objs[currentIndex].SetActive(true);
        hasBeenGenerated = true;
    }

    // Inicializa posición, rotación, escala y desactiva el objeto
    private void InitializeObject(GameObject obj, GameObject container)
    {
        obj.transform.parent = container.transform;

        if (spawnPoint != null)
        {
            obj.transform.position = spawnPoint.position;
            obj.transform.rotation = spawnPoint.rotation;
        }
        else
        {
            obj.transform.position = spawnPosition;
            obj.transform.rotation = Quaternion.Euler(spawnRotation);
        }

        obj.transform.localScale = spawnScale;
        obj.SetActive(false);
        objs.Add(obj);
    }

    // Rota el objeto actual según un valor de slider (0–1)
    public void RotateObj(float sliderValue)
    {
        if (!hasBeenGenerated || currentIndex < 0 || currentIndex >= objs.Count)
            return;

        objs[currentIndex].transform.eulerAngles = new Vector3(
            spawnRotation.x,
            360 * sliderValue,
            spawnRotation.z
        );
    }

    // Cambia al objeto según el índice
    public void ChangeObj(int index)
    {
        if (!hasBeenGenerated || objs.Count == 0)
            return;

        if (index < 0 || index >= objs.Count)
        {
            Debug.LogWarning("Índice fuera de rango en ChangeObj.");
            return;
        }

        objs[currentIndex].SetActive(false);
        currentIndex = index;
        objs[currentIndex].SetActive(true);
    }

    // Activa o desactiva el objeto actual
    public void ToggleObj(bool state)
    {
        if (!hasBeenGenerated || objs.Count == 0)
            return;

        objs[currentIndex].SetActive(state);
    }
}



