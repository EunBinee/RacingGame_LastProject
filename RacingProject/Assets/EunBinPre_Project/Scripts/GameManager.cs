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
    int curPlayerRank;
    //---------------------------------------------------------------------------------------------------
    //바퀴수 UI
    public bool isfinish = false;
    public int finishLap = 3;                //3이면 2바퀴 도는 것임
    public bool playerFinish = false;  //플레이어가 끝났는지 확인



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
        //explanation_Text = GameObject.Find("explanation_Text").GetComponent<Text>();
        Explanation_Anim = explanation_Board.GetComponent<Animator>();


        //등수
        sortArray = new List<RankingSystem>();
        foreach (var runner in runners)
        {
            RankingSystem runnerRank = runner.GetComponent<RankingSystem>();
            sortArray.Add(runnerRank);
        }

        curPlayerRank = player.GetComponent<RankingSystem>().rank;
        rankUI.playerRank = curPlayerRank;


    }


    void Update()
    {
        RankCalculation();

        curPlayerRank = player.GetComponent<RankingSystem>().rank;
        rankUI.playerRank = curPlayerRank;

        //----------------------------------------------------------------------------------------------------------------------------------
        //바퀴수 체크와 게임의 끝을 판정
        CheckLap();
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
    //--------------------------------------------------------------------------------------------
    //해설 시스템
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
    //----------------------------------------------------------------------------------
    //finishLap-1 바퀴 돌면 끝나도록!
    void CheckLap()
    {
        if (isfinish)
        {
            //만약 isFinish가 true가 된다면 바퀴가 끝나는 것이다.
            FinishGame();
        }
    }
    void FinishGame()
    {
        //1. 플레이어가 통과했을 경우 바로 끝
        //2. Ai가 통과했을 경우 10초를 기다린다.    
        //그리고 10초가 넘기면 바로 모든 게임을 끝낸다.
        //시간초가 흘러가는 중 플레이어가 통과하면 바로 게임을 끝낸다.
        if(!playerFinish)
        {
            //ai가 통과해서 끝난 경우
            Debug.Log("ai통과!! 10초를 기다립니다!");

        }
        else
        {
            //player가 통과해서 끝난 경우
            Debug.Log("끝!");
        }

    }

}
