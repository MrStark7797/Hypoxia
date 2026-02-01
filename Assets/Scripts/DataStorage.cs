using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public bool endlessMode = false;

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("Data").Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

}
