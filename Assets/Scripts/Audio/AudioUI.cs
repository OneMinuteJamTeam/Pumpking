using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AkGameObj))]
public class AudioUI : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event UIEvent1;
    [SerializeField]
    private AK.Wwise.Event UIEvent2;

    public void PlayAudio1()
    {
        UIEvent1.Post(this.gameObject);
    }

    public void PlayAudio2()
    {
        UIEvent2.Post(this.gameObject);
    }
}
