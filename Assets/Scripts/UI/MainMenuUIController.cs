using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : Singleton<MainMenuUIController>
{
    public void LoadLevel(string levelName) {
        CustomSceneManager.Instance.LoadScene(levelName);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
