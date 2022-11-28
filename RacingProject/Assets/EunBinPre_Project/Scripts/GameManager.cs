using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] runners;   //���� �ٰ��ִ� ���ʵ� (�÷��̾� + Ai)
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







            // int rank=sortArray.Count;
            //    for(int i = 0; i < sortArray.Count; i++)
            //  {
            //sortArray[i].rank = (i+1);  //�տ� ���� ���� �ϵ��̴�.?



            // rankUI.rankName[rank-1] = sortArray[i].name;
            //rank--;
            //   }
    }

}
