using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField]  List<RankingSystem> sortArray;
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
        //explanation_Board = GameObject.Find("explanation_Board");
       // explanation_Text = GameObject.Find("explanation_Text").GetComponent<Text>();


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
        Dialogue dialogue = DBManager.GetInstance().GetDialogue(CheckPointNumber);

        RankingSystem playerRank = player.GetComponent<RankingSystem>();

        int rank=0;

        if (playerRank.rank == 1)
            rank = playerRank.rank;
        else if (playerRank.rank == 2)
            rank = playerRank.rank;
        else if (playerRank.rank == 3)
            rank = playerRank.rank;
        else if (playerRank.rank >= 4 && playerRank.rank <= 6)
            rank = 4;
        else if(playerRank.rank >= 7 && playerRank.rank <= 10)
            rank = 5;



        explanation_Board.SetActive(true);
        Explanation_Anim.SetTrigger("StartEx");
        StartCoroutine(StartExplanation(dialogue, rank));
        

    }


    IEnumerator StartExplanation(Dialogue dialogue, int rank)
    {
        int i = 0;
        while(true)
        {
            
            explanation_Text.text = dialogue.contexts[rank-1][i];
            
            i++;
            if (dialogue.contexts[rank-1].Count > i )
            {
                yield return new WaitForSeconds(1f);
            }
            else
                break;
        }

        yield return new WaitForSeconds(1f);
        Explanation_Anim.SetTrigger("EndEx");
        Invoke("ExplanationSetActiveFalse", 1.2f);
    }

    void ExplanationSetActiveFalse()
    {
        explanation_Board.SetActive(false);
    }

}
