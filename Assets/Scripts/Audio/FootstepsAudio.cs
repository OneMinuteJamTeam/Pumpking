using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsAudio : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event footstepsEvent;

    public void TriggerEvent()
    {
        footstepsEvent.Post(this.transform.parent.gameObject);
    }
}
