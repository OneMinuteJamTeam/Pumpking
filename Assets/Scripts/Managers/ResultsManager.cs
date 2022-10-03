using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class ResultsManager : Singleton<ResultsManager>
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] Transform P1position;
    [SerializeField] Transform P2position;
    [SerializeField] GameObject PumpkinPrefab;
    [SerializeField] GameObject ScareCrowPrefab;

    private void Start() {

        GameObject p1 = Instantiate(PumpkinPrefab, P1position.position, Quaternion.identity);
        GameObject p2 = Instantiate(ScareCrowPrefab, P2position.position, Quaternion.identity);

        Vector3 rot = p1.transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        p1.transform.rotation = p2.transform.rotation = Quaternion.Euler(rot);

        if (PlayerPrefs.GetInt("P1Points") > PlayerPrefs.GetInt("P2Points")) 
        {
            winText.text = "Player 1 wins!";
            StartCoroutine(winAnimation(p1));

            p2.GetComponent<Rigidbody>().useGravity = false;

            Vector3 r = p2.transform.rotation.eulerAngles;
            r = new Vector3(r.x, r.y, r.z + 90);
            p2.transform.rotation = p2.transform.rotation = Quaternion.Euler(r);
        }
        else if (PlayerPrefs.GetInt("P1Points") < PlayerPrefs.GetInt("P2Points")) 
        {
            winText.text = "Player 2 wins!";
            StartCoroutine(winAnimation(p2));

            p1.GetComponent<Rigidbody>().useGravity = false;
            Vector3 r = p1.transform.rotation.eulerAngles;
            r = new Vector3(r.x, r.y, r.z +90);
            p1.transform.rotation = p1.transform.rotation = Quaternion.Euler(r);
        }
        else 
        {
            winText.text = "Draw!";
            p1.GetComponent<Rigidbody>().useGravity = p2.GetComponent<Rigidbody>().useGravity = false;
        }
       
    }

    private IEnumerator winAnimation(GameObject obj) 
    {
        float originalY = obj.transform.position.y;
        while (true) 
        {
            if (obj.transform.position.y < originalY) obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
