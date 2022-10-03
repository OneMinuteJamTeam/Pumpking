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
    private Transform P1SpawnPos;
    [SerializeField]
    private Transform P2SpawnPos;

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
        _pumpkinObj = Instantiate(pumpkinPrefab, new Vector3(P1SpawnPos.position.x, pumpkinPrefab.transform.position.y, P1SpawnPos.transform.position.z), Quaternion.identity);
        _scarecrowObj = Instantiate(scarecrowPrefab, new Vector3(P2SpawnPos.position.x, scarecrowPrefab.transform.position.y, P2SpawnPos.position.z), Quaternion.identity);

        // Escaping set
        _pumpkinObj.GetComponent<Pumpkin>().SetIsEscaping(isPumpkinEscpaing);
        _scarecrowObj.GetComponent<Scarecrow>().SetIsEscaping(isScarecrowEscpaing);
    }

    private void Start()
    {
        SpawnPlayers();
    }
}
