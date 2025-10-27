using UnityEngine;
using UnityEngine.Events;

public class TriggerMachine : MonoBehaviour
{
    public string interactionTag;
    public GameObject Spawn;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(interactionTag))
        {
            other.transform.position = Spawn.transform.position;
            other.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}
