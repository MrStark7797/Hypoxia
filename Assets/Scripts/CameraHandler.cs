using UnityEngine;

public class CameraHandler : MonoBehaviour
{
	public GameObject player;

    void Update() {
        transform.position = new Vector3(0,player.transform.position.y,0);
    }
}
