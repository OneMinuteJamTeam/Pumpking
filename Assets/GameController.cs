using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonDontDest<GameController>
{

    private bool isPause = false;

    public void SwitchPause() {
        if (GameUIManager.Instance) {
            if (isPause)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void UnpauseGame() {
        Time.timeScale = 1f;
        GameUIManager.Instance.ShowPausePanel(false);
        isPause = false;
    }
    public void PauseGame() {
        Time.timeScale = 0;
        GameUIManager.Instance.ShowPausePanel(true);
        isPause = true;
    }
}
