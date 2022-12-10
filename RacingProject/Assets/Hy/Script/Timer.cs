using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //public Text[] text_time; //�ð��� ǥ���� txt
    //float time;
    //public bool isTimer=false;
    public string time = @"00:00.000";
    private bool isTimer ;
    public float totalSeconds; // ���� �ð��� ���� �̺�Ʈ�� �߻��Ϸ���, �� ���� ����ϸ� �˴ϴ�. 
    public Text timerText;

    GameManager gameManager;

    void Start()
    {

    }
    private void Update()
    {
       
        //���� �� ���� Ÿ�̸� ����
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
