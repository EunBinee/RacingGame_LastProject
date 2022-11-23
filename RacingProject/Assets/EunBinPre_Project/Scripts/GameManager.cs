using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱���-------------------------------------------------------------------------------------------
    //��𼭵� ��������.~
    private static GameManager instance = new GameManager();

    public static GameManager GetInstance()
    {
        return instance;
    }
    //----------------------------------------------------------------------------------------------------

    [SerializeField] List<GameObject> runners = new List<GameObject>();   //���� �ٰ��ִ� ���ʵ� (�÷��̾� + Ai)
    List<RankingSystem> sortArray;
    [SerializeField] RankUI rankUI;

    void Start()
    {
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
    }
   public void  RankCalculation()
    {
        sortArray = sortArray.OrderBy(x => x.counter).ToList();
        //counter�� ���� �������� ����

        //�Ǿտ� �ִ°� ����~~!
        int rank=sortArray.Count;
        for(int i = 0; i < sortArray.Count; i++)
        {
            sortArray[i].rank = rank;
            rankUI.rankName[rank] = sortArray[i].name;
            rank--;
        }
    }

}
