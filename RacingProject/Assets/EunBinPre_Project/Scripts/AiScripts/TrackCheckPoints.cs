using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPoints : MonoBehaviour
{
    public EventHandler OnBicycleCorrectCheckPoint;
    public EventHandler OnBicycleWrongCheckPoint;



    [SerializeField] private List<Transform> bicycleTransformList; //������ ����Ʈ




    private List<CheckPointSingle> checkPointSingleList;      // ���� TrackCheckPoints�ȿ� �ִ� CheckPointSingle�� ����

    private List<int> nextCheckPointSingleIndex;                    //���� üũ����Ʈ�� �ε���
                                                                    //(�����Ű� �����ϴ� ���� ����Ʈ ���� // (������ ����Ʈ�� �ε����� ����)



    private void Awake()
    {
        checkPointSingleList = new List<CheckPointSingle>();
        foreach (Transform checkPoint in transform)
        {
            //CheckPoints�� ��� �ڽ��� ��ȯ�Ͽ� ������ �´�.
            CheckPointSingle checkPointSingle = checkPoint.GetComponent<CheckPointSingle>();
            checkPointSingle.SetTrackCheckPoint(this);
            checkPointSingleList.Add(checkPointSingle);
        }

        nextCheckPointSingleIndex = new List<int>();
        foreach (Transform carTransform in bicycleTransformList)
        {
            nextCheckPointSingleIndex.Add(0);   //��� �������� ó�� üũ ����Ʈ�� 0����
        }
    }

    public void CarThoughCheckPoint(CheckPointSingle checkPointSingle, Transform carTransform)
    {
        //�����Ű� CheckPointSingle�� �΋H����, CarThoughCheckPoint�� ȣ���Ͽ�
        //���� �����Ű� �΋H�� checkpoint(wayPoint)�� ���� �΋H�� �����Ÿ� �Ű������� ���ؼ� �˷��ش�.

        int nextCheckPointSingle_Index = nextCheckPointSingleIndex[bicycleTransformList.IndexOf(carTransform)];

        //����� üũ����Ʈ�� �´� �������� Ȯ���ϴ� �Լ�
        int curCheckPoint_Index = checkPointSingleList.IndexOf(checkPointSingle);   //���� ����� üũ����Ʈ�� �ε��� ��ȣ
        Debug.Log(curCheckPoint_Index);

        if (checkPointSingleList.IndexOf(checkPointSingle) == nextCheckPointSingle_Index)
        {
            //�°� ����ߴٸ�..
            Debug.Log("�˸°� ���!");

            CheckPointSingle correctCheckPointSingle = checkPointSingleList[nextCheckPointSingle_Index];
            //�´� üũ����Ʈ�� ����ϸ� CheckPointSing�� ���� ������ �´�.
            correctCheckPointSingle.Hide();
            nextCheckPointSingleIndex[bicycleTransformList.IndexOf(carTransform)] = (nextCheckPointSingle_Index + 1) % checkPointSingleList.Count; //�������� ���� ��쵵 �����ϱ�.. ����..
            OnBicycleCorrectCheckPoint?.Invoke(this, EventArgs.Empty);    //Null ���� ���ܸ� ���ϱ� ���ؼ�...
        }
        else
        {
            //Ʋ���� �����.
            Debug.Log("Ʋ�� ���̴�.!");
            OnBicycleWrongCheckPoint?.Invoke(this, EventArgs.Empty); //Null ���� ���ܸ� ���ϱ� ���ؼ�...

            //Ʋ���� ����ϸ�, Ʋ�� üũ ����Ʈ wrongCheckPointSingle�� �־��ְ�, �ٽ� �����ش�.
            //�׸��� ���� üũ����Ʈ�� �ڷ� ��ĭ ������.

            CheckPointSingle wrongCheckPointSingle = checkPointSingleList[curCheckPoint_Index]; 
            nextCheckPointSingleIndex[bicycleTransformList.IndexOf(carTransform)] = (nextCheckPointSingle_Index - 1) % checkPointSingleList.Count;

            wrongCheckPointSingle.Show();
        }

    }

    public void ResetCheckPoint(Transform bicycleTransform)
    {
        //�ٽ� üũ����Ʈ 0����

        nextCheckPointSingleIndex[bicycleTransformList.IndexOf(bicycleTransform)] = 0;


        foreach (CheckPointSingle checkPointSingle in checkPointSingleList)
        {
            checkPointSingle.Show();
        }
    }

    public CheckPointSingle GetNextCheckPoint(Transform bicycleTransform)
    {
        //���� üũ ����Ʈ�� gameObject�� �ѱ��.
        CheckPointSingle nextCheckPoint = checkPointSingleList[nextCheckPointSingleIndex[bicycleTransformList.IndexOf(bicycleTransform)]];
        return nextCheckPoint;
    }
    public int CurCheckPointIndex(Transform bicycleTransform)
    {
        //���� üũ ����Ʈ�� gameObject�� �ѱ��.
        int curIndex = (nextCheckPointSingleIndex[bicycleTransformList.IndexOf(bicycleTransform)] - 1);
        return curIndex;
    }
}
