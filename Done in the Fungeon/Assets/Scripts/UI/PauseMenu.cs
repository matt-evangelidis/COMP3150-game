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
    public GameObject pauseButton;
    [Tooltip("Game Objects to be destroyed when returning to the main menu")]
    public List<GameObject> toDestroy;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        SetPaused(paused);
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // pause if the player presses escape
        if (paused == false && Input.GetButtonDown("Pause"))
        {
            SetPaused(true);
        }
    }

    private void SetPaused(bool p)
    {
        // make the shell panel (in)active when (un)paused
        paused = p;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }

    public void OnPressedPlay()
    {
        // resume the game
        SetPaused(false);
    }

    public void BackToMainMenu()
    {
        foreach (GameObject go in toDestroy)
        {
            Destroy(go);
        }
        SceneManager.LoadScene("Main Menu");
    }

    public void PauseGame()
    {
        SetPaused(true);
    }
}