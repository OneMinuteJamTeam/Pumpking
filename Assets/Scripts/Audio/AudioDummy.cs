using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDummy : MonoBehaviour
{
    private float _timer = 0;
    // AkGameObject needed
    /*
    [SerializeField]
    private AK.Wwise.Event testEvent;
    */
    private void Start()
    {
        StartCoroutine(COTimer());
    }

    private void Update()
    {

        if (_timer >= 30)
            _timer = 30;
    }

    private IEnumerator COTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _timer++;
            if(_timer == 10)
            {
                Debug.Log("Play");
                //AkSoundEngine.PostEvent("Bullet",gameObject);
              //  testEvent.Post(gameObject);
            }


            //AkSoundEngine.SetRTPCValue("timer", _timer);
            Debug.Log(_timer);
        }
       
    }
}
