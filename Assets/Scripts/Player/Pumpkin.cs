using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class Pumpkin : Player {

    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashDuration = 0.5f;

    public override void SetIsEscaping(bool isEscaping)
    {
        base.SetIsEscaping(isEscaping);
        PlayerPrefs.SetInt("PumpkinEscaping", isEscaping ? 1 : 0);
    }

    protected override void UseAbility() 
    {
        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Ability used");
        CanReadInput = false;

        rb.velocity = transform.forward * dashSpeed;

        StartCoroutine(EndDash());
    }

    private IEnumerator EndDash() 
    {
        yield return new WaitForSeconds(dashDuration);

        CanReadInput = true;
    }
}
