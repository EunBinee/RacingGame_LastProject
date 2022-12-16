using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RankingSystem : MonoBehaviour
{
    //랭킹 시스템.. 
    //Ai와 플레이어 캐릭터 모두에게 적용이 되어야한다.
    [SerializeField] bool isPlayer = false; //플레이어인지 확인

    [SerializeField]TrackCheckPoints trackCheckPoints;

    public float counter = 0;
    public int rank = 1;

    [Tooltip("시작지점과 끝지점인지")]
    [SerializeField] int lapCount = 0;
    [SerializeField] int lap = 0;
    public int curCheckPoint_index;
    public CheckPointSingle curCheckPointSingle;
    public CheckPointSingle nextCheckPoint;

    //---------------------------------------------
    //랭킹 고정
    public bool fixRank = false;
    public int myRanking = 0;

    public float myRankReverse = 0;

    //---------------------------------------------
    public CheckPointSingle finalCheckPoint;

    void Awake()
    {
        trackCheckPoints = GameObject.FindWithTag("CheckPoints").GetComponent<TrackCheckPoints>();
    }

    private void Start()
    {

        nextCheckPoint = trackCheckPoints.GetNextCheckPoint(transform);               // 다음 체크 포인트의 스크립트
        curCheckPoint_index = trackCheckPoints.CurCheckPointIndex(transform);     // 현재 체크 포인트의 인덱스 값
        curCheckPointSingle = null;

        lapCount = 0;
        lap = 0;
    }
    private void Update()
    {
        DistanceCalculation();
    }

    private void DistanceCalculation()
    {
        if (!fixRank)
        {
            if (lapCount == 0)
            {
                //만약 스타트하고 아직 스타트라인을 지나지않은 경우에는 그냥 1로 계산

                lap = 1;
            }
            else
            {
                lap = lapCount;
            }

            float distance = Vector3.Distance(transform.position, nextCheckPoint.transform.position);

            counter = (lap * 1000 + curCheckPoint_index * 100 - distance);
        }
        else
        {
            rank = myRanking;
            counter += (myRankReverse * 1000);
        }
    
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {

            CheckPointSingle preCheckPoint = curCheckPointSingle;

            //체크 포인트에 닿았다면
            nextCheckPoint = trackCheckPoints.GetNextCheckPoint(transform);   
            curCheckPoint_index = trackCheckPoints.CurCheckPointIndex(transform);

            curCheckPointSingle = other.GetComponent<CheckPointSingle>();
            if(curCheckPointSingle.startAndGoal)
            {

               if(lapCount ==0)
                {
                    lapCount += 1;
                    trackCheckPoints.resetCheckPointShow();
                }

                if (finalCheckPoint == preCheckPoint)
                {
                    lapCount += 1;
                    trackCheckPoints.resetCheckPointShow();

                    if (lapCount == GameManager.GetInstance().finishLap)
                    {
                        GameManager.GetInstance().isfinish = true;


                        //골했을 경우
                        fixRank = true;
                        myRanking = rank;
                        myRankReverse = (10 - rank);

                        if (isPlayer)
                        {
                            GameManager.GetInstance().playerFinish = true;
                        }

                    }
                }

            }

        }
    }

}
