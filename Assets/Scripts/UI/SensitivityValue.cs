using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityValue : MonoBehaviour
{
    Text newText;
    public Slider slider;
    public Toggle toggleRun;
    public Toggle toggleCrouch;
    public Toggle toggleBob;

    private void Start()
    {
        newText = GetComponent<Text>();
        newText.text = (10f + "");
        slider.value = 10f;
        toggleRun.isOn = SharedValues.sharedTogRun;
        toggleCrouch.isOn = SharedValues.sharedTogCrouch;
        toggleBob.isOn = SharedValues.headBob;
    }

    public void textUpdate(float value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        newText.text = (Math.Round(value, 2) + "");
    }

}
