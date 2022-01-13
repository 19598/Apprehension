using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnCollision : MonoBehaviour
{
    public GameObject player;
    public LoadManager ldmanager;
    public float dimensions = 1f;
    bool hasSaved = false;

    /// <summary>
    /// If the player is close enough to the collider, it saves the game
    /// </summary>
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= dimensions && !hasSaved)
        {
            Debug.Log("Saving...");
            ldmanager.Save("recent");
            hasSaved = true;
        }
    }
}
