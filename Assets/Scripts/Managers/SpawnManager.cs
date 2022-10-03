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

    private GameObject _pumpkinObj = null;
    private GameObject _scarecrowObj = null;

    public void SpawnPlayers()
    {
        // Destroy prev players if any
        if(_pumpkinObj != null)
            Destroy(_pumpkinObj);
        if(_scarecrowObj != null)
            Destroy(_scarecrowObj);

        // Read who is escaping
        bool isPumpkinEscpaing = PlayerPrefs.GetInt("PumpkinEscaping") == 1? true : false;
        bool isScarecrowEscpaing = PlayerPrefs.GetInt("ScarecrowEscaping") == 1 ? true : false;

        // Spawn
        if (isPumpkinEscpaing)
            _pumpkinObj = Instantiate(pumpkinPrefab, new Vector3(escapeeSpawnPoint.position.x,pumpkinPrefab.transform.position.y, escapeeSpawnPoint.transform.position.z), Quaternion.identity);
        else
            _pumpkinObj = Instantiate(pumpkinPrefab, new Vector3(chaserSpawnPoint.position.x, pumpkinPrefab.transform.position.y, chaserSpawnPoint.transform.position.z), Quaternion.identity);

        if (isScarecrowEscpaing)
            _scarecrowObj = Instantiate(scarecrowPrefab, new Vector3(escapeeSpawnPoint.position.x, scarecrowPrefab.transform.position.y, escapeeSpawnPoint.transform.position.z), Quaternion.identity);
        else
            _scarecrowObj = Instantiate(scarecrowPrefab, new Vector3(chaserSpawnPoint.position.x, scarecrowPrefab.transform.position.y, chaserSpawnPoint.transform.position.z), Quaternion.identity);

        // Escaping set
        _pumpkinObj.GetComponent<Pumpkin>().SetIsEscaping(isPumpkinEscpaing);
        _scarecrowObj.GetComponent<Scarecrow>().SetIsEscaping(isScarecrowEscpaing);
    }

    private void Start()
    {
        SpawnPlayers();
    }
}
