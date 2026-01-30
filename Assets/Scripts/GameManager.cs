using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour { 

    public GameObject player;
    public GameObject wall;
    public WallScript wallScript;
    public GameObject wallTileBad;
    public GameObject wallTileGood;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wall = GameObject.FindGameObjectWithTag("Wall");
        wallScript = wall.GetComponent<WallScript>();
    }

    private void Start()
    {
        
    }

    private void GenerateRow()
    {
        GameObject[] newTileSet = new GameObject[8];
        for (int i = 0; i < 8; i++) {
            int r = Random.Range(1, 4);
            if (r == 1)
            {
                newTileSet[i] = wallTileBad;
            }
            else
            {
                newTileSet[i] = wallTileGood;
            }
        }
        wallScript.AddRow(newTileSet);
    }

}
