using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoints : MonoBehaviour
{
    public EventHandler OnCarCorrectCheckPoint;
    public EventHandler OnCarWrongCheckPoint;

    [SerializeField] private List<Transform> carTransformList; //�ڵ��� �� ����Ʈ

    private List<CheckPointSingle> checkPointSingleList;      //
    private List<int> nextCheckPointSingleIndex;                    //���� üũ����Ʈ�� �ε��� ( list�� ����� �ڵ��� ���� üũ ����Ʈ�� ������ ��)
    private void Awake()
    {
        //Transform checkPointTransform = transform.Find("CheckPoint");
        checkPointSingleList = new List<CheckPointSingle>();
        foreach (Transform checkPoint in transform)
        {
            //CheckPoints�� ��� �ڽ��� ��ȯ�Ͽ� ������ �´�.
            CheckPointSingle checkPointSingle = checkPoint.GetComponent<CheckPointSingle>();
            checkPointSingle.SetTrackCheckPoint(this);
            checkPointSingleList.Add(checkPointSingle);
        }
        nextCheckPointSingleIndex = new List<int>();
        foreach(Transform carTransform in carTransformList)
        {
            nextCheckPointSingleIndex.Add(0);   //ó�� üũ�ڽ��� 0����
        }
    }

    public void CarThoughCheckPoint(CheckPointSingle checkPointSingle,Transform carTransform)
    {
        int nextCheckPointSingle_Index = nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)];
        
        //����� üũ����Ʈ�� �´� �������� Ȯ���ϴ� �Լ�
        Debug.Log(checkPointSingleList.IndexOf(checkPointSingle));

        if (checkPointSingleList.IndexOf(checkPointSingle) == nextCheckPointSingle_Index)  
        {
            //�°� �����
            Debug.Log("�˸°� ���!");

            CheckPointSingle correctCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index];
            //�´� üũ����Ʈ�� ����ϸ� CheckPointSing�� ���� ������ �´�.
            correctCheckPointSingle.Hide();
            nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)] = (nextCheckPointSingle_Index + 1) % checkPointSingleList.Count; //�������� ���� ��쵵 �����ϱ�.. ����..
            OnCarCorrectCheckPoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //Ʋ���� �����.
            Debug.Log("Ʋ�� ���̴�.!");
            OnCarWrongCheckPoint?.Invoke(this, EventArgs.Empty);

            CheckPointSingle correctCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index];
            //�´� üũ����Ʈ�� ����ϸ� CheckPointSing�� ���� ������ �´�.
            correctCheckPointSingle.Show();
        }
    
    }

    public void ResetCheckPoint(Transform carTransform)
    {
        //�ٽ� üũ����Ʈ 0����

        nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)] = 0;


        foreach (CheckPointSingle checkPointSingle in checkPointSingleList) 
        {
            checkPointSingle.Show();
        }
    }

    public GameObject GetNextCheckPoint(Transform carTransform)
    {
        //�ٽ� üũ����Ʈ 0����
        GameObject nextCheckPoint = checkPointSingleList[nextCheckPointSingleIndex[carTransformList.IndexOf(carTransform)]].gameObject;
        return nextCheckPoint;
    }
}
