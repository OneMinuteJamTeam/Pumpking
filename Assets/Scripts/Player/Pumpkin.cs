using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class Pumpkin : Player {
    [Header("Dash settings")]
    [SerializeField] float dashSpeed = 100f;
    [SerializeField] float dashDuration = 0.5f;
    [SerializeField] bool disapearOnDash = true;

    [Header("References")]
    [SerializeField] TrailRenderer trailRenderer;
    [SerializeField] GameObject model;


    private bool dashActive = false;

    protected override void Awake() {
        base.Awake();
        trailRenderer.emitting = false;
    }

    public override void SetIsEscaping(bool isEscaping) {
        base.SetIsEscaping(isEscaping);
        PlayerPrefs.SetInt("PumpkinEscaping", isEscaping ? 1 : 0);
    }

    protected override void UseAbility() {
        if (CanMove) {
            dashActive = true;
            trailRenderer.emitting = true;
            if (disapearOnDash) model.SetActive(false);
            CanReadInput = false;

            rb.velocity = transform.forward * dashSpeed;

            StartCoroutine(EndDashCor());
        }
    }

    private IEnumerator EndDashCor() {
        yield return new WaitForSeconds(dashDuration);
        EndDash();
    }
    private void EndDash() {
        if (dashActive) {
            if (disapearOnDash) model.SetActive(true);
            CanReadInput = true;
            trailRenderer.emitting = false;
        }
    }

    protected override void OnCollisionEnter(Collision collision) {
        EndDash();
        base.OnCollisionEnter(collision);
    }
}
