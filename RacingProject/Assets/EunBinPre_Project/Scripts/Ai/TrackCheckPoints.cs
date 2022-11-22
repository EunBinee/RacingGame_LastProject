using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoints : MonoBehaviour
{
    public EventHandler OnCarCorrectCheckPoint;
    public EventHandler OnCarWrongCheckPoint;

    [SerializeField] private List<Transform> carTransformList; //자동차 별 리스트

    private List<CheckPointSingle> checkPointSingleList;      //
    private List<int> nextCheckPointSingleIndex;                    //다음 체크포인트의 인덱스 ( list로 나누어서 자동차 별로 체크 포인트를 나누어 줌)
    private void Awake()
    {
        //Transform checkPointTransform = transform.Find("CheckPoint");
        checkPointSingleList = new List<CheckPointSingle>();
        foreach (Transform checkPoint in transform)
        {
            //CheckPoints의 모든 자식을 순환하여 가지고 온다.
            CheckPointSingle checkPointSingle = checkPoint.GetComponent<CheckPointSingle>();
            checkPointSingle.SetTrackCheckPoint(this);
            checkPointSingleList.Add(checkPointSingle);
        }
        nextCheckPointSingleIndex = new List<int>();
        foreach(Transform carTransform in carTransformList)
        {
            nextCheckPointSingleIndex.Add(0);   //처음 체크박스는 0부터
        }
    }

    public void CarThoughCheckPoint(CheckPointSingle checkPointSingle,Transform carTransform)
    {
        int nextCheckPointSingle_Index = nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)];
        
        //통과한 체크포인트가 맞는 순서인지 확인하는 함수
        Debug.Log(checkPointSingleList.IndexOf(checkPointSingle));

        if (checkPointSingleList.IndexOf(checkPointSingle) == nextCheckPointSingle_Index)  
        {
            //맞게 통과함
            Debug.Log("알맞게 통과!");

            CheckPointSingle correctCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index];
            //맞는 체크포인트를 통과하면 CheckPointSing의 값을 가지고 온다.
            correctCheckPointSingle.Hide();
            nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)] = (nextCheckPointSingle_Index + 1) % checkPointSingleList.Count; //여러바퀴 도는 경우도 있으니깐.. 모듈로..
            OnCarCorrectCheckPoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //틀리게 통과함.
            Debug.Log("틀린 길이다.!");
            OnCarWrongCheckPoint?.Invoke(this, EventArgs.Empty);

            CheckPointSingle correctCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index];
            //맞는 체크포인트를 통과하면 CheckPointSing의 값을 가지고 온다.
            correctCheckPointSingle.Show();
        }
    
    }

    public void ResetCheckPoint(Transform carTransform)
    {
        //다시 체크포인트 0부터

        nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)] = 0;


        foreach (CheckPointSingle checkPointSingle in checkPointSingleList) 
        {
            checkPointSingle.Show();
        }
    }

    public GameObject GetNextCheckPoint(Transform carTransform)
    {
        //다시 체크포인트 0부터
        GameObject nextCheckPoint = checkPointSingleList[nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)]].gameObject;
        return nextCheckPoint;
    }
}
