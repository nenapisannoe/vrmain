using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{ 
    public TMP_Text  timeDisplay;

    private bool isRunning = false;
    private float elapsedTime = 0f;

    public void StartTimer()
    {
        if (isRunning) return;
        RestartTimer();
    }

    public void RestartTimer()
    {
        isRunning = true;
        elapsedTime = 0f;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
        }
        if (timeDisplay != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds);
            timeDisplay.text = timeFormatted;
            Debug.Log("i happened");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Start"))
        {
            StartTimer();
        }
        else if (other.CompareTag("End"))
        {
            StopTimer();
        }
    }
}
