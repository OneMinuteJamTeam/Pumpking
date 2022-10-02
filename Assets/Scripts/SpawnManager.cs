using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
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

    public void SpawnPlayers()
    {
        // Read who is escaping
        bool isPumpkinEscpaing = PlayerPrefs.GetInt("PumpkinEscaping") == 1? true : false;
        bool isScarecrowEscpaing = PlayerPrefs.GetInt("ScarecrowEscaping") == 1 ? true : false;

        // Spawn
        GameObject pumpkinObj = null;
        GameObject scarecrowObj = null;

        if (isPumpkinEscpaing)
            pumpkinObj = Instantiate(pumpkinPrefab, new Vector3(escapeeSpawnPoint.position.x,pumpkinPrefab.transform.position.y, escapeeSpawnPoint.transform.position.z), Quaternion.identity);
        else
            pumpkinObj = Instantiate(pumpkinPrefab, new Vector3(chaserSpawnPoint.position.x, pumpkinPrefab.transform.position.y, chaserSpawnPoint.transform.position.z), Quaternion.identity);

        if (isScarecrowEscpaing)
            scarecrowObj = Instantiate(scarecrowPrefab, new Vector3(escapeeSpawnPoint.position.x, scarecrowPrefab.transform.position.y, escapeeSpawnPoint.transform.position.z), Quaternion.identity);
        else
            scarecrowObj = Instantiate(scarecrowPrefab, new Vector3(chaserSpawnPoint.position.x, pumpkinPrefab.transform.position.y, chaserSpawnPoint.transform.position.z), Quaternion.identity);

        // Escaping set
        pumpkinObj.GetComponent<Pumpkin>().SetIsEscaping(isPumpkinEscpaing);
        scarecrowObj.GetComponent<Scarecrow>().SetIsEscaping(isScarecrowEscpaing);
    }

    private void Start()
    {
        SpawnPlayers();
    }
}
