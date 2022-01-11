using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private void Start()
    {
        SharedValues.loadValues();
    }
    public void setRun(bool value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SharedValues.sharedTogRun = value;
        PlayerController.sprintToggle = value;
        SharedValues.saveValues();
    }

    public void setCrouch(bool value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SharedValues.sharedTogCrouch = value;
        PlayerController.crouchToggle = value;
        SharedValues.saveValues();
    }

    public void setShare(float value)
    {
        SharedValues.sharedSens = value * .001f;
        SharedValues.saveValues();
    }

    public void setBob(bool value)
    {
        SharedValues.headBob = value;
        PlayerHeadBob.headBobbing = value;
        SharedValues.saveValues();
    }
}
