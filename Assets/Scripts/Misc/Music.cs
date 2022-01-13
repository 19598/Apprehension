using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays main menu music
/// </summary>
public class Music : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainMenuMusic");
    }
}
