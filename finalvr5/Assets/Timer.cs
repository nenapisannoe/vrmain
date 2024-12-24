using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{ 
    public TMP_Text timeText;
    public TMP_Text collectablesText;

    private bool isRunning = false;
    private float elapsedTime = 0f;
    private int collectableCollected = 0;

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
        if (timeText != null)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds);
            timeText.text = timeFormatted;
            Debug.Log("i happened");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Start"))
        {
            StartTimer();
        }
        else if(other.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            PathBuilder.onCollectable?.Invoke(collectableCollected);
            collectableCollected++;
            collectablesText.text = collectableCollected.ToString();
        }
        else if (other.CompareTag("End"))
        {
            StopTimer();
        }
    }
}
