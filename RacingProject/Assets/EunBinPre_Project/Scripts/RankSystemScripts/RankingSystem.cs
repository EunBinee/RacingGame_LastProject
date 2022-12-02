using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class RankingSystem : MonoBehaviour
{
    //�÷��̾�� Ai�� ����� �����ϱ� ���� ��ũ��Ʈ�Դϴ�.
    //����� �Ÿ�����, ���� ��������Ҵ��� ����, ���� ���° üũ ����Ʈ�� �ִ��� ���θ� ���Ͽ� GameManger���� �����մϴ�.

    [SerializeField]TrackCheckPoints trackCheckPoints;          //��� üũ����Ʈ�� ������ ���ִ�.

    public float counter = 0;
    public int rank = 0;

    [SerializeField] int lapCount = 0; //���� ����� ���Ҵ���
    public int curCheckPoint_index;
    public CheckPointSingle nextCheckPoint;


    void Awake()
    {
        
    }

    private void Start()
    {
        nextCheckPoint = trackCheckPoints.GetNextCheckPoint(transform);               // �Ÿ��� ��� ���ؼ� transform �ʿ�
        curCheckPoint_index = trackCheckPoints.CurCheckPointIndex(transform);     // ���� ���� �ִ� üũ ����Ʈ�� �ε���
    }
    private void Update()
    {
        DistanceCalculation();
    }

    private void DistanceCalculation()
    {

        float distance = Vector3.Distance(transform.position, nextCheckPoint.transform.position);

        counter = lapCount * 1000 + curCheckPoint_index * 100 - distance;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            //���� CheckPoint�� �����ٸ�..
            //curCheckPoint�� �ε�����
            //������ �������ϴ� NextCheckPoint�� transform�� �޾ƿ´�. 

            nextCheckPoint = trackCheckPoints.GetNextCheckPoint(transform);     //�Ÿ��� ��� ���ؼ� transform �ʿ�
            curCheckPoint_index = trackCheckPoints.CurCheckPointIndex(transform);

        }
    }

}
