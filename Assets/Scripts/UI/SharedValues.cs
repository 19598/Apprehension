 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedValues
{
    /// <summary>
    /// Holds values that need to be used across multiple scenes. Values are passed into 
    /// these variables from both the Game and Menu scenes
    /// 
    /// All variables here are defaulted
    /// </summary>
    public static float sharedSens = .01f;//Sensitivity value
    public static bool headBob = true;//Headbob Toggle
    public static bool sharedTogRun = true;//Run Toggle
    public static bool sharedTogCrouch = false;//Crouch Toggle
    public static float mockSens = sharedSens;//float holding sens value, used for Sens lower and raise methods

    public static bool loadFlag = false;//Flag for loading saves at beginning of game scene

    /// <summary>
    /// Save all the player settigns
    /// </summary>
    public static void saveValues()
    {
        PlayerPrefs.SetFloat("sharedSens", sharedSens);
        PlayerPrefs.SetInt("headBob", headBob ? 1 : 0);
        PlayerPrefs.SetInt("sharedTogRun", sharedTogRun ? 1 : 0);
        PlayerPrefs.SetInt("sharedTogCrouch", sharedTogCrouch ? 1 : 0);
        PlayerPrefs.Save();
    }
}
