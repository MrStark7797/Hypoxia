using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public GameObject camera;
    public int oxygenLevel;
    public int pos = 0;
    public int lives = 5;
    public float speed;
    private SpriteRenderer spriteRenderer;
    private bool isBoulder(Vector3 position, int direction)
    {
        GameObject square = null;
        if (direction == 1 || direction == 3)
        {
            var row = GameObject.Find("row" + position.y.ToString());
            Debug.Log("Direction 1 or 3");
            if (direction == 1)
            {
                square = row.transform.GetChild((int)position.x + 3 + 1).gameObject;
            }
            else
            {
                square = row.transform.GetChild((int)position.x + 3 - 1).gameObject;
            }
        } else if (direction == 0) { 
            var row = GameObject.Find("row" + (position.y+1).ToString());
            square = row.transform.GetChild((int)position.x + 3).gameObject;
        } else if(direction == 2)
        {
            var row = GameObject.Find("row" + (position.y - 1).ToString());
            square = row.transform.GetChild((int)position.x + 3).gameObject;
        }
        if (square.name == "Rockface_boulder_0(Clone)" || square.name == "Ice_stalagmite_0(Clone)")
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    public void moveLeft()
    {
        if (pos >= -2 && !isBoulder(transform.position, 3))
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

        if (pos <= 2 && !isBoulder(transform.position, 1))
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
            Debug.Log("Move failed: Already at right boundary (pos > 2) OR Move into Boulder");
        }
    }
    public void moveUp()
    {
        if (oxygenLevel <= 0)
        {
            fall();
        }
        else if (isBoulder(transform.position, 0))
        {
            Debug.Log("Move is into Boulder");
        }
        else
        {
            oxygenLevel--;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            transform.position += new Vector3(0, 1, 0);
            camera.transform.position = new Vector3(0, transform.position.y, -10);
            Debug.Log(camera.transform.position);
        }
    }
    public void moveDown() {
        if (isBoulder(transform.position, 2))
        {
            Debug.Log("Move is into Boulder");
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            transform.position += new Vector3(0, -1, 0);
            camera.transform.position = new Vector3(0, transform.position.y, -10);
        }
        
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
            transform.position += new Vector3(0, 2, 0);
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            camera.transform.position = new Vector3(0, transform.position.y, -10);
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
