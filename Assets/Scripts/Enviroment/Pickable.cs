using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{

    [SerializeField] float boostAmmount = 5f;
    [SerializeField] float boostDuration = 2f;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().BoostSpeed(boostAmmount,boostDuration);
            Debug.Log("pickable picked");
            Destroy(gameObject);
        }
    }
}
