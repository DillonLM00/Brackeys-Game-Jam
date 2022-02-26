using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject ingameUI;

    private void Start()
    {
        pausePanel.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            togglePauseScreen();
        }
    }

    public void togglePauseScreen()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        if (pausePanel.activeSelf)     // un- /pauses the game based on pauseMenuBackground.activeSelf, if (Time.timeScale == 1.0) can cause errors based on float comparisons
        {
            Time.timeScale = 0f;
            ingameUI.SetActive(false);
        }
        else
        {
            Time.timeScale = 1.0f;
            ingameUI.SetActive(true);
        }
    }
}
