using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinRagdoll : Ragdoll
{
    private void Awake() {
        SetRole(Player.eRole.Chaser);
    }

    public override void SetRole(Player.eRole _role) {
        base.SetRole(_role);
        PlayerPrefs.SetInt(Player.P1_ESCAPEE_KEY, _role == Player.eRole.Escapee ? 1 : 0);
    }
}
