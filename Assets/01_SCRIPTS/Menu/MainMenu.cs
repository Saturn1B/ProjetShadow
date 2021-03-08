using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel, NewGamePanel, SettingsPanel, CurrentPanel;
    public Button LoadGameButton;
    public bool hasSave;


    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetFloat("X"));
        if(PlayerPrefs.GetFloat("X") == 0)
        {
            LoadGameButton.interactable = false;
            hasSave = false;
        }
        else
        {
            LoadGameButton.interactable = true;
            hasSave = true;
        }

        CurrentPanel = MainMenuPanel;
    }

    public void NewGame()
    {
        if (!hasSave)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            MainMenuPanel.SetActive(false);
            NewGamePanel.SetActive(true);
            CurrentPanel = NewGamePanel;
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void Back()
    {
        CurrentPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }
}
