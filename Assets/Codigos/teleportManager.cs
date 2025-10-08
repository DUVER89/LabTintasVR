using UnityEngine;

public class teleportManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator transition;

    public void TeleportPlayer(Transform teleportPointRoom)
    {
        transition.SetTrigger("Usuario");
        player.transform.position = teleportPointRoom.position;
    }
}
