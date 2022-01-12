using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityValue : MonoBehaviour
{
    public Text newText;
    public Slider slider;


    /// <summary>
    /// Changes values of the slider and slider text to the saved value
    /// </summary>
    private void Start()
    {
        newText.text = ((PlayerPrefs.GetFloat("sharedSens")*1000) + "");
        slider.value = (PlayerPrefs.GetFloat("sharedSens")*1000);
    }

    /// <summary>
    /// Updates slider value text depening on slider value
    /// </summary>
    /// <param name="value"></param>
    public void textUpdate(float value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        newText.text = (Math.Round(value, 2) + "");
    }
}
