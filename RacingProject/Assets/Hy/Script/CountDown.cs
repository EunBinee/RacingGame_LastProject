using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour
{
    public GameObject OptionPanel;

    public int Timer = 0;

    //카운트 이미지
    public GameObject Num_A;   //1번
    public GameObject Num_B;   //2번
    public GameObject Num_C;   //3번
    public GameObject Num_GO;

    //타이머
    public string m_Timer = @"00:00.000";  //분 초 밀리초
    public float m_TotalSeconds; 
    private bool m_IsPlaying;
    public Text m_Text;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;

        Num_A.SetActive(false);
        Num_B.SetActive(false);
        Num_C.SetActive(false);
        Num_GO.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Count();

        //타이머
        if (m_IsPlaying)
        {
            m_Timer = StockwatchTimer();
        }

        if (m_Text)
            m_Text.text = m_Timer;

    }

    public void Count()
    {
        //카운트

        //게임 시작시 정지
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }


        //Timer 가 90보다 작거나 같을경우 Timer 계속증가

        if (Timer <= 200)
        {
            Timer++;

            
            if (Timer < 50)
            {
                Num_C.SetActive(true); //3
            }

            if (Timer > 100)
            {
                Num_C.SetActive(false);
                Num_B.SetActive(true); //2
            }

            if (Timer > 150)
            {
                Num_B.SetActive(false);
                Num_A.SetActive(true); //1
            }

            if (Timer >= 200)
            {
                Num_A.SetActive(false);
                Num_GO.SetActive(true); //go
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //게임시작
                m_IsPlaying=true;
}
        }

      
    }
    
    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        Num_GO.SetActive(false);
    }

    string StockwatchTimer()
    {
        m_TotalSeconds += Time.deltaTime;
        TimeSpan timespan = TimeSpan.FromSeconds(m_TotalSeconds);
        string timer = string.Format("{0:00}:{1:00}.{2:00}",timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        return timer;
    }

}






