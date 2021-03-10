using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject HUDPanel, PausePanel, SettingsPanel, ControlPanel, GraphicsPanel, VolumesPanel, WarningPanel, CurrentPanel, controlSettings;

    public Life playerLife;

    private void Start()
    {
        controlSettings = GameObject.Find("ControlSettings");
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.isPressed)
        {
            HUDPanel.SetActive(false);
            PausePanel.SetActive(true);
            playerLife.isPaused = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        HUDPanel.SetActive(true);
        PausePanel.SetActive(false);
        playerLife.isPaused = false;
        Time.timeScale = 1;
    }

    public void Warning()
    {
        WarningPanel.SetActive(true);
        PausePanel.SetActive(false);
        CurrentPanel = WarningPanel;
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
        PausePanel.SetActive(false);
        CurrentPanel = SettingsPanel;
    }

    public void Controls()
    {
        ControlPanel.SetActive(true);
        PausePanel.SetActive(false);
        CurrentPanel = ControlPanel;
    }

    public void Graphics()
    {
        GraphicsPanel.SetActive(true);
        PausePanel.SetActive(false);
        CurrentPanel = GraphicsPanel;
    }

    public void Volumes()
    {
        VolumesPanel.SetActive(true);
        PausePanel.SetActive(false);
        CurrentPanel = VolumesPanel;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        DontDestroyOnLoad(controlSettings);
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        CurrentPanel.SetActive(false);
        PausePanel.SetActive(true);
        CurrentPanel = PausePanel;
    }

    public void Back2()
    {
        CurrentPanel.SetActive(false);
        SettingsPanel.SetActive(true);
        CurrentPanel = SettingsPanel;
    }
}
