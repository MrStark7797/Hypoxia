using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerInput playerInput;
    public GameObject[] buttons;
    public Sprite buttonDefault;
    public Sprite buttonHighlighted;
    private int buttonPointer = 0;

    public GameObject finalTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        HighlightButton(buttons[buttonPointer]);
    }

    private void HighlightButton(GameObject button)
    {
        button.GetComponent<Image>().sprite = buttonHighlighted;
    }

    private void UnHighlightButton(GameObject button)
    {
        button.GetComponent <Image>().sprite = buttonDefault;
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (buttonPointer == 0)
            {
                playerInput.SwitchCurrentActionMap("Player");
                pauseMenu.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().running = true;
            }
            else if (buttonPointer == 1) {
                playerInput.SwitchCurrentActionMap("Player");
                Destroy(finalTime);
                SceneManager.LoadScene("GamePlayLoop");
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UnHighlightButton(buttons[buttonPointer]);
            buttonPointer = buttonPointer == 0 ? 2 : buttonPointer - 1;
            HighlightButton(buttons[buttonPointer]);
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            UnHighlightButton(buttons[buttonPointer]);
            buttonPointer = buttonPointer == 2 ? 0 : buttonPointer + 1;
            HighlightButton(buttons[buttonPointer]);
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerInput.SwitchCurrentActionMap("UI");
            pauseMenu.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().running = false;
        }
    }

    public void OnUnpause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerInput.SwitchCurrentActionMap("Player");
            pauseMenu.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().running = true;
        }
    }
}
