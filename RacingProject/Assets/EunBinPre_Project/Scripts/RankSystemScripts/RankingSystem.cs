using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class RankingSystem : MonoBehaviour
{
    //플레이어와 Ai의 등수를 측정하기 위한 스크립트입니다.
    //등수는 거리측정, 현재 몇바퀴돌았는지 여부, 현재 몇번째 체크 포인트에 있는지 여부를 더하여 GameManger에서 결정합니다.

    [SerializeField]TrackCheckPoints trackCheckPoints;          //모든 체크포인트의 정보가 모여있다.

    public float counter = 0;
    public int rank = 0;

    [SerializeField] int lapCount = 0; //현재 몇바퀴 돌았는지
    public int curCheckPoint_index;
    public CheckPointSingle nextCheckPoint;


    void Awake()
    {
        
    }

    private void Start()
    {
        nextCheckPoint = trackCheckPoints.GetNextCheckPoint(transform);               // 거리를 재기 위해서 transform 필요
        curCheckPoint_index = trackCheckPoints.CurCheckPointIndex(transform);     // 현재 내가 있는 체크 포인트의 인덱스
    }
    private void Update()
    {
        DistanceCalculation();
    }

    private void DistanceCalculation()
    {
        float distance = Vector3.Distance(nextCheckPoint.transform.position, transform.position);
        counter = lapCount * 1000 + curCheckPoint_index * 100 + distance;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            //만약 CheckPoint를 지난다면..
            //curCheckPoint의 인덱스와
            //다음에 지나야하는 NextCheckPoint의 transform을 받아온다. 

            nextCheckPoint = trackCheckPoints.GetNextCheckPoint(transform);     //거리를 재기 위해서 transform 필요
            curCheckPoint_index = trackCheckPoints.CurCheckPointIndex(transform);

        }
    }

}
