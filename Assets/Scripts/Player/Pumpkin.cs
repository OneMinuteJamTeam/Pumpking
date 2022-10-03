using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class Pumpkin : Player {

    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashDuration = 0.5f;
    private bool dashActive = false;

    public override void SetIsEscaping(bool isEscaping)
    {
        base.SetIsEscaping(isEscaping);
        PlayerPrefs.SetInt("PumpkinEscaping", isEscaping ? 1 : 0);
    }

    protected override void UseAbility() 
    {
        if (CanMove) {
            CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Ability used");
            CanReadInput = false;
            dashActive = true;
            HandleSlow();
            StartCoroutine(EndDash());
        }
    }

    protected override void HandleInput() {
        if (dashActive) {
            rb.velocity = transform.forward * dashSpeed;
        }
        base.HandleInput();
    }



    private IEnumerator EndDash() 
    {
        yield return new WaitForSeconds(dashDuration);
        dashActive = false;
        CanReadInput = true;
    }
}
