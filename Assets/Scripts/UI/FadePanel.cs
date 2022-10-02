using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : Singleton<FadePanel>
{
    private static float fadePanelSpeed = 50f;
    private static bool isActive = false;

    private Image panel;

    protected override void Awake() {
        base.Awake();
        panel = GetComponent<Image>();

        if (isActive) {
            panel.color = Color.black;
            StartCoroutine(fadeOut());
        }
    }

    public IEnumerator fadeIn(CustomSceneManager.LoadSceneDeleg f) 
    {
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0f);
        while (panel.color.a < 1) {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a + 0.1f);
            yield return new WaitForSecondsRealtime(1 / fadePanelSpeed);
        }
        isActive = true;
        f();
    }

    private IEnumerator fadeOut() 
    {
        while (panel.color.a > 0) {
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, panel.color.a - 0.1f);
            yield return new WaitForSecondsRealtime(1 / fadePanelSpeed);
        }
        isActive = false;
    }
}
