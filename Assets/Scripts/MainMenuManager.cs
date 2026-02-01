using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private DataStorage DataStorage;

    public Sprite buttonHighlighted;
    public Sprite buttonDefault;
    public GameObject[] buttons;
    private int buttonPointer = 0;

    private bool endlessMode = false;
    public TextMeshProUGUI modeText;
    public GameObject menuObject;
    public GameObject leaderboardObject;
    public GameObject leaderboardStandard;
    public GameObject leaderboardEndless;
    public GameObject standardScoresText;
    public GameObject endlessScoresText;
    private bool inLeaderboard;
    public GameObject[] leaderboardButtons;
    private int leaderboardPointer = 0;

    private void Start()
    {
        DataStorage = GameObject.FindGameObjectWithTag("Data").GetComponent<DataStorage>();
        modeText.text = DataStorage.endlessMode ? "Mode: Endless" : "Mode: Standard";

        HighlightButton(buttons[buttonPointer]);

        inLeaderboard = false;
    }

    private void HighlightButton(GameObject button)
    {
        button.GetComponent<Image>().sprite = buttonHighlighted;
        button.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1f);
    }

    private void UnHighlightButton(GameObject button)
    {
        button.GetComponent<Image>().sprite = buttonDefault;
        button.GetComponent<RectTransform>().localScale = new Vector3(1.25f, 1.25f, 1f);

    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inLeaderboard)
            {
                UnHighlightButton(leaderboardButtons[leaderboardPointer]);
                leaderboardPointer = (leaderboardPointer + 1) % 2;
                HighlightButton(leaderboardButtons[leaderboardPointer]);
            }
            else
            {
                UnHighlightButton(buttons[buttonPointer]);
                buttonPointer = buttonPointer == 0 ? 3 : buttonPointer - 1;
                HighlightButton(buttons[buttonPointer]);
            }
            
        }
    }

    public void OnDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inLeaderboard)
            {
                UnHighlightButton(leaderboardButtons[leaderboardPointer]);
                leaderboardPointer = (leaderboardPointer + 1) % 2;
                HighlightButton(leaderboardButtons[leaderboardPointer]);
            }
            else
            {
                UnHighlightButton(buttons[buttonPointer]);
                buttonPointer = buttonPointer == 3 ? 0 : buttonPointer + 1;
                HighlightButton(buttons[buttonPointer]);
            }
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inLeaderboard)
            {
                if (leaderboardPointer == 0)
                {
                    endlessMode = !endlessMode;
                    DataStorage.endlessMode = endlessMode;
                    modeText.text = endlessMode ? "Mode: Endless" : "Mode: Standard";
                    leaderboardEndless.SetActive(endlessMode);
                    leaderboardStandard.SetActive(!endlessMode);
                }
                else
                {
                    menuObject.SetActive(true);
                    leaderboardObject.SetActive(false);
                    inLeaderboard = false;
                }
            }
            else
            {
                if (buttonPointer == 0)
                {
                    SceneManager.LoadScene("GamePlayLoop");
                }
                else if (buttonPointer == 1)
                {
                    endlessMode = !endlessMode;
                    DataStorage.endlessMode = endlessMode;
                    modeText.text = endlessMode ? "Mode: Endless" : "Mode: Standard";
                }
                else if (buttonPointer == 2)
                {
                    menuObject.SetActive(false);
                    leaderboardObject.SetActive(true);
                    HighlightButton(leaderboardButtons[leaderboardPointer]);
                    UnHighlightButton(leaderboardButtons[(leaderboardPointer + 1) % 2]);
                    if (endlessMode)
                    {
                        leaderboardEndless.SetActive(true);
                        leaderboardStandard.SetActive(false);
                        endlessScoresText.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + DataStorage.DisplayEndlessScores() + "</mspace>";
                    }
                    else
                    {
                        leaderboardEndless.SetActive(false);
                        leaderboardStandard.SetActive(true);
                        standardScoresText.GetComponent<TextMeshProUGUI>().text = "<mspace=0.5em>" + DataStorage.DisplayStandardScores() + "</mspace>";
                    }
                    inLeaderboard = true;
                    
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }

}
