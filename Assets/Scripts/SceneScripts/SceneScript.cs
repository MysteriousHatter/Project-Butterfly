using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject instructionsPanel;
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("BaseLevel");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void HowToPlay()
    {
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
    }

    public void MenuPanel()
    {
        menuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
    }
}
