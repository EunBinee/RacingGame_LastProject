using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ResultImg;

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
   public  int curPlayerRank;


    //---------------------------------------------------------------------------------------------------
    //바퀴수 UI
    public bool isfinish = false;
    public int finishLap = 2;                //3이면 2바퀴 도는 것임
    public bool playerFinish = false;  //플레이어가 끝났는지 확인

    //--------------------------------------------------------------------------------------------------------
    //해설
    [SerializeField] GameObject explanation_Board;
    [SerializeField] Text explanation_Text;
    //애니메이션
    Animator Explanation_Anim;

    //-------------------------------------------------------------------------------------------------------
    //프리펩 네임
    List<string> AiNameList;
    public string playerName = "";  //플레이어 이름

    //-------------------------------------------------------------------------------------------------------
    //결과 창
    [SerializeField] GameObject ResultImgPanel;
    [SerializeField] GameObject FinishCount;
    bool alreadyShow = false;




    private void Awake()
    {
        //랜덤 Ai이름
        AiNameList = new List<string>();
        string[] ainames = new string[20];
        ainames[0] = "귀여운 라이언"; ainames[1] = "피자먹고 잠든 무지"; ainames[2] = "츄리닝 입은 네오"; ainames[3] = "춘식이"; ainames[4] = "흔냥";
        ainames[5] = "넌무"; ainames[6] = "어피치"; ainames[7] = "캐뒨"; ainames[8] = "누흐"; ainames[9] = "호식이";
        ainames[10] = "베라"; ainames[11] = "꿔바로우"; ainames[12] = "뽀오"; ainames[13] = "고라니"; ainames[14] = "세모";
        ainames[15] = "히아"; ainames[16] = "세이"; ainames[17] = "나나"; ainames[18] = "나원"; ainames[19] = "희성";
        ainames = ShuffleArray(ainames);
        for (int i = 0; i < 9; i++)
        {
            AiNameList.Add(ainames[i]);
        }

        playerName = PlayerPrefs.GetString("CurrentPlayerName");

        //플레이어 이름 변경
        player.name = playerName;


        //Ai이름 변경
        runners = GameObject.FindGameObjectsWithTag("Bicycle");

        int j = 0;
        for(int i =  0;i<runners.Length; i++)
        {
            if (runners[i].name != playerName)
            {
                runners[i].name = AiNameList[j];
                j++;
            }
        }

    }


    void Start()
    {
        instance = this;

        rankUI = GameObject.FindWithTag("RankUI").GetComponent<RankUI>();
        explanation_Board = GameObject.Find("explanation_Board");
        //explanation_Text = GameObject.Find("explanation_Text").GetComponent<Text>();
        Explanation_Anim = explanation_Board.GetComponent<Animator>();

        explanation_Board.SetActive(false);
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
    public void RankCalculation()
    {

        sortArray = sortArray.OrderBy(x => x.counter).ToList();
        //counter에 따라서 오름차순 정렬
        int rank_ = sortArray.Count;
        for (int i = 0; i < sortArray.Count; i++)
        {
            //앞에 있을수록 뒷순위이다.
            sortArray[i].rank = rank_;
            rankUI.rankName[rank_  - 1] = sortArray[i].name;
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

        //등수 가지고 오기..
        List<RankingSystem> rankSort = new List<RankingSystem>();

        rankSort = sortArray;
        int rankIndex = rankSort.Count;
        string playerName = player.name;
        string name_1th = rankSort[rankIndex-1].name;
        string name_2th = rankSort[rankIndex - 2].name;
        string name_3th = rankSort[rankIndex - 3].name;

        while (true)
        {


            string text = dialogue.contexts[rank - 1][i];
            text = text.Replace("`", ",");
            text = text.Replace("[P]", playerName);
            text = text.Replace("[1]", name_1th);
            text = text.Replace("[2]", name_2th);
            text = text.Replace("[3]", name_3th);

            explanation_Text.text = text;


            i++;
            if (dialogue.contexts[rank-1].Count > i )
            {
                yield return new WaitForSeconds(1.5f);
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
        if(!alreadyShow)
        {
            if (!playerFinish)
            {
                //ai가 통과해서 끝난 경우-----
                Debug.Log("ai통과!! 10초를 기다립니다!");
                FinishCount.SetActive(true);

                FinishCount.GetComponent<FinishCount>().StartCount();

                alreadyShow = true;
            }
            else
            {
                //player가 통과해서 끝난 경우 ------
                Debug.Log("끝!");



                ResultImgPanel.SetActive(true);
                ResultImgPanel.GetComponent<ResultImg>().ChangeImg(Img.PlayerFinish);

                alreadyShow = true;

            }
        }



    }


    //----------------------------------------------------------------------------------
    //랜덤 Ai이름
    private T[] ShuffleArray<T>(T[] ainames)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < ainames.Length; ++i)
        {
            random1 = Random.Range(0, ainames.Length);
            random2 = Random.Range(0, ainames.Length);

            temp = ainames[random1];
            ainames[random1] = ainames[random2];
            ainames[random2] = temp;
        }

        return ainames;
    }

}
