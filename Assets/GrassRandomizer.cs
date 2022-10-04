using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRandomizer : MonoBehaviour {

    [SerializeField] List<GameObject> generators;
    private List<GameObject> generatedObjects;

    public void RandomizeHeight(float randAmmount) {

        if(generatedObjects == null)
            generatedObjects = new List<GameObject>();  

        foreach (GameObject generator in generators) generator.SetActive(false);

        for (int i = 0; i < generatedObjects.Count; i++) {
            DestroyImmediate(generatedObjects[i]);
        }
        generatedObjects.Clear();

        for (int i = 0; i < generators.Count; i++) {
            GameObject obj = Instantiate(generators[i], generators[i].transform.position, generators[i].transform.rotation);
            float randomizedHeight = Random.Range(obj.transform.position.y, obj.transform.position.y + randAmmount);
            obj.transform.position = new Vector3(obj.transform.position.x, randomizedHeight, obj.transform.position.z);
            obj.SetActive(true);
            generatedObjects.Add(obj);
            obj.transform.parent = transform;
        }
    }
}
