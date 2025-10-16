using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour
{
    [System.Serializable]
    public class RadialOption
    {
        public string label;
        public Sprite icon;
        public UnityEngine.Events.UnityEvent onClick;
    }

    public List<RadialOption> options = new List<RadialOption>();
    public GameObject buttonPrefab;
    public float radius = 150f;

    void Start()
    {
        CreateRadialLayout();
    }

    void CreateRadialLayout()
    {
        float angleStep = 360f / options.Count;
        float angle = 0f;

        foreach (var option in options)
        {
            GameObject newButton = Instantiate(buttonPrefab, transform);
            newButton.transform.localPosition = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                0f
            );

            // Configura texto o icono
            TMP_Text text = newButton.GetComponentInChildren<TMP_Text>();
            if (text != null)
                text.text = option.label;

            Image icon = newButton.GetComponentInChildren<Image>();
            if (icon != null && option.icon != null)
                icon.sprite = option.icon;

            // Asigna evento
            Button b = newButton.GetComponent<Button>();
            if (b != null)
                b.onClick.AddListener(() => option.onClick.Invoke());

            angle += angleStep;
        }
    }
}

