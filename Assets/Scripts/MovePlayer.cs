using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float MoveSpeed;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveSpeed = 1;
        transform.position += transform.up * MoveSpeed * 10 * Time.deltaTime;
    }
}
