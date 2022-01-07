using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public Key key;
    public PlayerController player;
    public float openAngle;
    private bool isOpen = false;


    public void Open()
    {
        if (doesPlayerHaveKey() && !isOpen)
        {
            Debug.Log(name + "is open");
            transform.Rotate(new Vector3(0, 0, openAngle));
            isOpen = true;
        }
    }

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
