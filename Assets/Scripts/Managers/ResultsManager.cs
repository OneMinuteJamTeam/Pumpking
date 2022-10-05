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
    [SerializeField] GameObject pumpkinRagdoll;
    [SerializeField] GameObject scareCrowRagdoll;
    [SerializeField] GameObject pumpkinRagdollDead;
    [SerializeField] GameObject scareCrowRagdollDead;
    [SerializeField] TextMeshProUGUI resultScoreText;

    private void Start() {

        int p1Points = PlayerPrefs.GetInt("P1Points");
        int p2Points = PlayerPrefs.GetInt("P2Points");

        resultScoreText.text = p1Points.ToString() + " - " + p2Points.ToString();

        if (p1Points > p2Points) 
        {
            pumpkinRagdoll.SetActive(true);
            StartCoroutine(winAnimation(pumpkinRagdoll));
            scareCrowRagdollDead.SetActive(true);

            pumpkinRagdoll.GetComponent<ResultRagdoll>().SetActiveCrown(true);

            winText.text = "Player 1 wins!";
        }
        else if (PlayerPrefs.GetInt("P1Points") < PlayerPrefs.GetInt("P2Points")) 
        {
            scareCrowRagdoll.SetActive(true);
            StartCoroutine(winAnimation(scareCrowRagdoll));
            pumpkinRagdollDead.SetActive(true);

            scareCrowRagdoll.GetComponent<ResultRagdoll>().SetActiveCrown(true);

            winText.text = "Player 2 wins!";
        }
        else 
        {
            winText.text = "Draw!";
            scareCrowRagdoll.SetActive(true);
            pumpkinRagdoll.SetActive(true);
        }
       
    }

    private IEnumerator winAnimation(GameObject obj) 
    {
        Debug.Log("coroutine started");
        obj.GetComponent<Rigidbody>().useGravity = true;
        float originalY = obj.transform.position.y;
        while (true) 
        {
            Debug.Log("loop running");
            if (obj.transform.position.y < originalY) obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
