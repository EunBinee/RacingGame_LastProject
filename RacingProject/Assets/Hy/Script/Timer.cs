using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public string time = @"00:00.000";
    private bool isTimer ;
    public float totalSeconds; // 만약 시간에 따라서 이벤트를 발생하려면, 이 값을 사용. 
    public Text timerText;

    GameManager gameManager;

    void Start()
    {

    }
    private void Update()
    {
       
        //바퀴 다 돌면 타이머 정지
        //if (gameManager.playerFinish)
        //{
        //    isTimer = !isTimer;
        //}
        if (isTimer)
        {
            time= StockwatchTimer();
        }
        if (timerText)
            timerText.text = time;


    }
    string StockwatchTimer()
    {
        totalSeconds += Time.deltaTime;
        TimeSpan timespan = TimeSpan.FromSeconds(totalSeconds);
        string timer = string.Format("{0:00}:{1:00}.{2:00}",timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        return timer;
    }
}
