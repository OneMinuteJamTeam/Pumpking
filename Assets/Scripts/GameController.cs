using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonDontDest<GameController>
{
    public int Player1Points { get; private set; } = 0;
    public int Player2Points { get; private set; } = 0;

    private bool _isPause = false;
    
    private int _round = 1;

    private void Update()
    {
        if(InputManager.Instance.GetApplicationPausePressed() && !_isPause)
            PauseGame();
    }

    public void GivePoint(int _player) 
    {
        if (_player == 0)
            Player1Points++;
        else if (_player == 1)
            Player2Points++;
        else 
            Debug.LogError("GameController: assigned point to player " + _player + ", that doesn't exist");

        if(_round == 1) 
        {
            _round++;
            SwapRoles();
        }
        else if (_round == 2) 
        {
            CustomSceneManager.Instance.LoadScene("Results");
        }
    }

    // TO-DO: DELETE
    public void DebugPlayerWins(int playerWhoWins) 
    {
        if (playerWhoWins == 0) 
        {
            Player1Points = Player2Points = 0;
            CustomSceneManager.Instance.LoadScene("Results");
        }
        else if (playerWhoWins == 1)
            Player1Points = 10;
        else
            Player2Points = 10;

        CustomSceneManager.Instance.LoadScene("Results");
    }

    public void ResetPoints()
    {
        Player1Points = Player2Points = 0;
    }

    private void SwapRoles()
    {
        // TO-DO
    }

    #region Pause Handling
    public void SwitchPause() {
        if (_isPause)
            UnpauseGame();
        else
            PauseGame();
    }

    public void UnpauseGame() {
        if (_isPause)
        {
            Time.timeScale = 1f;
            GameUIManager.Instance.ShowPausePanel(false);
            _isPause = false;
        }
    }
    public void PauseGame() {
        if (!_isPause) 
        {
            Time.timeScale = 0;
            GameUIManager.Instance.ShowPausePanel(true);
            _isPause = true;
        }
    }
    #endregion
}
