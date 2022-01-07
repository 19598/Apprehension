using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public Key key;
    public PlayerController player;
    public float openAngle;
    private bool isOpen = false;


    //opens door
    public void Open()
    {
        //if the player has the key and the door isn't open
        if (doesPlayerHaveKey() && !isOpen)
        {
            transform.Rotate(new Vector3(0, 0, openAngle));//open door
            isOpen = true;
        }
    }

    //iterates through each key the player has and sees if it has the correct one
    public bool doesPlayerHaveKey()
    {
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
