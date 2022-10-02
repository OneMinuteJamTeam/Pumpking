using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRoleManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI p1RoleText;
    [SerializeField]
    private TextMeshProUGUI p2RoleText;

    [SerializeField]
    private GameObject pumpkinModel;
    [SerializeField]
    private GameObject scarecrowModel;
    [SerializeField]
    private Transform p1ModelPosition;
    [SerializeField]
    private Transform p2ModelPosition;


    public void LoadScene(string sceneName)
    {
        CustomSceneManager.Instance.LoadScene(sceneName);
    }

    public void SwapRoles()
    {
        RoleSwapper.Instance.SwapRoles();
        SetRoleTexts();
    }
    public void SwapClasses() {
        RoleSwapper.Instance.SwapClasses();
        SetClassesModels();
    }

    private void Start()
    {
        SetRoleTexts();
        SetClassesModels();
    }

    private void SetRoleTexts()
    {
        p1RoleText.text = RoleSwapper.Instance.P1Role.ToString();
        p2RoleText.text = RoleSwapper.Instance.P2Role.ToString();
    }
    private void SetClassesModels() {
        if(RoleSwapper.Instance.P1Class == RoleSwapper.GameClass.Pumpkin) {
            pumpkinModel.transform.position = p1ModelPosition.position;
            scarecrowModel.transform.position = p2ModelPosition.position;
        }
        else if(RoleSwapper.Instance.P1Class == RoleSwapper.GameClass.Scarecrow) {
            scarecrowModel.transform.position = p1ModelPosition.position;
            pumpkinModel.transform.position = p2ModelPosition.position;
        }
    }
}
