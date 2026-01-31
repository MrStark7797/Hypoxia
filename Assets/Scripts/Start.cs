using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    public void OnSpace(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene("GamePlayLoop");
        }

    }
}
