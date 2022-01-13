using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    /// <summary>
    /// Takes in text from the Continue button
    /// </summary>
    public Text text;

    /// <summary>
    /// On start, checks if there are any saves in the database.
    /// If there are none, the 'Continue' button gets greyed out
    /// </summary>
    public void Start()
    {
        bool setTextVal = false;

        Debug.Log(SaveGame.getSaves().Count);
        while (SaveGame.getSaves().Count < 1 && !setTextVal)
        {
            text.color = Color.grey;
            setTextVal = true;
        }
    }
}
