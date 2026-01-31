using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public int oxygenLevel;
    public int pos = 0;
    public int lives = 5;
    public float speed;
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
                transform.position += new Vector3(-1, 0, 0);
                oxygenLevel--;
                Debug.Log("Moved Left. Position: " + pos);
            }

        }
    }
    private void Start()
    {
        oxygenLevel = 10000000;
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.5f;
        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // 'Started' is like 'wasPressedThisFrame'
        if (context.started)
        {
            Vector2 inputVec = context.ReadValue<Vector2>();
            Debug.Log(inputVec);
            if (inputVec.x < -0.5f) moveLeft();
            else if (inputVec.x > 0.5f) moveRight();

            if (inputVec.y > 0.5f) moveUp();
            else if (inputVec.y < -0.5f) moveDown();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump();
        }
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
                transform.position += new Vector3(1, 0, 0);
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
}
