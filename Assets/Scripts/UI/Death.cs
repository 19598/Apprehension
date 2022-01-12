using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    //Holding the Death Screen game object
    public GameObject deathScreen;

    /// <summary>
    /// Checks the health of the player and activates the death screen when health is at or below 0
    /// </summary>
    /// <param name="value"></param>
    public void checkDeath(float value)
    {
        if (value <= 0)
        {
            deathScreen.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
