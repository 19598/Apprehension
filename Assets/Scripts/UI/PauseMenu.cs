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

    void Start()
    {
        
    }

    // Update is called once per frame
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

    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        LookRaise();
    }

    public void Pause()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        LookLower();
    }

    public void GiveUp()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Resume();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void LookRaise()
    {
        float sensValue = SharedValues.mockSens;

        if (SharedValues.sharedSens == 0)
        {
            SharedValues.sharedSens += sensValue;
        } 

        
    }

    public void LookLower()
    {
        float sensValue = SharedValues.mockSens;

        if (SharedValues.sharedSens != 0)
        {
            SharedValues.sharedSens -= sensValue;
        }
    }

    public void Save()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        loadmanage.Save("recent");
    }

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

    public void HealthCheck(float flo)
    {
        float value = flo;

        if (value < 1)
        {
            LookLower();
        }
    }

    public void PullWinScreen()
    {
        
    }
}
