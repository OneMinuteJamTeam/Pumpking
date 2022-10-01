using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("pickable picked");
        Destroy(gameObject);
    }
}
