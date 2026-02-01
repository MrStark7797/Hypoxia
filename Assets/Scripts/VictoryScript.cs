using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    public void OnPress(InputAction.CallbackContext context)
    {
        if (context.started) { SceneManager.LoadScene("Main"); }
    }
}
