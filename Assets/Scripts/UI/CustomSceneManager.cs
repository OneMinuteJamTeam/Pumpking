using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : SingletonDontDest<CustomSceneManager>
{
    public delegate void LoadSceneDeleg();

    public void LoadScene(string sceneToLoad) 
    {
        LoadSceneDeleg f = () => {
            SceneManager.LoadScene(sceneToLoad);
            GameController.Instance.UnpauseGame();
        };

        if(FadePanel.Instance != null)
            StartCoroutine(FadePanel.Instance.fadeIn(f));
        else
            f();
    }
}