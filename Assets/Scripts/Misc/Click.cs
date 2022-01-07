using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void click()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

}
