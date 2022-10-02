using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float CurrentTimer { get => _timer; }
    public bool IsDecreasing { get; set; }

    [SerializeField]
    private TextMeshProUGUI textMesh;


    private float _timer;
    private bool _stop = false;
    public void StartTimerAt(float seconds)
    {
        StopTimer();
        _timer = seconds;
        UpdateTimerDisplay(_timer);
        ResumeTimer();
    }

    public void StartTimerAt(float seconds, bool isDecreasing)
    {
        StopTimer();
        IsDecreasing = isDecreasing;
        _timer = seconds;
        UpdateTimerDisplay(_timer);
        ResumeTimer();
    }

    public bool StopTimer() => _stop = true;
    public bool ResumeTimer() => _stop = false;

    public string GetCurrentFormattedTimer()
    {
        float minutes = Mathf.FloorToInt(_timer / 60);
        float seconds = Mathf.FloorToInt(_timer % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        string timeFormatted = "";
        timeFormatted = timeFormatted + currentTime[0] + currentTime[1] + ":" + currentTime[2] + currentTime[3];

        return timeFormatted;
    }

    public string GetFormattedTimer(float timer)
    {
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        string timeFormatted = "";
        timeFormatted = timeFormatted + currentTime[0] + currentTime[1] + ":" + currentTime[2] + currentTime[3];

        return timeFormatted;
    }

    private void Update()
    {
        //if (_stop)
        //    return;

        //if (!IsDecreasing)
        //    _timer += Time.deltaTime;
        //else if(_timer > 0 && IsDecreasing)
        //    _timer -= Time.deltaTime;

        //if (IsDecreasing && _timer <= 0)
        //    return;

        //UpdateTimerDisplay(_timer);
    }

    private void ResetTimer()
    {
        _timer = 0.0f;
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        string timeFormatted = "";
        timeFormatted = timeFormatted + currentTime[0] + currentTime[1] + ":" + currentTime[2] + currentTime[3];

        // TODO: Update TextMesh Here 
        textMesh.text = timeFormatted;
    }
}
