 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedValues
{
    public static float sharedSens = .01f;
    public static bool headBob = true;
    public static bool sharedTogRun = true;
    public static bool sharedTogCrouch = false;
    public static Settings settings;
    public static float mockSens = sharedSens;

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

    /// <summary>
    /// Checks if the player has prefrences saved, and if they do they load them
    /// </summary>
    public static void loadValues()
    {
        if (PlayerPrefs.HasKey("sharedSens"))
        {
            //settings = FindObjectOfType<Settings>();
            settings.setShare(PlayerPrefs.GetFloat("sharedSens"));
            settings.setBob(PlayerPrefs.GetInt("headBob") == 1 ? true : false);
            settings.setRun(PlayerPrefs.GetInt("sharedTogRun") == 1 ? true : false);
            settings.setCrouch(PlayerPrefs.GetInt("sharedTogCrouch") == 1 ? true : false);
        }
    }
}
