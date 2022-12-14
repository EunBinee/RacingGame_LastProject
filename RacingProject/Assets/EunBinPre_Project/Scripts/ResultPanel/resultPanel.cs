using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{

    //UI 패널 다 끄기



    //애들 등수 받아오기(함수 만들기)\
    [SerializeField] GameObject Player;
    public bool playerPass = false;

    [SerializeField] private GameObject[] bicycles;
    [SerializeField] List<RankingSystem> sortArray;

    [SerializeField] GameObject resultImg;

    //sortArray대로 차례로 값 넣어주기
    public Text[] bicyclesRank;


    //별 등수 넣기
    //1~2등 별 3개, 3등 2개, 4~6등 별 1개, 7등에서 10등 별 0개
    public GameObject[] star;

    public Sprite[] playerRankImage;
    public Image playerRank;

    void Start()
    {
        bicycles = GameObject.FindGameObjectsWithTag("Bicycle");
        for (int i = 0; i < bicycles.Length; i++)
        {
            //움직임 막음
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
        //rank에 따라서 오름차순 정렬
        int rank_ = sortArray.Count();
        for (int i = 0; i < bicycles.Length; i++)
        {
            //앞에 있을수록 뒷순위이다.
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
                //1등인 경우
                star[0].SetActive(true);
                Debug.Log("1등 " + rank.ToString());
                star[1].SetActive(true);

                star[2].SetActive(true);
                //youwin
                playerRank.sprite = playerRankImage[0];

            }
            else if (rank <= 3)
            {
                star[0].SetActive(true);
                star[1].SetActive(true);
                Debug.Log("2~3등 " + rank.ToString());
                //youwin

                playerRank.sprite = playerRankImage[0];
            }
            else if (rank <= 6)
            {
                star[0].SetActive(true);
                Debug.Log("이후등 " + rank.ToString());
                //youF

                playerRank.sprite = playerRankImage[1];
            }

        }
        else
        {
            //플레이어가 통과 못한 경우
            //별은 안 킨다.

            // 이미지는 you Lose로 함

            playerRank.sprite = playerRankImage[2];

        }

    }
}
