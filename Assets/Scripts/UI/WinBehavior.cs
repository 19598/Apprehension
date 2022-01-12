using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBehavior : MonoBehaviour
{
    /// <summary>
    /// Pulls in the Win screen and playerController
    /// </summary>
    public GameObject winScn;
    public GameObject player;
    public float dimensions = 1f;
    bool hasWon = false;


    /// <summary>
    /// Checks to see if player is in contact with collider
    /// If it is, game is paused, sens is lowered, and hasWon is set to true
    /// </summary>
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= dimensions && !hasWon)
        {
            winScn.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            LowerSens();

            hasWon = true;
        }
    }
    
    private void LowerSens()
    {
        float sensValue = SharedValues.mockSens;

        if (SharedValues.sharedSens != 0)
        {
            SharedValues.sharedSens -= sensValue;
        }
    }
}
