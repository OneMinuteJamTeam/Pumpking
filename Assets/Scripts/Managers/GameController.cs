using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using xPoke.CustomLog;

public class GameController : Singleton<GameController>
{
    public int Player1Points { get; private set; } = 0;
    public int Player2Points { get; private set; } = 0;

    [Header("References")]
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private RandomObjectSpawner crownsTrapsSpawner;
    [SerializeField]
    private Pumpkin _pumpkin;
    [SerializeField]
    private Scarecrow _scarecrow;
    [Header("Spawn Points")]
    [SerializeField]
    private Transform P1SpawnPos;
    [SerializeField]
    private Transform P2SpawnPos;

    private GameObject pickablesContainer;
    private bool _isPause = false;
    private int _round = 1;
    private bool _startedSwap = false;
    private bool _roundOver = false;
    private List<GameObject> ragdolls;

    private void Start()
    {
        ResetPoints();
        InitPlayersRoles();
        timer.StartTimerAt(60, true);
        crownsTrapsSpawner.Spawn();
        EnablePlayersMovement();
        ragdolls = new List<GameObject>();  
    }

    private void Update()
    {
        if(InputManager.Instance.GetApplicationPausePressed())
            SwitchPause();

        if(timer.CurrentTimer >=30 && timer.CurrentTimer < 31 && !_startedSwap)
            GivePointToEscapee();
        
        if(timer.CurrentTimer <= 0 && !_roundOver)
            GivePointToEscapee();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_player"></param>
    /// <param name="_wait">waits some time before swap</param>
    public void GivePoint(int _player,bool _wait = false) 
    {
        if (_player == 0)
            Player1Points++;
        else if (_player == 1)
            Player2Points++;
        else 
            Debug.LogError("GameController: assigned point to player " + _player + ", that doesn't exist");

        PlayerPrefs.SetInt("P1Points",Player1Points);
        PlayerPrefs.SetInt("P2Points",Player2Points);

        //Debug.Log("P1 "+Player1Points + " - P2 " + Player2Points);

        GameUIManager.Instance.SetScoreText(Player1Points,Player2Points);

        _pumpkin.CanReadInput = false;
        _scarecrow.CanReadInput = false;

        float waitTime;
        if (_wait) waitTime = 0.5f;
        else waitTime = 0f;
        StartCoroutine(givePointCor(waitTime));
    }
    private IEnumerator givePointCor(float _waitTime) {
        yield return new WaitForSeconds(_waitTime);
        if (_round == 1) {
            _round++;
            SwapRoles();
        }
        else if (_round == 2) {
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
        _startedSwap = true;

        timer.StartTimerAt(30, true);
        timer.StopTimer();

        Destroy(pickablesContainer);

        GameUIManager.Instance.PlaySwapPanel();

        for (int i = 0; i < ragdolls.Count; i++) Destroy(ragdolls[i]);

        _pumpkin.gameObject.SetActive(true);
        _scarecrow.gameObject.SetActive(true);

        _pumpkin.SetIsEscaping(!_pumpkin.IsEscaping);
        _scarecrow.SetIsEscaping(!_scarecrow.IsEscaping);

        _pumpkin.transform.position = P1SpawnPos.position;
        _scarecrow.transform.position = P2SpawnPos.position;

        crownsTrapsSpawner.Spawn();
    }

    private void GivePointToEscapee()
    {
        if (_pumpkin.IsEscaping)
            GivePoint((int)_pumpkin.GetPlayerNumber());
        else if (_scarecrow.IsEscaping)
            GivePoint((int)_scarecrow.GetPlayerNumber());
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

    public void EnablePlayersMovement() {
        _pumpkin.CanReadInput = true;
        _scarecrow.CanReadInput = true;
    }

    public void ResumeTimer() {
        timer.ResumeTimer();
    }

    private void InitPlayersRoles() {
        bool isPumpkinEscaping = PlayerPrefs.GetInt("PumpkinEscaping") == 1 ? true : false;
        bool isScarecrowEscpaing = PlayerPrefs.GetInt("ScarecrowEscaping") == 1 ? true : false;
        _scarecrow.SetIsEscaping(isScarecrowEscpaing);
        _pumpkin.SetIsEscaping(isPumpkinEscaping);
    }

    public void RegisterRagdoll(GameObject _ragdoll) {
        ragdolls.Add(_ragdoll);
    }

    #endregion
}
