using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    List<RankingSystem> sortArray;
    [SerializeField] RankUI rankUI;
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

        string curPlayerRankText = player.GetComponent<RankingSystem>().rank.ToString() + "��!";
        rankUI.curPlayerRank.text = curPlayerRankText;
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

    }



}
