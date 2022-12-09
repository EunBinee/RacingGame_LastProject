using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Boost : MonoBehaviour
{
    //private GameObject Player;
    private BicycleController ct;
    public GameObject[] runners;
    [SerializeField] List<BicycleController> runnerBicycleList;

    [SerializeField] private bool isBoost = false;

    [SerializeField] private float time = 0;
    [SerializeField] private float maxTime = 3;         //5�ʵ��� �ν���
    private float fixTime = 3;      //������ �ð�

    private List<int> num;

    void Start()
    {
        runners = GameObject.FindGameObjectsWithTag("Bicycle");
        runnerBicycleList = new List<BicycleController>();

        for (int i = 0; i < runners.Length; i++)
        {
            runnerBicycleList.Add(runners[i].GetComponent<BicycleController>());
        }
        // runnerBicycleList[0].sprint = true;
        num = new List<int>();
    }
    void Update()
    {
        if (isBoost)
        {
            if (time > maxTime)
            {
                Debug.Log("�ð� ��");
                time = 0;
                maxTime = fixTime;
                //ct.sprint = false;
                foreach (int i in num)
                {
                    runnerBicycleList[i].sprint = false;
                }


                isBoost = false;
            }
            else
            {
                time += Time.deltaTime;
                //ct.sprint = true;
               
            }
           
        }
        else { num.Clear(); }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wheel"))
        {
            if (isBoost)
            {
                for (int i = 0; i < runners.Length; i++)
                {
                    if (other.transform.parent.name == runners[i].name)
                    {
                        Debug.Log("�ð��߰�");
                        time = 0;

                        num.Add(i);
                        runnerBicycleList[i].sprint = true;
                    }

                }
            }
            else
            {
                for (int i = 0; i < runners.Length; i++)
                {
                    if (other.transform.parent.name == runners[i].name)
                    {
                        Debug.Log("�޸���!");
                        isBoost = true;

                        num.Add(i);
                        runnerBicycleList[i].sprint = true;
                    }
                }
            }
        }
    }


}