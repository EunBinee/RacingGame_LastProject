using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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
    //랭킹
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] runners;   //현재 뛰고있는 러너들 (플레이어 + Ai)
    List<RankingSystem> sortArray;
    [SerializeField] RankUI rankUI;
    //--------------------------------------------------------------------------------------------------------
    //해설
    [SerializeField] GameObject explanation_Board;
    [SerializeField] Text explanation_Text;
    //애니메이션
    Animator Explanation_Anim;

    //-------------------------------------------------------------------------------------------------------


    void Start()
    {
        instance = this;

        player = GameObject.Find("Player");
        runners = GameObject.FindGameObjectsWithTag("Bicycle");
        rankUI = GameObject.FindWithTag("RankUI").GetComponent<RankUI>();
        explanation_Board = GameObject.Find("explanation_Board");
       // explanation_Text = GameObject.Find("explanation_Text");


        Explanation_Anim = explanation_Board.GetComponent<Animator>();
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
    }

    public void GetDialogue(int CheckPointNumber)
    {

    }



}
