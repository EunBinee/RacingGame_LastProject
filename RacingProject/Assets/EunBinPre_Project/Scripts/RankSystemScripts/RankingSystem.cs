using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    //�÷��̾�� Ai�� ����� �����ϱ� ���� ��ũ��Ʈ�Դϴ�.
    //����� �Ÿ�����, ���� ��������Ҵ��� ����, ���� ���° üũ ����Ʈ�� �ִ��� ���θ� ���Ͽ� GameManger���� �����մϴ�.

    [SerializeField]TrackCheckPoints trackCheckPoints;          //��� üũ����Ʈ�� ������ ���ִ�.
    
    int curCheckPoint_index;
    CheckPointSingle nextCheckPoint;

    void Awake()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CheckPoint"))
        {
            //���� CheckPoint�� �����ٸ�..
            //curCheckPoint�� �ε�����
            //������ �������ϴ� NextCheckPoint�� transform�� �޾ƿ´�. 

            

        }
    }

}
