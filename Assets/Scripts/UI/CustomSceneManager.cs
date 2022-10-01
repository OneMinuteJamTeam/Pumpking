using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomSceneManager : SingletonDontDest<CustomSceneManager>
{

    public delegate void LoadSceneDeleg();

    public void LoadScene(string sceneToLoad) {
        //Debug.Log("load scene called");
        //if(FadePanel.Instance != null)
            StartCoroutine(FadePanel.Instance.fadeIn(() => { SceneManager.LoadScene(sceneToLoad); }));
    }
}