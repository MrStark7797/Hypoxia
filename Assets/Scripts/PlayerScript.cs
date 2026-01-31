using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public int oxygenLevel;
    public int pos = 0;
    public int lives = 5;
    private SpriteRenderer spriteRenderer;
    public void moveLeft()
    {
        if (pos >= -2)
        {
            if (oxygenLevel <= 0)
            {
                fall();
            }
            else
            {
                pos -= 1;
                transform.position += new Vector3(-1,0,0);
                oxygenLevel--;
                Debug.Log("Moved Left. Position: " + pos);
            }

        }
    }
    private void Start()
    {
        oxygenLevel = 10000000;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void moveRight()
    {
        // Breadcrumb 1: Did we enter the function?
        Debug.Log("Function called. Current pos: " + pos + " Oxygen: " + oxygenLevel);

        if (pos <= 2)
        {
            if (oxygenLevel <= 0)
            {
                Debug.Log("Move failed: No oxygen!");
                fall();
            }
            else
            {
                pos += 1;
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                transform.position += new Vector3(0.5f, 0, 0);
                GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
                transform.position += new Vector3(0.5f, 0, 0);
                oxygenLevel--;
                Debug.Log("SUCCESS! New pos: " + pos);
            }
        }
        else
        {
            Debug.Log("Move failed: Already at right boundary (pos > 2)");
        }
    }
    public void moveUp()
    {
        if (oxygenLevel <= 0)
        {
            fall();
        }
        else
        {
            oxygenLevel--;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }
    public void moveDown() {
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    public void Jump()
    {
        if (oxygenLevel <= 2)
        {
            fall();
        }
        else
        {
            oxygenLevel-=3;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }

    public void fall()
    {
        if (lives <= 1)
        {

        }
        else
        {


        }
    }
    public void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            moveLeft();
            Debug.Log(transform.position);
            
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            moveUp();
            Debug.Log(transform.position);
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            moveDown();
            Debug.Log(transform.position);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            moveRight();
            Debug.Log(transform.position);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
            Debug.Log(transform.position);
        }
    }

}
