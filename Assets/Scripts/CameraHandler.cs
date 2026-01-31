using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class CameraHandler : MonoBehaviour
{
	public GameObject player;
    private void Update()
    {
        transform.position = new Vector3(0,player.transform.position.y,0);
    }
}
