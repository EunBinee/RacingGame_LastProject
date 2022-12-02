using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSingle : MonoBehaviour
{
    private TrackCheckPoints trackCheckPoints;
    private MeshRenderer MeshR;



    [Tooltip("해설이 나올 체크포인트인지.")]
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
            //만약 해설이 있는 체크 박스이면?

            if(other.gameObject.name =="Player")
            {
                //위에 if문 나중에 UserName으로 변경하기

                GameManager.GetInstance().GetDialogue(checkPointNumber);


                haveDialogue = false;   //한번 실행되면 이제 실행안하도록
                
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
