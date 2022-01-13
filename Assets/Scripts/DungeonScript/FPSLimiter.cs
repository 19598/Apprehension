using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    /// <summary>
    /// Limits the framerate of the game and turns off vsync
    /// </summary>
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;   
    }
}
