using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPuzzle : MonoBehaviour
{
    public GameObject key;
    public PlayerController player;
    public float openAngle;
    private bool isOpen = false;


    public void Open()
    {
        if (doesPlayerHaveKey() && !isOpen)
        {
            transform.Rotate(new Vector3(0, 0, openAngle));
            isOpen = true;
        }
    }

    public bool doesPlayerHaveKey()
    {
        try
        {
            if (player.keys.BinarySearch(key) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    void Update()
    {
        Open();
    }
}
