using UnityEngine;
using UnityEngine.Rendering;

public class PlayerScript : MonoBehaviour
{
    public int oxygenLevel;
    public int pos = 0;
    public int lives = 5;
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
            }

        }
    }

    public void moveRight()
    {
        if (pos <= 2)
        {
            if (oxygenLevel <= 0)
            {
                fall();
            }
            else
            {
                pos += 1;
            }

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
