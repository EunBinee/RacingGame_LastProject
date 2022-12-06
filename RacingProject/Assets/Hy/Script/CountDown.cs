using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public GameObject OptionPanel;

    public int Timer = 0;

    //ī��Ʈ �̹���
    public GameObject Num_A;   //1��
    public GameObject Num_B;   //2��
    public GameObject Num_C;   //3��
    public GameObject Num_GO;


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
                Time.timeScale = 1.0f; //���ӽ���
            }
        }

      
    }
    

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        Num_GO.SetActive(false);
    }

}






