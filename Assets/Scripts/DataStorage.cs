using UnityEngine;

public class DataStorage : MonoBehaviour
{
    public bool endlessMode = false;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
