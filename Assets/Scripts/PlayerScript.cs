using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public GameObject camera;
    public int oxygenLevel;
    public int pos = 0;
    public int lives = 5;
    public float speed;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI o2Text;
    private SpriteRenderer spriteRenderer;
    void UpdateText(string message)
    {
        Canvas.ForceUpdateCanvases();
        if (scoreText != null)
        {
            scoreText.text = message;
            Canvas.ForceUpdateCanvases();
        }
    }
    void UpdateOxygen(string message)
    { Canvas.ForceUpdateCanvases(); if (o2Text != null) { o2Text.text = message; Canvas.ForceUpdateCanvases();} }
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
        oxygenLevel = 20;
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.5f;
        UpdateOxygen("Oxygen: " + oxygenLevel.ToString());


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
            var row = GameObject.Find("row" + (transform.position.y).ToString());
            var square = row.transform.GetChild((int)transform.position.x + 3).gameObject;
            CheckForOxygen(square);

            UpdateText("Level: " + transform.position.y.ToString());
            UpdateOxygen("Oxygen: " + oxygenLevel.ToString());
            if (square.name != "Rockface1_0(Clone)" && square.name != "Ice1_0(Clone)")
            {
                Debug.Log(square.name);
                int randomInt = Random.Range(0, 2);
                Debug.Log(randomInt);
                if (square.name == "Rockface_boulder_0(Clone)" || square.name == "Ice_stalagmite_0(Clone)")
                {
                    fall();
                }
                else if (square.name == "Rockface_cracked_0(Clone)" || square.name == "Ice_cracked_0(Clone)") { 

                    if (randomInt == 1)
                    {
                        fall();
                    }
                    
                }
            }

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Jump();
            UpdateText("Level: " + transform.position.y.ToString());
            UpdateOxygen("Oxygen: " + oxygenLevel.ToString());
            var row = GameObject.Find("row" + (transform.position.y).ToString());
            var square = row.transform.GetChild((int)transform.position.x + 3).gameObject;
            if (square.name != "Rockface1_0(Clone)" && square.name != "Ice1_0(Clone)")
            {
                Debug.Log(square.name);
                CheckForOxygen(square);
                if (square.name == "Rockface_boulder_0(Clone)" || square.name == "Ice_stalagmite_0(Clone)")
                {
                    fall();
                }
                else if (Random.Range(0, 2) == 0)
                {
                    fall();
                }
            }
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
			// generate new terrain
			GameObject rowCheck = GameObject.Find("row" + ((int)transform.position.y + 2).ToString());
			GameObject wall = GameObject.Find("WallObject");
			Debug.Log("Found wall " + wall.name); 
			if (rowCheck == null && wall != null) {
				wall.GetComponent<WallScript>().AddRow();
               
            }
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
			// generate new terrain
			GameObject rowCheck;
			int rowPos;
			GameObject wall = GameObject.Find("WallObject");
			rowPos = (int)transform.position.y + 2 - 1;
			rowCheck = GameObject.Find("row" + rowPos.ToString());
			Debug.Log("Found wall " + wall.name); 
			if (rowCheck == null && wall != null) {
				wall.GetComponent<WallScript>().AddRow(rowPos);
			}
			rowPos = (int)transform.position.y + 2;
			rowCheck = GameObject.Find("row" + rowPos.ToString());
			Debug.Log("Found wall " + wall.name); 
			if (rowCheck == null && wall != null) {
				wall.GetComponent<WallScript>().AddRow(rowPos);
			}
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
        SceneManager.LoadScene("Main");
    }
    public void AddOxygen(float amount)
    {
        int oldLevel = oxygenLevel;
        oxygenLevel = Mathf.Min(100, oxygenLevel + (int)amount);

        Debug.Log($"Oxygen Increased: {oldLevel} -> {oxygenLevel}");
        Canvas.ForceUpdateCanvases();
        UpdateOxygen("Oxygen: " + oxygenLevel.ToString());
        Canvas.ForceUpdateCanvases();
    }
    private void CheckForOxygen(GameObject square)
    {
        OnTriggerEffect oxygen = square.GetComponentInChildren<OnTriggerEffect>();

        if (oxygen != null)
        {
            Debug.Log("Oxygen found on square!");
            AddOxygen(oxygen.oxygenRestoreAmount);
            if (oxygen.prefabLooted != null)
            {
                Instantiate(oxygen.prefabLooted, oxygen.transform.position, Quaternion.identity, oxygen.transform.parent);
            }
            Destroy(oxygen.gameObject);
        }
    }
}
