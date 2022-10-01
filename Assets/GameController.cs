using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonDontDest<GameController>
{

    private bool isPause = false;
    public void Pause() {
        if (GameUIManager.Instance) {
            if (isPause) {
                Time.timeScale = 1f;
                GameUIManager.Instance.ShowPausePanel(false);
                isPause = false;
            }
            else {
                Time.timeScale = 0;
                GameUIManager.Instance.ShowPausePanel(true);
                isPause = true;
            }
        }
    }
}
