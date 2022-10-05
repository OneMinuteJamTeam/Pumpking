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
    [SerializeField] TrailRenderer chaserTrailRenderer;
    [SerializeField] TrailRenderer escapeeTrailRenderer;
    [SerializeField] GameObject pumpkinRagdollPref;
    [SerializeField] GameObject scarecrowRagdollPref;

    [Header("Audio")]
    [SerializeField]
    private AK.Wwise.Event pumpkinAbilityEvent;

    TrailRenderer trailRenderer;

    private bool dashActive = false;
    protected override void Awake() {
        base.Awake();
        chaserTrailRenderer.emitting = escapeeTrailRenderer.emitting = false;
    }

    public override void SetIsEscaping(bool isEscaping) {
        base.SetIsEscaping(isEscaping);
        PlayerPrefs.SetInt("PumpkinEscaping", isEscaping ? 1 : 0);
        if (isEscaping)
            trailRenderer = escapeeTrailRenderer;
        else
            trailRenderer = chaserTrailRenderer;
    }

    protected override void UseAbility() {
        if (CanMove) {

            // Audio
            pumpkinAbilityEvent.Post(this.gameObject);

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


    private void OnCollisionEnter(Collision collision) {
    EndDash();
    if (collision.collider.tag.Equals("Player")) {
            if (!IsEscaping)
                GameController.Instance.GivePoint(((int)PlayerNumber.PlayerOne));
            else
                GameController.Instance.GivePoint((int)PlayerNumber.PlayerTwo);
  
    }
}
}
