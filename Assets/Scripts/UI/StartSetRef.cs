using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSetRef : MonoBehaviour
{
    /// <summary>
    /// Takes in all toggles from menu scene to be used on start
    /// </summary>
    public Toggle toggleRun;
    public Toggle toggleCrouch;
    public Toggle toggleBob;

    // Start is called before the first frame update
    /// <summary>
    /// Sets all toggles to values saved in playerprefs. Used for when game is closed and reopened.
    /// </summary>
    void Start()
    {
        toggleRun.isOn = (PlayerPrefs.GetInt("sharedTogRun") == 1 ? true : false);
        toggleCrouch.isOn = (PlayerPrefs.GetInt("sharedTogCrouch") == 1 ? true : false);
        toggleBob.isOn = (PlayerPrefs.GetInt("headBob") == 1 ? true : false);
    }

}
