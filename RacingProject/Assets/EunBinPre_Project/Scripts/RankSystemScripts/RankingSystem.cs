using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    //플레이어와 Ai의 등수를 측정하기 위한 스크립트입니다.
    //등수는 거리측정, 현재 몇바퀴돌았는지 여부, 현재 몇번째 체크 포인트에 있는지 여부를 더하여 GameManger에서 결정합니다.

    [SerializeField]TrackCheckPoints trackCheckPoints;          //모든 체크포인트의 정보가 모여있다.
    
    int curCheckPoint_index;
    CheckPointSingle nextCheckPoint;

    void Awake()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            //만약 CheckPoint를 지난다면..
            //curCheckPoint의 인덱스와
            //다음에 지나야하는 NextCheckPoint의 transform을 받아온다. 

            

        }
    }

}
