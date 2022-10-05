using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRoleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI pumpkinText;
    [SerializeField]
    private TextMeshProUGUI scarecrowText;
    [SerializeField]
    PumpkinRagdoll pumpkinRagdoll;
    [SerializeField]
    private TextMeshProUGUI P1Text;
    [SerializeField]
    private TextMeshProUGUI P2Text;
    [SerializeField]
    ScarecrowRagdoll scarecrowRagdoll;


    public void LoadScene(string sceneName)
    {
        CustomSceneManager.Instance.LoadScene(sceneName);
    }

    public void SwapRoles()
    {
        scarecrowRagdoll.SwapRole();
        pumpkinRagdoll.SwapRole();
        SetTexts();
    }

    private void Start()
    {
        SetTexts();
        pumpkinRagdoll.ShowCrown(false);
        scarecrowRagdoll.ShowCrown(false);
    }

    private void SetTexts()
    {
        Player.eRole prole = pumpkinRagdoll.GetRole();
        Player.eRole srole = scarecrowRagdoll.GetRole();

        pumpkinText.text = prole.ToString();
        scarecrowText.text = srole.ToString();

        if (prole == Player.eRole.Chaser) {
            pumpkinText.color = ColorsManager.Instance.ChaserUiColor; ;
            P1Text.color = ColorsManager.Instance.ChaserUiColor; ;
            scarecrowText.color = ColorsManager.Instance.EscapeeUiColor;
            P2Text.color = ColorsManager.Instance.EscapeeUiColor;
        }
        else {
            pumpkinText.color = ColorsManager.Instance.EscapeeUiColor;
            P1Text.color = ColorsManager.Instance.EscapeeUiColor;
            scarecrowText.color = ColorsManager.Instance.ChaserUiColor;
            P2Text.color = ColorsManager.Instance.ChaserUiColor;
        }
    }
}
