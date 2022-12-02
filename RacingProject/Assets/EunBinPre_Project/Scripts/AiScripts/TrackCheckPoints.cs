using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoints : MonoBehaviour
{
    public EventHandler OnBicycleCorrectCheckPoint;
    public EventHandler OnBicycleWrongCheckPoint;


    [SerializeField] private GameObject[] bicycleArray;
    [SerializeField] private List<Transform> bicycleTransformList; //자전거 리스트

    private List<CheckPointSingle> checkPointSingleList;      // 현재 TrackCheckPoints안에 있는 CheckPointSingle들 모음

    private List<int> nextCheckPointSingleIndex;                    //다음 체크포인트의 인덱스
                                                                                                 //(자전거가 가야하는 다음 포인트 지점 // (자전거 리스트의 인덱스로 구분)
    private List<int> curCheckPointSingleIndex;                         //현재 자전거가 지나온 체크포인트

    [SerializeField] private TrackCheckPointUI trackCheckPointUI;


    private void Awake()
    {
        bicycleArray = GameObject.FindGameObjectsWithTag("Bicycle");

        bicycleTransformList = new List<Transform>();
        for(int i = 0; i < bicycleArray.Length; i++)
        {
            bicycleTransformList.Add(bicycleArray[i].transform);
        }


        checkPointSingleList = new List<CheckPointSingle>();
        foreach (Transform checkPoint in transform)
        {
            //CheckPoints의 모든 자식을 순환하여 가지고 온다.
            CheckPointSingle checkPointSingle = checkPoint.GetComponent<CheckPointSingle>();
            checkPointSingle.SetTrackCheckPoint(this);
            checkPointSingleList.Add(checkPointSingle);
        }

        nextCheckPointSingleIndex = new List<int>();
        curCheckPointSingleIndex = new List<int>();
        foreach (Transform carTransform in bicycleTransformList)
        {
            nextCheckPointSingleIndex.Add(0);   //모든 자전거의 처음 체크 포인트는 0부터
            curCheckPointSingleIndex.Add(0);
        }
    }

    public void CarThoughCheckPoint(CheckPointSingle checkPointSingle, Transform BicycleTransform)
    {
        //자전거가 CheckPointSingle에 부딫히면, CarThoughCheckPoint를 호출하여
        //현재 자전거가 부딫힌 checkpoint(wayPoint)와 현재 부딫힌 자전거를 매개변수를 통해서 알려준다.

        int nextCheckPointSingle_Index = nextCheckPointSingleIndex[bicycleTransformList.IndexOf(BicycleTransform)];

        //통과한 체크포인트가 맞는 순서인지 확인하는 함수
        int curCheckPoint_Index = checkPointSingleList.IndexOf(checkPointSingle);   //현재 통과한 체크포인트의 인덱스 번호
        curCheckPointSingleIndex[bicycleTransformList.IndexOf(BicycleTransform)] = curCheckPoint_Index;


        Debug.Log(curCheckPoint_Index);

        if (curCheckPoint_Index == nextCheckPointSingle_Index)
        {
            //맞게 통과했다면..
            Debug.Log("알맞게 통과!");

            CheckPointSingle correctCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index];
            //맞는 체크포인트를 통과하면 CheckPointSing의 값을 가지고 온다.
            correctCheckPointSingle.Hide();
            nextCheckPointSingleIndex[bicycleTransformList.IndexOf(BicycleTransform)] = (nextCheckPointSingle_Index + 1) % checkPointSingleList.Count; //여러바퀴 도는 경우도 있으니깐.. 모듈로..
            
            OnBicycleCorrectCheckPoint?.Invoke(this, EventArgs.Empty);    //Null 참조 예외를 피하기 위해서...

            if (BicycleTransform.name == "Player")
            {
                trackCheckPointUI.Hide();
            }
        }
        else
        {
            //틀리게 통과함.
            Debug.Log("틀린 길이다.!");
            OnBicycleWrongCheckPoint?.Invoke(this, EventArgs.Empty); //Null 참조 예외를 피하기 위해서...

            //틀리게 통과하면, 틀린 체크 포인트 wrongCheckPointSingle에 넣어주고, 다시 보여준다.
            //그리고 다음 체크포인트도 뒤로 한칸 물린다.

            CheckPointSingle wrongCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index]; 

            wrongCheckPointSingle.Show();

            //만약 
            if(BicycleTransform.name == "Player")
            {
                trackCheckPointUI.Show();
            }
            
        }

    }

    public void ResetCheckPoint(Transform bicycleTransform)
    {
        //다시 체크포인트 0부터

        nextCheckPointSingleIndex[bicycleTransformList.IndexOf(bicycleTransform)] = 0;


        foreach (CheckPointSingle checkPointSingle in checkPointSingleList)
        {
            checkPointSingle.Show();
        }
    }

    public CheckPointSingle GetNextCheckPoint(Transform bicycleTransform)
    {
        //다음 체크 포인트의 gameObject를 넘긴다.
        CheckPointSingle nextCheckPoint = checkPointSingleList[nextCheckPointSingleIndex[bicycleTransformList.IndexOf(bicycleTransform)]];
        return nextCheckPoint;
    }
    public int CurCheckPointIndex(Transform bicycleTransform)
    {
        //다음 체크 포인트의 gameObject를 넘긴다.
        int curIndex = curCheckPointSingleIndex[bicycleTransformList.IndexOf(bicycleTransform)];
        return curIndex;
    }
}
