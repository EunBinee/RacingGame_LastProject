using System.Collections;
using System.Collections.Generic;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
