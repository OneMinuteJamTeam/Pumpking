using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameUIManager : Singleton<GameUIManager>
{
    [Header("References")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] MyUIBar p1CooldownBar;
    [SerializeField] MyUIBar p2CooldownBar;
    [SerializeField] SwapPanel swapPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI swapScoreText;
    [SerializeField] TextMeshProUGUI pumpkinRoleText;
    [SerializeField] TextMeshProUGUI scarecrowRoleText;

    private void Start()
    {
        UpdateRolesText();
    }

    public void PlaySwapPanel()
    {
        swapPanel.gameObject.SetActive(true);
        swapPanel.Play();
    }
    public void OnResumePressed() {
        GameController.Instance.UnpauseGame();
        GameController.Instance.SwitchPause();
    }
    public void OnReturnToMainMenuPressed() {
        GameController.Instance.UnpauseGame();
        CustomSceneManager.Instance.LoadScene("MainMenu");
    }

    public void DebugOnPlayerWins(int p) {
        GameController.Instance.DebugPlayerWins(p);
    }

    public MyUIBar GetCooldownBar(Player.PlayerNumber p) {
        if (p == Player.PlayerNumber.PlayerOne) return p1CooldownBar;
        else return p2CooldownBar;
    }

    public void UpdateRolesText()
    {
        Pumpkin p = FindObjectOfType<Pumpkin>();
        if (p.IsEscaping)
        {
            pumpkinRoleText.text = "Escapee";
            scarecrowRoleText.text = "Chaser";
        }
        else
        {
            pumpkinRoleText.text = "Chaser";
            scarecrowRoleText.text = "Escapee";
        }
    }

    #region Panels Handling

    public void ShowPausePanel(bool active) 
    {
        Debug.Log("show pause panel called: "+active);
        pausePanel.SetActive(active);
    }

    public void ShowGamePanel(bool active) 
    {
        gamePanel.SetActive(active);
    }

    public void SetScoreText(int p1Score,int p2Score) {
        scoreText.text = p1Score.ToString()+" - "+p2Score.ToString();
        swapScoreText.text = p1Score.ToString() + " - " + p2Score.ToString();
    }

    #endregion

}

