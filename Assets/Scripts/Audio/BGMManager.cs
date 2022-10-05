using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : SingletonDontDest<BGMManager>
{
    [Header("BGM Music")]
    [SerializeField]
    private AK.Wwise.Event mainMenuEvent;
    [SerializeField]
    private AK.Wwise.Event firstGameEvent;
    [SerializeField]
    private AK.Wwise.Event secondGameEvent;
    [SerializeField]
    private AK.Wwise.Event resultEvent;

    [Header("SFX")]
    [SerializeField]
    private AK.Wwise.Event sfxSwapRole;
    // Add other events here

    private uint currentEventID;

    public void PlaySFXSwapRole()
    {
        firstGameEvent.Stop(this.gameObject);
        sfxSwapRole.Post(this.gameObject);
    }

    public void PlaySecondHalfTheme()
    {
        currentEventID = secondGameEvent.Post(this.gameObject);
    }

    private void Start()
    {
        currentEventID = mainMenuEvent.Post(this.gameObject);
    }

    private void Update()
    {
        CheckScene();
    }

    private void CheckScene()
    {
        if(SceneManager.GetActiveScene().name.Equals("Final Scene") && currentEventID == mainMenuEvent.PlayingId)
        {
            currentEventID = firstGameEvent.Post(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name.Equals("Results") && currentEventID == secondGameEvent.PlayingId)
        {
            currentEventID = resultEvent.Post(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name.Equals("RoleSelection") && currentEventID == resultEvent.PlayingId)
        {
            currentEventID = mainMenuEvent.Post(this.gameObject);
        }
    }
}
