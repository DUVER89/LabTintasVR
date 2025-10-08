using UnityEngine;

public class teleportManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void TeleportPlayer(Transform teleportPointRoom)
    {
        player.transform.position = teleportPointRoom.position;
    }
}
