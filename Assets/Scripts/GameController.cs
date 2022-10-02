using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xPoke.CustomLog;

public class GameController : Singleton<GameController>
{
    public int Player1Points { get; private set; } = 0;
    public int Player2Points { get; private set; } = 0;

    [Header("References")]
    [SerializeField]
    private Timer timer;

    private bool _isPause = false;
    
    private int _round = 1;

    private Player _p1Obj;
    private Player _p2Obj;
    private bool _rolesSwapped = false;
    private bool _roundOver = false;

    private void Start()
    {
        ResetPoints();
        StartCoroutine(COGetPlayerRef());
        timer.StartTimerAt(60, true);
    }

    private void Update()
    {
        if(InputManager.Instance.GetApplicationPausePressed())
            SwitchPause();

        if(timer.CurrentTimer >=30 && timer.CurrentTimer < 31 && !_rolesSwapped)
            GivePointToEscapee();
        
        if(timer.CurrentTimer <= 0 && !_roundOver)
            GivePointToEscapee();
    }

    public void GivePoint(int _player) 
    {
        if (_player == 0)
            Player1Points++;
        else if (_player == 1)
            Player2Points++;
        else 
            Debug.LogError("GameController: assigned point to player " + _player + ", that doesn't exist");

        PlayerPrefs.SetInt("P1Points",Player1Points);
        PlayerPrefs.SetInt("P2Points",Player2Points);

        Debug.Log("P1 "+Player1Points + " - P2 " + Player2Points);

        if (_round == 1) 
        {
            _round++;
            SwapRoles();
        }
        else if (_round == 2) 
        {
            _roundOver = true;
            CustomSceneManager.Instance.LoadScene("Results");
        }
    }

    // TO-DO: DELETE
    public void DebugPlayerWins(int playerWhoWins) 
    {
        if (playerWhoWins == 0) 
        {
            Player1Points = Player2Points = 0;
            CustomSceneManager.Instance.LoadScene("Results");
        }
        else if (playerWhoWins == 1)
            Player1Points = 10;
        else
            Player2Points = 10;

        CustomSceneManager.Instance.LoadScene("Results");
    }

    public void ResetPoints()
    {
        PlayerPrefs.DeleteKey("P1Points");
        PlayerPrefs.DeleteKey("P2Points");
        Player1Points = Player2Points = 0;
        _round = 1;
        _roundOver = false;
    }

    private void SwapRoles()
    {
        CustomLog.Log(CustomLog.CustomLogType.GAMEPLAY, "ROLES SWAP");
        _rolesSwapped = true;
        
        timer.StopTimer();
        timer.StartTimerAt(30, true);

        _p1Obj.CanReadInput = false;
        _p2Obj.CanReadInput = false;

        Destroy(_p1Obj.gameObject);
        Destroy(_p2Obj.gameObject);

        StartCoroutine(COSwap());

        timer.ResumeTimer();
    }

    private void GivePointToEscapee()
    {
        if (_p1Obj.IsEscaping)
            GivePoint((int)_p1Obj.GetPlayerNumber());
        else if (_p2Obj.IsEscaping)
            GivePoint((int)_p2Obj.GetPlayerNumber());
    }

    private IEnumerator COGetPlayerRef()
    {
        yield return new WaitForSeconds(0.1f);
        FindPlayersReferences();

        
    }

    private void FindPlayersReferences() {
        Player[] players = FindObjectsOfType<Player>();
        if (players[0].GetPlayerNumber() == Player.PlayerNumber.PlayerOne) {
            _p1Obj = players[0];
            _p2Obj = players[1];
        }
        else {
            _p1Obj = players[1];
            _p2Obj = players[0];
        }
    }

    private IEnumerator COSwap()
    {
        yield return new WaitForSeconds(1.0f);

        _p1Obj.SetIsEscaping(!_p1Obj.IsEscaping);
        _p2Obj.SetIsEscaping(!_p2Obj.IsEscaping);

        SpawnManager.Instance.SpawnPlayers();

        FindPlayersReferences();
    }

    #region Pause Handling
    public void SwitchPause() {
        if (_isPause)
            UnpauseGame();
        else
            PauseGame();
    }

    public void UnpauseGame() {
        if (_isPause)
        {
            Time.timeScale = 1f;
            GameUIManager.Instance.ShowPausePanel(false);
            _isPause = false;
        }
    }
    public void PauseGame() {
        if (!_isPause) 
        {
            Time.timeScale = 0;
            GameUIManager.Instance.ShowPausePanel(true);
            _isPause = true;
        }
    }
    #endregion
}
