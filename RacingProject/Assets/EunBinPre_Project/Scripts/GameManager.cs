using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�̱���-------------------------------------------------------------------------------------------
    //��𼭵� ��������.~
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }
    //----------------------------------------------------------------------------------------------------
    //��ŷ
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] runners;   //���� �ٰ��ִ� ���ʵ� (�÷��̾� + Ai)
    [SerializeField]  List<RankingSystem> sortArray;
    [SerializeField] RankUI rankUI;

    //---------------------------------------------------------------------------------------------------
    //������ UI
    public bool isfinish = false;
    public int finishLap = 3;                //3�̸� 2���� ���� ����
    public bool playerFinish = false;  //�÷��̾ �������� Ȯ��



    //--------------------------------------------------------------------------------------------------------
    //�ؼ�
    [SerializeField] GameObject explanation_Board;
    [SerializeField] Text explanation_Text;
    //�ִϸ��̼�
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

        string curPlayerRankText = player.GetComponent<RankingSystem>().rank.ToString() + "��!";
        rankUI.curPlayerRank.text = curPlayerRankText;

        //----------------------------------------------------------------------------------------------------------------------------------
        //������ üũ�� ������ ���� ����
        CheckLap();
    }
   public void  RankCalculation()
    {
        sortArray = sortArray.OrderBy(x => x.counter).ToList();
        //counter�� ���� �������� ����
        int rank_ = sortArray.Count;
        for (int i = 0; i < sortArray.Count; i++)
        {
            //�տ� �������� �޼����̴�.
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
    //----------------------------------------------------------------------------------
    //finishLap-1 ���� ���� ��������!
    void CheckLap()
    {
        if (isfinish)
        {
            //���� isFinish�� true�� �ȴٸ� ������ ������ ���̴�.
            FinishGame();
        }
    }
    void FinishGame()
    {
        //1. �÷��̾ ������� ��� �ٷ� ��
        //2. Ai�� ������� ��� 10�ʸ� ��ٸ���.    
        //�׸��� 10�ʰ� �ѱ�� �ٷ� ��� ������ ������.
        //�ð��ʰ� �귯���� �� �÷��̾ ����ϸ� �ٷ� ������ ������.
        if(!playerFinish)
        {
            //ai�� ����ؼ� ���� ���
            Debug.Log("ai���!! 10�ʸ� ��ٸ��ϴ�!");

        }
        else
        {
            //player�� ����ؼ� ���� ���
            Debug.Log("��!");
        }

    }

}
