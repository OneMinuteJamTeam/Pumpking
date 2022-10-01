using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    [Header("References")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gamePanel;

    public void OnResumePressed() {
        GameController.Instance.Pause();
    }
    public void OnReturnToMainMenuPressed() {
        CustomSceneManager.Instance.LoadScene("MainMenu");
    }

    //============================================= panels handling

    public void ShowPausePanel(bool active) {
        pausePanel.SetActive(active);
    }
    public void ShowGameOverPanel(bool active) {
        gameOverPanel.SetActive(active);
    }

    public void ShowGamePanel(bool active) {
        gamePanel.SetActive(active);
    }
}

