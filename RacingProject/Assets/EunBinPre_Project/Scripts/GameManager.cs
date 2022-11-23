using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤-------------------------------------------------------------------------------------------
    //어디서든 쓸수있음.~
    private static GameManager instance = new GameManager();

    public static GameManager GetInstance()
    {
        return instance;
    }
    //----------------------------------------------------------------------------------------------------

    [SerializeField] List<GameObject> runners = new List<GameObject>();   //현재 뛰고있는 러너들 (플레이어 + Ai)
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
        //counter에 따라서 오름차순 정렬

        //맨앞에 있는게 꼴찌~~!
        int rank=sortArray.Count;
        for(int i = 0; i < sortArray.Count; i++)
        {
            sortArray[i].rank = rank;
            rankUI.rankName[rank] = sortArray[i].name;
            rank--;
        }
    }

}
