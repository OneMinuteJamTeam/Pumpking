using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class Pumpkin : Player
{
   
=======
public class Pumpkin : Player {

    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashDuration = 0.5f;

    protected override void UseAbility() {
        CustomLog.Log(CustomLog.CustomLogType.PLAYER, "Ability used");
        canUseAblity = false;
        readInput = false;

        _rb.velocity = transform.forward * dashSpeed;

        StartCoroutine(EndDash());
    }

    private IEnumerator EndDash() {
        yield return new WaitForSeconds(dashDuration);
        readInput = true;
        canUseAblity = true;
    }
>>>>>>> Stashed changes
}
