using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public Text[] text_time; //시간을 표시할 txt
    //float time;
    //public bool isTimer=false;
    public string time = @"00:00.000";
    private bool isTimer ;
    public float totalSeconds; // 만약 시간에 따라서 이벤트를 발생하려면, 이 값을 사용하면 됩니다. 
    public Text timerText;

    GameManager gameManager;

    void Start()
    {

    }
    private void Update()
    {
       
        //바퀴 다 돌면 타이머 정지
        if (gameManager.playerFinish)
        {
            isTimer = !isTimer;
        }
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

    //private void Timertxt()
    //{
    //    time += Time.deltaTime;
    //    text_time[0].text = ((int)time / 3600).ToString();
    //    text_time[1].text = ((int)time / 60 % 60).ToString();
    //    text_time[2].text = ((int)time % 60).ToString();

    //}
}
