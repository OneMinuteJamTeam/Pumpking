using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Players Prefabs")]
    [SerializeField]
    private GameObject pumpkinPrefab;
    [SerializeField]
    private GameObject scarecrowPrefab;

    [Header("Spawn Points")]
    [SerializeField]
    private Transform escapeeSpawnPoint;
    [SerializeField]
    private Transform chaserSpawnPoint;

    private GameObject p1Obj = null;
    private GameObject p2Obj = null;

    public void SpawnPlayers()
    {
        // Destroy prev players if any
        if(p1Obj != null)
            Destroy(p1Obj);
        if(p2Obj != null)
            Destroy(p2Obj);

        // Read who is escaping
        bool isP1Escaping = PlayerPrefs.GetInt("P1Escaping") == 1? true : false;
        bool isP2Escaping = PlayerPrefs.GetInt("P2Escaping") == 1 ? true : false;

        bool isP1Pumpkin = PlayerPrefs.GetInt("P1Pumpkin") == 1 ? true : false;
        bool isP2Pumpkin = PlayerPrefs.GetInt("P2Pumpkin") == 1 ? true : false;

        Debug.Log("p1 escaping:" + isP1Escaping);
        Debug.Log("p1 pumpkin:" + isP1Pumpkin);

        Debug.Log("p2 escaping:" + isP2Escaping);
        Debug.Log("p2 pumpkin:" + isP2Pumpkin);

        // Prefab selection
        GameObject p1Prefab;
        GameObject p2Prefab;

        if (isP1Pumpkin)
            p1Prefab = pumpkinPrefab;
        else
            p1Prefab = scarecrowPrefab;
        if (isP2Pumpkin)
            p2Prefab = pumpkinPrefab;
        else
            p2Prefab = scarecrowPrefab;

        // Spawn

        if (isP1Escaping)
            p1Obj = Instantiate(p1Prefab, new Vector3(escapeeSpawnPoint.position.x, pumpkinPrefab.transform.position.y, escapeeSpawnPoint.transform.position.z), Quaternion.identity);
        else
            p1Obj = Instantiate(p1Prefab, new Vector3(chaserSpawnPoint.position.x, pumpkinPrefab.transform.position.y, chaserSpawnPoint.transform.position.z), Quaternion.identity);

        if (isP2Escaping)
            p2Obj = Instantiate(p2Prefab, new Vector3(escapeeSpawnPoint.position.x, scarecrowPrefab.transform.position.y, escapeeSpawnPoint.transform.position.z), Quaternion.identity);
        else
            p2Obj = Instantiate(p2Prefab, new Vector3(chaserSpawnPoint.position.x, scarecrowPrefab.transform.position.y, chaserSpawnPoint.transform.position.z), Quaternion.identity);

        // Escaping set
        //p1Obj.GetComponent<Player>().SetIsEscaping(isP1Escaping);
        //p2Obj.GetComponent<Player>().SetIsEscaping(isP2Escaping);

    }

    private void Start()
    {
        SpawnPlayers();
    }
}
