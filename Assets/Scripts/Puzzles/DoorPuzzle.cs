using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public Key key;
    public PlayerController player;
    public float openAngle;
    private bool isOpen = false;

    /// <summary>
    /// Opens the door
    /// </summary>
    public void Open()
    {
        //if the player has the key and the door isn't open
        if (doesPlayerHaveKey() && !isOpen)
        {
            transform.Rotate(new Vector3(0, 0, openAngle));//open door
            isOpen = true;
        }
    }

    
    /// <summary>
    /// Checks if the player has the key corresponding to this door
    /// </summary>
    /// <returns>Whether or not he player has the key (boolean)</returns>
    public bool doesPlayerHaveKey()
    {
        //loops through all the keys the player has and checks if they have the correct one
        foreach (GameObject keys in player.keys)
        {
            if (keys.GetComponent<Key>().name == key.name)
            {
                return true;
            }
        }
        return false;
    }

    void Update()
    {
        Open();
    }
}
