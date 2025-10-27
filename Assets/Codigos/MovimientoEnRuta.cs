using UnityEngine;

public class MovimientoEnRuta : MonoBehaviour
{
    [Header("Puntos de la ruta (Objetos vacíos)")]
    [SerializeField] private Transform[] puntos;

    [Header("Velocidad de movimiento")]
    [SerializeField] private float velocidad = 2f;

    private int indiceActual = 0;
    private bool avanzando = true;

    void Update()
    {
        if (puntos.Length == 0) return;

        // Mover el objeto hacia el punto actual
        transform.position = Vector3.MoveTowards(transform.position, puntos[indiceActual].position, velocidad * Time.deltaTime);

        // Verificar si llegó al punto actual
        if (Vector3.Distance(transform.position, puntos[indiceActual].position) < 0.05f)
        {
            if (avanzando)
            {
                indiceActual++;
                // Si llegó al último punto, cambiar dirección
                if (indiceActual >= puntos.Length)
                {
                    indiceActual = puntos.Length - 2;
                    avanzando = false;
                }
            }
            else
            {
                indiceActual--;
                // Si llegó al primer punto, cambiar dirección
                if (indiceActual < 0)
                {
                    indiceActual = 1;
                    avanzando = true;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Dibujar líneas entre puntos para visualizar la ruta
        Gizmos.color = Color.yellow;
        for (int i = 0; i < puntos.Length - 1; i++)
        {
            if (puntos[i] != null && puntos[i + 1] != null)
                Gizmos.DrawLine(puntos[i].position, puntos[i + 1].position);
        }
    }
}
