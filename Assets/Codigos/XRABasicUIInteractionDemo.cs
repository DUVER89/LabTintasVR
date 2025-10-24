using System;
using System.Collections.Generic;
using UnityEngine;

public class XRABasicUIInteractionDemo : MonoBehaviour
{
    [Header("Prefabs a generar")]
    [SerializeField] private List<GameObject> customPrefabs = new List<GameObject>();

    [Header("Punto de aparición (Spawn Point)")]
    [SerializeField] private Transform spawnPoint;

    private int currentIndex = -1;
    private bool hasBeenGenerated;
    private List<GameObject> objs = new List<GameObject>();

    public void GenerateObjects()
    {
        if (hasBeenGenerated)
            return;

        if (customPrefabs.Count == 0)
        {
            Debug.LogWarning("No hay prefabs asignados para generar.");
            return;
        }

        GameObject container = new GameObject("ObjectsContainer");

        foreach (GameObject prefab in customPrefabs)
        {
            if (prefab == null) continue;

            GameObject obj = Instantiate(prefab, spawnPoint != null ? spawnPoint.position : Vector3.zero,
                                         spawnPoint != null ? spawnPoint.rotation : Quaternion.identity,
                                         container.transform);
            obj.SetActive(false);
            objs.Add(obj);
        }

        currentIndex = 0;
        objs[currentIndex].SetActive(true);
        hasBeenGenerated = true;
    }

    public void ChangeObj(int index)
    {
        if (!hasBeenGenerated || index < 0 || index >= objs.Count)
            return;

        objs[currentIndex].SetActive(false);
        currentIndex = index;
        objs[currentIndex].SetActive(true);
    }

    public void ToggleObj(bool state)
    {
        if (!hasBeenGenerated)
            return;

        objs[currentIndex].SetActive(state);
    }

    public void RotateObj(float sliderValue)
    {
        if (!hasBeenGenerated || objs.Count == 0)
            return;

        Transform objTransform = objs[currentIndex].transform;

        Vector3 currentRotation = objTransform.eulerAngles;
        objTransform.eulerAngles = new Vector3(
            currentRotation.x,
            360f * sliderValue,
            currentRotation.z
        );
    }
}
