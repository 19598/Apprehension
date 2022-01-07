using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public void setRun(bool value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SharedValues.sharedTogRun = value;
        PlayerController.sprintToggle = value;
    }

    public void setCrouch(bool value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SharedValues.sharedTogCrouch = value;
        PlayerController.crouchToggle = value;
    }

    public void setShare(float value)
    {
        SharedValues.sharedSens = value * .001f;
    }

    public void setBob(bool value)
    {
        SharedValues.headBob = value;
        PlayerHeadBob.headBobbing = value;
    }
}
