using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> possibleObjects;

    private int chosenId;
    private GameObject generatedObject;
     
    private void Start() {

        foreach(GameObject obj in possibleObjects) {
            obj.SetActive(false);
        }
        chosenId = Random.Range(0, possibleObjects.Count);
    }

    public void Spawn() {
        if(generatedObject)Destroy(generatedObject);
        generatedObject = Instantiate(possibleObjects[chosenId]);
        generatedObject.SetActive(true);
    }
}
