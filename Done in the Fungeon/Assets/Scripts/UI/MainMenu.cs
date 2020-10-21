using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;
    public GameObject quitButton;
    public GameObject healthIndicator;

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

    public void QuitGame()
    {
        Destroy(healthIndicator);
        Application.Quit();
    }
}
