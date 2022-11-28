using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤-------------------------------------------------------------------------------------------
    //어디서든 쓸수있음.~
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }
    //----------------------------------------------------------------------------------------------------
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] runners;   //현재 뛰고있는 러너들 (플레이어 + Ai)
    List<RankingSystem> sortArray;
    [SerializeField] RankUI rankUI;

    void Start()
    {
        instance = this;

        player = GameObject.Find("Player");
        runners = GameObject.FindGameObjectsWithTag("Bicycle");
        rankUI = GameObject.FindWithTag("RankUI").GetComponent<RankUI>();

        sortArray = new List<RankingSystem>();
        foreach (var runner in runners)
        {
            RankingSystem runnerRank = runner.GetComponent<RankingSystem>();
            sortArray.Add(runnerRank);
        }
    }


    void Update()
    {
        RankCalculation();

        string curPlayerRankText = player.GetComponent<RankingSystem>().rank.ToString() + "등!";
        rankUI.curPlayerRank.text = curPlayerRankText;
    }
   public void  RankCalculation()
    {
        sortArray = sortArray.OrderBy(x => x.counter).ToList();
        //counter에 따라서 오름차순 정렬
        int rank_ = sortArray.Count;
        for (int i = 0; i < sortArray.Count; i++)
        {
            //앞에 있을수록 뒷순위이다.
            sortArray[i].rank = rank_;
            rankUI.rankName[rank_ - 1] = sortArray[i].name;
            rank_--;
        }







            // int rank=sortArray.Count;
            //    for(int i = 0; i < sortArray.Count; i++)
            //  {
            //sortArray[i].rank = (i+1);  //앞에 있을 수록 일등이다.?



            // rankUI.rankName[rank-1] = sortArray[i].name;
            //rank--;
            //   }
    }

}
