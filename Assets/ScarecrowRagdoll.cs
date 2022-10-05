using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowRagdoll : Ragdoll
{
    [SerializeField] MeshRenderer lanternMeshRenderer;
    [SerializeField] Material chasingMaterial;
    [SerializeField] Material escapingMaterial;

    private void Awake() {
        SetRole(Player.eRole.Escapee);
    }

    public override void SetRole(Player.eRole _role) {

        base.SetRole(_role);
        bool isEscaping = _role == Player.eRole.Escapee;

        PlayerPrefs.SetInt(Player.P1_ESCAPEE_KEY, isEscaping ? 1 : 0);
        if (isEscaping) {
            lanternMeshRenderer.material = escapingMaterial;
        }
        else {
            lanternMeshRenderer.material = chasingMaterial;
        }

    }
}
