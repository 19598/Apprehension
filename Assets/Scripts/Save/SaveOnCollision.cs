using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOnCollision : MonoBehaviour
{
    public GameObject player;
    public LoadManager ldmanager;
    public float dimensions = 1f;
    bool hasSaved = false;
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
    private void OnDrawGizmos()
    {
        if (hasSaved)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, dimensions);
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, dimensions);
        }
    }
}
