using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndlessEndingScript : MonoBehaviour
{
    public InputField inputField;
    public TextMeshProUGUI heightText;

    private DataStorage dataStorage;

    private void Start()
    {
        inputField = GetComponent<InputField>();
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(gameObject);
        dataStorage = GameObject.FindGameObjectWithTag("Data").GetComponent<DataStorage>();

        heightText.text = "Final Height: " + dataStorage.height.ToString() + "M";

    }

    public void OnPress(string s)
    {
        dataStorage.WriteEndlessScores(s);

        SceneManager.LoadScene("Main");
    }
}
