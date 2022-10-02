using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    [Header("References")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] MyUIBar p1CooldownBar;
    [SerializeField] MyUIBar p2CooldownBar;

    public void OnResumePressed() {
        GameController.Instance.SwitchPause();
    }
    public void OnReturnToMainMenuPressed() {
        CustomSceneManager.Instance.LoadScene("MainMenu");
    }

    public void DebugOnPlayerWins(int p) {
        GameController.Instance.DebugPlayerWins(p);
    }

    public MyUIBar GetCooldownBar(Player.PlayerNumber p) {
        if (p == Player.PlayerNumber.PlayerOne) return p1CooldownBar;
        else return p2CooldownBar;
    }

    #region Panels Handling

    public void ShowPausePanel(bool active) 
    {
        Debug.Log("show pause panel called: "+active);
        pausePanel.SetActive(active);
    }
    public void ShowGameOverPanel(bool active) 
    {
        gameOverPanel.SetActive(active);
    }

    public void ShowGamePanel(bool active) 
    {
        gamePanel.SetActive(active);
    }

    #endregion

}

