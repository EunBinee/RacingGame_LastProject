using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class resultPanel : MonoBehaviour
{
    //UI �г� �� ����

    //�ֵ� ��� �޾ƿ���(�Լ� �����)\
    [SerializeField] private GameObject[] bicycles;
    [SerializeField] List<RankingSystem> sortArray;



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
            sortArray.Add(bicycles[i].GetComponent<RankingSystem>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SortRankArray();
    }

    public void SortRankArray()
    {
        sortArray = sortArray.OrderBy(x => x.rank).ToList();
        //rank�� ���� �������� ����

        for (int i = 0 ; i < bicyclesRank.Length ; i++)
        {
            //�տ� �������� �޼����̴�.
            bicyclesRank[i].text = sortArray[i].gameObject.name;


        }
    }

}
