using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultRagdoll : MonoBehaviour
{
    [SerializeField]
    private GameObject crownObj;

    public void SetActiveCrown(bool active)
    {
        crownObj.SetActive(active);
    }
}
