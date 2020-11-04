using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject creditButton;
    public GameObject healthIndicator;
    public GameObject creditPanel;


    // Start is called before the first frame update
    void Awake()
    {
        healthIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        healthIndicator.SetActive(true);
        SceneManager.LoadScene("StartScene");
    }

    public void ShowCredits()
    {
        creditPanel.SetActive(true);
    }

    public void HideCredits()
    {
        creditPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Destroy(healthIndicator);
        Application.Quit();
    }
}
