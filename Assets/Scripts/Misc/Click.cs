using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a click sound
/// </summary>
public class Click : MonoBehaviour
{
    public void click()
    {
        FindObjectOfType<AudioManager>().Play("Click");
    }

}
