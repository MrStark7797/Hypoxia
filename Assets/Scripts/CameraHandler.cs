using UnityEngine;

public class CameraHandler : MonoBehaviour
{
	public GameObject player;

    void Update() {
        transform.position.y = player.transform.position.y;
    }
}
