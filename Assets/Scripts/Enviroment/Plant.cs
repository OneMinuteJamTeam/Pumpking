using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{

    [SerializeField] float rootTime = 1f;
    [SerializeField] float reactivateTime = 2f;

    [SerializeField] Material activeMaterial;
    [SerializeField] Material unactiveMaterial;

    private Player rootedPlayer = null;
    private bool isActive = true;

    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = activeMaterial;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player" && rootedPlayer == null && isActive) {
            Player otherPlayer = other.gameObject.GetComponent<Player>();
            rootedPlayer = otherPlayer;
            otherPlayer.CanMove = false;
            StartCoroutine(freePlayer());
        }
    }

    private IEnumerator freePlayer() {
        yield return new WaitForSeconds(rootTime);
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
