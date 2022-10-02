using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonDontDest<GameController>
{

    public int Player1Points { get; private set; } = 0;
    public int Player2Points { get; private set; } = 0;

    private bool isPause = false;
    
    private int round = 1;

    public void GivePoint(int _player) {
        if (_player == 1) Player1Points++;
        else if (_player == 2) Player2Points++;
        else Debug.LogError("GameController: assigned point to player " + _player + ", that doesn't exist");

        if(round == 1) {
            round++;
            SwapRoles();
        }
        else if (round == 2) {
            CustomSceneManager.Instance.LoadScene("Results");
        }
    }

    private void SwapRoles() {
        //TODO
    }

    //DELETE
    public void DebugPlayerWins(int playerWhoWins) {
        if (playerWhoWins == 0) {
            Player1Points = Player2Points = 0;
            CustomSceneManager.Instance.LoadScene("Results");
        }
        else if (playerWhoWins == 1)
            Player1Points = 10;
        else
            Player2Points = 10;

        CustomSceneManager.Instance.LoadScene("Results");
    }

    public void ResetPoints() {
        Player1Points = Player2Points = 0;
    }


    //======================================================== pause handling
    public void SwitchPause() {
        if (GameUIManager.Instance) {
            if (isPause)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void UnpauseGame() {
        if (isPause) {
            Time.timeScale = 1f;
            GameUIManager.Instance.ShowPausePanel(false);
            isPause = false;
        }
    }
    public void PauseGame() {
        if (!isPause) {
            Time.timeScale = 0;
            GameUIManager.Instance.ShowPausePanel(true);
            isPause = true;
        }
    }
}
