using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSingle : MonoBehaviour
{
    private TrackCheckPoints trackCheckPoints;
    private MeshRenderer MeshR;
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
