using System.Security.Cryptography;
using UnityEngine;

public class PatternSlicer : MonoBehaviour
{
    public Texture2D masterPattern;
    public int seedID;

    public int width = 32;
    public int height = 32;
    public float pixelsPerUnit = 16f;
    void Start()
    {
        GenerateUniqueWall();
    }

    public void GenerateUniqueWall()
    {
        Random.InitState(seedID);
        int maxX = masterPattern.width - width;
        int maxY = masterPattern.height - height;

        int randomX = Random.Range(0, maxX);
        int randomY = Random.Range(0, maxY);

        Rect sliceRect = new Rect(randomX, randomY, width, height);
        Sprite newSprite = Sprite.Create(masterPattern, sliceRect, new Vector2(0.5f, 0.5f), pixelsPerUnit);

        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}