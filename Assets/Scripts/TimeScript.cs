using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    private TextMeshPro text;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();

        DontDestroyOnLoad(gameObject);
    }

    public void DisplayFinalTime(float time)
    {
        text.text = "Final Time: " + ((int)time / 60).ToString() + ":" + (time % 60).ToString("0.00");
    }
}
