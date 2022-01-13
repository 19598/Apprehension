using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    /// <summary>
    /// Takes in toggle value and updates SharedValues togglerun bool. Then saves to playerprefs
    /// </summary>
    /// <param name="value"></param>
    public void setRun(bool value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SharedValues.sharedTogRun = value;
        PlayerController.sprintToggle = value;
        SharedValues.saveValues();
    }

    /// <summary>
    /// Takes in toggle value and updates SharedValues togglecrouch bool. Then saves to playerprefs
    /// </summary>
    /// <param name="value"></param>
    public void setCrouch(bool value)
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SharedValues.sharedTogCrouch = value;
        PlayerController.crouchToggle = value;
        SharedValues.saveValues();
    }

    /// <summary>
    /// Takes in slider value, multiplies to account for low input values. Then saves to playerprefs
    /// </summary>
    /// <param name="value"></param>
    public void setShare(float value)
    {
        SharedValues.sharedSens = value * .001f;
        SharedValues.mockSens = value * .001f;
        SharedValues.saveValues();
    }

    /// <summary>
    /// Takes in toggle value and updates SharedValues togglebob bool. Then saves to playerprefs
    /// </summary>
    /// <param name="value"></param>
    public void setBob(bool value)
    {
        SharedValues.headBob = value;
        PlayerHeadBob.headBobbing = value;
        SharedValues.saveValues();
    }
}
