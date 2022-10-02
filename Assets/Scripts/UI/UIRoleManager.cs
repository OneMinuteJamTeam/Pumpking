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

    public void LoadScene(string sceneName)
    {
        CustomSceneManager.Instance.LoadScene(sceneName);
    }

    public void SwapRoles()
    {
        RoleSwapper.Instance.SwapRoles();
        SetTexts();
    }

    private void Start()
    {
        SetTexts();
    }

    private void SetTexts()
    {
        pumpkinText.text = RoleSwapper.Instance.PumpkinRole.ToString();
        scarecrowText.text = RoleSwapper.Instance.ScarecrowRole.ToString();
    }
}
