using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] Light playerLight;
    private Player.eRole role;
    public virtual void SetRole(Player.eRole _role) {
            role = _role;
            if(_role == Player.eRole.Escapee) {
                playerLight.color = ColorsManager.Instance.EscapeeLightColor;
                PlayerPrefs.SetInt(Player.P1_ESCAPEE_KEY,1);
                PlayerPrefs.SetInt(Player.P2_ESCAPEE_KEY,0);
            }
            else {
            playerLight.color = ColorsManager.Instance.ChaserLightColor;
            PlayerPrefs.SetInt(Player.P1_ESCAPEE_KEY, 0);
            PlayerPrefs.SetInt(Player.P2_ESCAPEE_KEY, 1);
        }
    }

    public Player.eRole GetRole() { return role; }

    public void SwapRole() {
        if (role == Player.eRole.Escapee) SetRole(Player.eRole.Chaser);
        else SetRole(Player.eRole.Escapee);
    }



}
