using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : SingletonDontDest<CustomSceneManager>
{

    public delegate void LoadSceneDeleg();

    public void LoadScene(string sceneToLoad) {
        StartCoroutine(UIController.Instance.fadePanelAnimation(() => { SceneManager.LoadScene(sceneToLoad); }));
    }
}