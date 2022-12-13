using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resultPanel : MonoBehaviour
{
    //UI 패널 다 끄기

    //애들 등수 받아오기(함수 만들기)\
    [SerializeField] private GameObject[] bicycles;
    [SerializeField] List<RankingSystem> sortArray;



    //sortArray대로 차례로 값 넣어주기
    public Text[] bicyclesRank;


    //별 등수 넣기
    //1~2등 별 3개, 3등 2개, 4~6등 별 1개, 7등에서 10등 별 0개
    public GameObject[] star;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
