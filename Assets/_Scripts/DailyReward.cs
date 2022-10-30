using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

/* 
    THIS SCRIPT DOESN'T USE THE WorldTimeAPI yet...
 */
public class DailyReward : MonoBehaviour
{
    DateTime startTimer;
    DateTime TimerEnd;

    private bool timerRunning;

    public GameObject panel;
    public TextMeshProUGUI dailyText;

    public void OnButtonClick()
    {
        if (timerRunning)
            return; 
        StartTimer();
    }

    private void InitializePanel()
    {
        dailyText.text = "Time until next reward: \n" +
            (TimerEnd - DateTime.Now).Hours + " :" +
            (TimerEnd - DateTime.Now).Minutes + " :"+
            (TimerEnd - DateTime.Now).Seconds;
    }

    private void FixedUpdate()
    {
        if (!timerRunning)
            return; 
        InitializePanel(); 
    }
    private void StartTimer()
    {
        if (!timerRunning)
        {
            startTimer = DateTime.Now;
            TimerEnd = startTimer.AddDays(1);

            timerRunning = true;

            StartCoroutine(Timer());

            InitializePanel();
        }
    }

    private IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinish = (TimerEnd - start).TotalMilliseconds;

        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinish));
        
        timerRunning = false;
        // print("Reward Daily"); 
    }
}
