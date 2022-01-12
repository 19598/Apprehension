using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinBehavior : MonoBehaviour
{
    public GameObject winScn;
    public GameObject player;
    public float dimensions = 1f;
    bool hasWon = false;

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= dimensions && !hasWon)
        {
            Debug.Log("Winning...");
            winScn.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            LowerSens();
            float sensValue = SharedValues.mockSens;

            if (SharedValues.sharedSens != 0)
            {
                SharedValues.sharedSens -= sensValue;
            }
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

        hasWon = true;
    }
}
