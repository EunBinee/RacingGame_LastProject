using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SBPScripts;

public class CountDown : MonoBehaviour
{
    public GameObject OptionPanel;

    public int Timer = 0;

    //ī��Ʈ �̹���
    public GameObject Num_A;   //1��
    public GameObject Num_B;   //2��
    public GameObject Num_C;   //3��
    public GameObject Num_GO;

    //Ÿ�̸�
    public string m_Timer = @"00:00.000";  //�� �� �и���
    public float m_TotalSeconds;
    private bool m_IsPlaying;
    public Text m_Text;

    GameObject[] bicycleArray;

    private void Awake()
    {
        Timer = 0;

        Num_A.SetActive(false);
        Num_B.SetActive(false);
        Num_C.SetActive(false);
        Num_GO.SetActive(false);

    }

    private void Start()
    {
        bicycleArray = GameObject.FindGameObjectsWithTag("Bicycle");
    }
    // Update is called once per frame
    void Update()
    {

        Count();

        //Ÿ�̸�
        if (m_IsPlaying)
        {
            m_Timer = StockwatchTimer();
        }

        if (m_Text)
            m_Text.text = m_Timer;

    }

    public void Count()
    {
        //ī��Ʈ

        //���� ���۽� ����
        if (Timer == 0)
        {
            Time.timeScale = 0.0f;
        }


        //Timer �� 90���� �۰ų� ������� Timer �������

        if (Timer <= 80)
        {
            Timer++;


            if (Timer < 20)
            {
                Num_C.SetActive(true); //3
            }

            if (Timer > 40)
            {
                Num_C.SetActive(false);
                Num_B.SetActive(true); //2
            }

            if (Timer > 60)
            {
                Num_B.SetActive(false);
                Num_A.SetActive(true); //1
            }

            if (Timer >= 80)
            {
                Num_A.SetActive(false);
                Num_GO.SetActive(true); //go
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //���ӽ���
                m_IsPlaying = true;

                for (int i = 0; i < bicycleArray.Length; i++)
                {
                    bicycleArray[i].GetComponent<BicycleController>().isNotStart = true;
                }

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
        string timer = string.Format("{0:00}:{1:00}.{2:00}", timespan.Minutes, timespan.Seconds, timespan.Milliseconds);

        return timer;
    }

}





