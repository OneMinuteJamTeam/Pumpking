using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    private int chosenId;
    private GameObject generatedObject;
     
    private void Start() {

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        chosenId = Random.Range(0, transform.childCount);
    }

    public void Spawn() {
        if(generatedObject)Destroy(generatedObject);
        generatedObject = Instantiate(transform.GetChild(chosenId).gameObject);
        generatedObject.SetActive(true);
    }
}
