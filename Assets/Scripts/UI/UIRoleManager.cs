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
        pumpkinText.text = pumpkinRagdoll.GetRole().ToString();
        scarecrowText.text = scarecrowRagdoll.GetRole().ToString();
    }
}
