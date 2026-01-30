using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour { 

    public GameObject player;
    public GameObject wall;
    public WallScript wallScript;
    public GameObject wallTile;

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
        Tile[] newTileSet = new Tile[8];
        for (int i = 0; i < 8; i++) {
            int r = Random.Range(1, 4);
            if (r == 1)
            {

            }
        }
    }

}
