using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour
{

    public InputField inputField;
    public TextMeshProUGUI timeText;

    private DataStorage dataStorage;

    private void Start()
    {
        inputField = GetComponent<InputField>();
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(gameObject);
        dataStorage = GameObject.FindGameObjectWithTag("Data").GetComponent<DataStorage>();

        timeText.text = dataStorage.finalTime.ToString("00:00.00");

    }

    public void OnPress(string s)
    {
        dataStorage.WriteStandardScores(s);

        SceneManager.LoadScene("Main");
    }
}
