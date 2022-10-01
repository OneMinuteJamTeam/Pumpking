using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : Player {

    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    protected override void UseAbility() {
        
        readInput = false;

        _rb.velocity = transform.forward * dashSpeed;

        StartCoroutine(EndDash());
    }

    private IEnumerator EndDash() {
        yield return new WaitForSeconds(dashDuration);
        //_rb.velocity = Vector3.zero;
        readInput = true;
    }
}
