using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSetRef : MonoBehaviour
{
    public Toggle toggleRun;
    public Toggle toggleCrouch;
    public Toggle toggleBob;

    // Start is called before the first frame update
    void Start()
    {
        toggleRun.isOn = (PlayerPrefs.GetInt("sharedTogRun") == 1 ? true : false);
        toggleCrouch.isOn = (PlayerPrefs.GetInt("sharedTogCrouch") == 1 ? true : false);
        toggleBob.isOn = (PlayerPrefs.GetInt("headBob") == 1 ? true : false);
    }

}
