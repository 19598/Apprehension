using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class canPickup : MonoBehaviour
{
    public Display display;
    public GameObject player;
    public float dimensions = 1f;
    bool hasPickedUp = false;
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= dimensions && !hasPickedUp)
        {
            display.ApplyTextToScreen("Picked Up " + this.gameObject.name, 3f);
            player.GetComponent<PlayerController>().keys.Add(this.gameObject);

            hasPickedUp = true;
        }
        if (hasPickedUp)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnDrawGizmos()
    {
        if (hasPickedUp)
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
