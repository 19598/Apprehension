using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Loads the next scene and changes the SharedValues script's loadFlag variable to true
    /// </summary>
    /// 
    public void ContinueGame()
    {
        if (SaveGame.getSaves().Count > 0)
        {
            FindObjectOfType<AudioManager>().Play("Click");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SharedValues.loadFlag = true;
        }
    }
    
    /// <summary>
    /// Loads the next scene and changes the SharedVales script's loadFlag variable to false
    /// Also deletes current save files.
    /// </summary>
    public void NewGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SharedValues.loadFlag = false;
        SaveGame.deleteSave();
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
