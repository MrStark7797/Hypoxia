using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerScript : MonoBehaviour
{
    public GameObject camera;
    public int oxygenLevel;
    public int lives = 5;
    public float speed;
    public Vector3 currentCheckpoint;
    public GameObject prefabPiton;
    public Sprite perryClimb;
    public Sprite perryFall;
    private bool addRows = true;
    private float peakPos = Mathf.Infinity;

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
            //Debug.Log("Direction 1 or 3");
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
        if (transform.position.x >= -2 && !isBoulder(transform.position, 3))
        {
            if (oxygenLevel <= 0)
            {
                fall();
            }
            else
            {
               
                transform.position += new Vector3(-1, 0, 0);
                oxygenLevel--;
                Debug.Log("Moved Left. Position: " + transform.position.x);
            }

        }
    }
    private void Start()
    {
        oxygenLevel = 20;
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = 0.5f;
        UpdateOxygen("Oxygen: " + oxygenLevel.ToString());
        currentCheckpoint = new Vector3(0, 0, -0.1f);
        GameObject.Instantiate(prefabPiton, new Vector3(transform.position.x, transform.position.y + 0.25f, -0.05f), Quaternion.identity, GameObject.Find("row0").transform.GetChild((int)transform.position.x + 3));

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        // 'Started' is like 'wasPressedThisFrame'
        if (context.started)
        {
            Vector2 inputVec = context.ReadValue<Vector2>();
            //Debug.Log(inputVec);
            if (inputVec.x < -0.5f) moveLeft();
            else if (inputVec.x > 0.5f) moveRight();

            if (inputVec.y > 0.5f) moveUp();
            else if (inputVec.y < -0.5f) moveDown();
            var row = GameObject.Find("row" + (transform.position.y).ToString());
            var square = row.transform.GetChild((int)transform.position.x + 3).gameObject;
            CheckForOxygen(square);
            CheckCheckpoint(square);
            CheckPeak();
            UpdateText("Level: " + transform.position.y.ToString());
            UpdateOxygen("Oxygen: " + oxygenLevel.ToString());
            if (square.name != "Rockface1_0(Clone)" && square.name != "Ice1_0(Clone)")
            {
                //Debug.Log(square.name);
                int randomInt = Random.Range(0, 5);
                //Debug.Log(randomInt);
                if (square.name == "Rockface_boulder_0(Clone)" || square.name == "Ice_stalagmite_0(Clone)")
                {
                    fall();
                }
                else if (square.name == "Rockface_cracked_0(Clone)" || square.name == "Ice_cracked_0(Clone)") { 

                    if (randomInt == 0)
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
            CheckForOxygen(square);
            CheckCheckpoint(square);
            CheckPeak();
            if (square.name != "Rockface1_0(Clone)" && square.name != "Ice1_0(Clone)")
            {
                //Debug.Log(square.name);
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
        Debug.Log("Function called. Current pos: " +transform.position+ " Oxygen: " + oxygenLevel);

        if (transform.position.x <= 2 && !isBoulder(transform.position, 1))
        {
            if (oxygenLevel <= 0)
            {
                //Debug.Log("Move failed: No oxygen!");
                fall();
            }
            else
            {

                transform.position += new Vector3(1, 0, 0);
                oxygenLevel--;
            }
        }
        else
        {
            //Debug.Log("Move failed: Already at right boundary (pos > 2) OR Move into Boulder");
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
            //Debug.Log("Move is into Boulder");
        }
        else
        {
            oxygenLevel--;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            transform.position += new Vector3(0, 1, 0);
            camera.transform.position = new Vector3(0, transform.position.y, -10);
            //Debug.Log(camera.transform.position);
			// generate new terrain
			GameObject rowCheck = GameObject.Find("row" + ((int)transform.position.y + 2).ToString());
			GameObject wall = GameObject.Find("WallObject");
			//Debug.Log("Found wall " + wall.name); 
			if (rowCheck == null && wall != null && addRows) {
				wall.GetComponent<WallScript>().AddRow();
               
            }
        }

    }
    public void moveDown() {
        if (isBoulder(transform.position, 2))
        {
            //Debug.Log("Move is into Boulder");
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
			//Debug.Log("Found wall " + wall.name); 
			if (rowCheck == null && wall != null && addRows) {
				wall.GetComponent<WallScript>().AddRow(rowPos);
			}
			rowPos = (int)transform.position.y + 2;
			rowCheck = GameObject.Find("row" + rowPos.ToString());
			//Debug.Log("Found wall " + wall.name); 
			if (rowCheck == null && wall != null && addRows) {
				wall.GetComponent<WallScript>().AddRow(rowPos);
			}
        }
    }

    public void fall()
    {
        if (lives <= 1 || oxygenLevel <= 0)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            StartCoroutine(MoveToCheckpoint(currentCheckpoint));
            //transform.position = currentCheckpoint;
            //camera.transform.position = new Vector3(0, transform.position.y, -10);
        }
        
    }

    private IEnumerator MoveToCheckpoint(Vector3 aimPosition)
    {
        GetComponent<SpriteRenderer>().sprite = perryFall;
        while (transform.position != aimPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, aimPosition, Time.deltaTime);
            camera.transform.position = new Vector3(0, transform.position.y, -10);
            yield return new WaitForEndOfFrame();
        }
        GetComponent<SpriteRenderer>().sprite = perryClimb;
    }

    public void AddOxygen(float amount)
    {
        int oldLevel = oxygenLevel;
        oxygenLevel = Mathf.Min(100, oxygenLevel + (int)amount);

        //Debug.Log($"Oxygen Increased: {oldLevel} -> {oxygenLevel}");
        Canvas.ForceUpdateCanvases();
        UpdateOxygen("Oxygen: " + oxygenLevel.ToString());
        Canvas.ForceUpdateCanvases();
    }
    private void CheckForOxygen(GameObject square)
    {
        OnTriggerEffect oxygen = square.GetComponentInChildren<OnTriggerEffect>();

        if (oxygen != null)
        {
            //Debug.Log("Oxygen found on square!");
            AddOxygen(oxygen.oxygenRestoreAmount);
            if (oxygen.prefabLooted != null)
            {
                Instantiate(oxygen.prefabLooted, oxygen.transform.position, Quaternion.identity, oxygen.transform.parent);
            }
            Destroy(oxygen.gameObject);
        }
    }

    private void CheckCheckpoint(GameObject square)
    {
        Debug.Log(transform.position);
        Debug.Log(currentCheckpoint);
        if (transform.position.y - currentCheckpoint.y >= 10 && (square.name == "Rockface1_0(Clone)" || square.name == "Ice1_0(Clone)"))
        {
            currentCheckpoint = transform.position;
            GameObject.Instantiate(prefabPiton, new Vector3(transform.position.x, transform.position.y+0.25f, -0.05f), Quaternion.identity, square.transform);
        }
    }

    public void ReachPeak(float PeakPos)
    {
        addRows = false;
        peakPos = PeakPos;
    }

    private void CheckPeak()
    {
        if (transform.position.y >= peakPos)
        {
            SceneManager.LoadScene("Main");
        }
    }
}
