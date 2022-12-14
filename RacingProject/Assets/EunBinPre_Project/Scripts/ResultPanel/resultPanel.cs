using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{

    //UI �г� �� ����



    //�ֵ� ��� �޾ƿ���(�Լ� �����)\
    [SerializeField] GameObject Player;
    public bool playerPass = false;

    [SerializeField] private GameObject[] bicycles;
    [SerializeField] List<RankingSystem> sortArray;

    [SerializeField] GameObject resultImg;

    //sortArray��� ���ʷ� �� �־��ֱ�
    public Text[] bicyclesRank;


    //�� ��� �ֱ�
    //1~2�� �� 3��, 3�� 2��, 4~6�� �� 1��, 7��� 10�� �� 0��
    public GameObject[] star;

    void Start()
    {
        bicycles = GameObject.FindGameObjectsWithTag("Bicycle");
        for (int i = 0; i < bicycles.Length; i++)
        {
            //������ ����
            bicycles[i].GetComponent<BicycleController>().isFinish = true;
            sortArray.Add(bicycles[i].GetComponent<RankingSystem>());
        }


        SortRankArray();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SortRankArray()
    {
        resultImg.SetActive(false);
        sortArray = sortArray.OrderBy(x => x.rank).ToList();
        //rank�� ���� �������� ����
        int rank_ = sortArray.Count();
        for (int i = 0; i < bicycles.Length; i++)
        {
            //�տ� �������� �޼����̴�.
            bicyclesRank[i].text = sortArray[i].gameObject.name;
           // Debug.Log(sortArray[rank_ - 1].gameObject.name);
            rank_--;

        }
        StarByRank();
    }



    public void StarByRank()
    {
        int rank = Player.GetComponent<RankingSystem>().rank;
        if (playerPass)
        {
            if (rank == 1)
            {
                //1���� ���
                star[0].SetActive(true);
                Debug.Log("1�� " + rank.ToString());
                star[1].SetActive(true);

                star[2].SetActive(true);



            }
            else if (rank <= 3)
            {
                star[0].SetActive(true);
                star[1].SetActive(true);
                Debug.Log("2~3�� " + rank.ToString());
            }
            else if (rank <= 6)
            {
                star[0].SetActive(true);
                Debug.Log("���ĵ� " + rank.ToString());
            }

        }
        else
        {
            //�÷��̾ ��� ���� ���
            //���� �� Ų��.

            // �̹����� you Lose�� ��

        }

    }
}
