 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedValues
{
    public static float sharedSens = .01f;
    public static bool headBob = true;
    public static bool sharedTogRun = true;
    public static bool sharedTogCrouch = false;

    public static float mockSens = sharedSens;

    public void saveValues()
    {
        PlayerPrefs.SetFloat("sharedSens", sharedSens);
        PlayerPrefs.SetInt("headBob", headBob ? 1 : 0);
        PlayerPrefs.SetInt("sharedTogRun", sharedTogRun ? 1 : 0);
        PlayerPrefs.SetInt("sharedTogCrouch", sharedTogCrouch ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void loadValues()
    {
        sharedSens = PlayerPrefs.GetFloat("sharedSens");
        headBob = PlayerPrefs.GetInt("headBob") == 1 ? true : false;
        sharedTogRun = PlayerPrefs.GetInt("sharedTogRun") == 1 ? true : false;
        sharedTogCrouch = PlayerPrefs.GetInt("sharedTogCrouch") == 1 ? true : false;
    }
}
