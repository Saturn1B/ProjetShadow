using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel, NewGamePanel, SettingsPanel, CurrentPanel, GraphicsPanel, VolumesPanel, ControlPanel;
    public Button LoadGameButton;
    public bool hasSave;

    public GameObject controlSettings;

    private void Awake()
    {
        controlSettings = GameObject.Find("ControlSettings");

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
            DontDestroyOnLoad(controlSettings);
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
        DontDestroyOnLoad(controlSettings);
        SceneManager.LoadScene(1);
    }

    public void Settings()
    {
        MainMenuPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CurrentPanel = SettingsPanel;
    }

    public void Graphics()
    {
        SettingsPanel.SetActive(false);
        GraphicsPanel.SetActive(true);
        CurrentPanel = GraphicsPanel;
    }

    public void Volumes()
    {
        SettingsPanel.SetActive(false);
        VolumesPanel.SetActive(true);
        CurrentPanel = VolumesPanel;
    }

    public void Control()
    {
        SettingsPanel.SetActive(false);
        ControlPanel.SetActive(true);
        CurrentPanel = ControlPanel;
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
        CurrentPanel = MainMenuPanel;
    }

    public void Back2()
    {
        CurrentPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CurrentPanel = SettingsPanel;
    }
}
