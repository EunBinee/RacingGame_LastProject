using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSingle : MonoBehaviour
{
    private TrackCheckPoints trackCheckPoints;
    private MeshRenderer MeshR;



    [Tooltip("�ؼ��� ���� üũ����Ʈ����.")]
    public bool haveDialogue;
    public int checkPointNumber;

    private void Start()
    {
        MeshR = GetComponent<MeshRenderer>();   
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.CompareTag("Bicycle"))
        {
            trackCheckPoints.CarThoughCheckPoint(this, other.transform);
        }

        if(haveDialogue)
        {
            //���� �ؼ��� �ִ� üũ �ڽ��̸�?

            if(other.gameObject.name =="Player")
            {
                //���� if�� ���߿� UserName���� �����ϱ�

                GameManager.GetInstance().GetDialogue(checkPointNumber);


                haveDialogue = false;   //�ѹ� ����Ǹ� ���� ������ϵ���
                
            }
        }
    }
    public void SetTrackCheckPoint(TrackCheckPoints trackCheckPoints)
    {
        this.trackCheckPoints = trackCheckPoints;
    }

    public void Hide()
    {
        MeshR.enabled = false;
    }
    public void Show()
    {
        MeshR.enabled = true;
    }
}
