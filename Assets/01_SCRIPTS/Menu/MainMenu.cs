using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel, NewGamePanel, SettingsPanel, CurrentPanel, GraphicsPanel, VolumesPanel, ControlPanel, LoadingPanel;
    public Button LoadGameButton;
    public Slider loadingSlider;
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
            MainMenuPanel.SetActive(false);
            DontDestroyOnLoad(controlSettings);
            //SceneManager.LoadScene(1);
            StartCoroutine(LoadAsync());
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
        MainMenuPanel.SetActive(false);
        DontDestroyOnLoad(controlSettings);
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadAsync());
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
        NewGamePanel.SetActive(false);
        DontDestroyOnLoad(controlSettings);
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadAsync());
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

    IEnumerator LoadAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        LoadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            yield return null;
        }
    }
}
