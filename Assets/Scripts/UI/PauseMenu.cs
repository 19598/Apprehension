using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public LoadManager loadmanage;
    public static bool gamePaused = false;
    public GameObject pauseMenuUI;
    public GameObject Health;
    public GameObject WinScreen;


    /// <summary>
    /// Pauses/Unpauses when esape is pressed
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Play("Click");
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// When called, drops pause menu, resumes time, locks cursor, and increases sensitivity to original amount.
    /// </summary>
    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        LookRaise();
    }

    /// <summary>
    /// activates pause menu, stops time, unlocks cursor, and decreases sensitivity to 0
    /// </summary>
    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        LookLower();
    }

    /// <summary>
    /// Resumes game, unlocks cursor, and returns to main menu
    /// </summary>
    public void GiveUp()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Resume();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Decreases sensitivity to 0
    /// </summary>
    public void LookRaise()
    {
        float sensValue = SharedValues.mockSens;

        if (SharedValues.sharedSens == 0)
        {
            SharedValues.sharedSens += sensValue;
        } 
    }

    /// <summary>
    /// Returns sensitivity to 'original' value
    /// </summary>
    public void LookLower()
    {
        float sensValue = SharedValues.mockSens;

        if (SharedValues.sharedSens != 0)
        {
            SharedValues.sharedSens -= sensValue;
        }
    }

    /// <summary>
    /// Saves current gamestate
    /// </summary>
    public void Save()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        loadmanage.Save("recent");
    }

    /// <summary>
    /// Loads most recent save
    /// </summary>
    public void Load()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("1");
        if (SaveGame.getSaves().Count > 0)
        {
            loadmanage.Load("recent");
            Debug.Log("2");
        }
    }

    /// <summary>
    /// Takes in health value and checks if it is equal to or less than 0. If it is, decreases sensitivity to 0
    /// </summary>
    /// <param name="flo"></param>
    public void HealthCheck(float flo)
    {
        if (flo <= 0)
        {
            LookLower();
        }
    }
}
