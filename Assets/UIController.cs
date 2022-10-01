using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIController : Singleton<UIController>
{
    [Header("References")]
    [SerializeField] Image fadePanel;

    [Header("Settings")]
    [SerializeField] float fadePanelSpeed;

    public IEnumerator fadePanelAnimation(CustomSceneManager.LoadSceneDeleg f) {
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0);
        while (fadePanel.color.a < 1) {
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, fadePanel.color.a + 0.1f);
            yield return new WaitForSeconds(1 / fadePanelSpeed);
        }
        f();
        while (fadePanel.color.a > 0) {
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, fadePanel.color.a - 0.1f);
            yield return new WaitForSeconds(1 / fadePanelSpeed);
        }
    }


}
