using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : MonoBehaviour
{
    [SerializeField] float slowAmmount = 2f;

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().SlowAmmount = slowAmmount;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Player>().SlowAmmount = 0;
        }
    }

}
