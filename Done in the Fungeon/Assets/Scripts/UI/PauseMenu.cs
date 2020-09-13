using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Code from COMP260 Week 10 Prac
public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject resumeButton;
    public GameObject mainMenuButton;
    private bool paused = true;

    // Start is called before the first frame update
    void Start()
    {
        SetPaused(paused);
    }

    // Update is called once per frame
    void Update()
    {
        // pause if the player presses escape
        if (paused == false && Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
            Debug.Log("Escape key pressed");
            SetPaused(true);
        }
    }

    private void SetPaused(bool p)
    {
        // make the shell panel (in)active when (un)paused
        paused = p;
        Debug.Log("SetPaused Called");
        Time.timeScale = paused ? 0 : 1;
    }

    public void OnPressedPlay()
    {
        // resume the game
        pausePanel.SetActive(false);
        SetPaused(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}