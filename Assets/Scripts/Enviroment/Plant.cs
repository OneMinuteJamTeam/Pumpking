using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

    [SerializeField] float rootTime = 1f;
    [SerializeField] float reactivateTime = 2f;

    [SerializeField] Material activeMaterial;
    [SerializeField] Material unactiveMaterial;

    [SerializeField] private Animator animator;

    [Header("Audio")]
    [SerializeField] private AK.Wwise.Event plantSFX;

    private Player rootedPlayer = null;
    private bool isActive = true;

    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = activeMaterial;
    }

    private void Update() {
        if (rootedPlayer && isActive) {
            rootedPlayer.transform.position = Vector3.MoveTowards(rootedPlayer.transform.position, transform.position, 40 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && rootedPlayer == null && isActive) {

            // Audio
            plantSFX.Post(this.gameObject);

            Player otherPlayer = other.gameObject.GetComponent<Player>();
            rootedPlayer = otherPlayer;
            otherPlayer.CanMove = false;
            animator.SetBool("Enter", true);
            StartCoroutine(freePlayer());
        }
    }

    private IEnumerator freePlayer() {
        yield return new WaitForSeconds(rootTime / 2.0f);
        animator.SetBool("Enter", false);
        yield return new WaitForSeconds(rootTime / 2.0f);
        //animator.SetBool("Enter", false);
        isActive = false;
        meshRenderer.material = unactiveMaterial;
        rootedPlayer.CanMove = true;
        rootedPlayer = null;
        StartCoroutine(reactivate());
    }
    private IEnumerator reactivate() {
        yield return new WaitForSeconds(reactivateTime);
        isActive = true;
        meshRenderer.material = activeMaterial;
    }
}
