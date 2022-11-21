using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrCarController : MonoBehaviour
{
    public bool isAI;

    public ScrWheel[] wheels;   //바퀴들

    [Header("Car Specs")]
    public float wheelBase;       // in meter
    public float rearTrack;         // in meter
    public float turnRadius;       // in meter

    [Header("Inputs")]
    public float steerInput;        // 좌우 키 입력

    [SerializeField] private float AckermannAngleLeft;       // 앞 왼쪽 바퀴의 각도
    [SerializeField] private float AckermannAngleRight;     // 앞 오른쪽 바퀴의 각도

    void Update()
    {
        if(isAI)
        {

        }
        else
        {
            steerInput = Input.GetAxis("Horizontal");
        }

        if (steerInput > 0)           // is turning right
        {
            AckermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
            AckermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
        }
        else if (steerInput < 0)  // is turning left
        {
            AckermannAngleLeft = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack / 2))) * steerInput;
            AckermannAngleRight = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack / 2))) * steerInput;
        }
        else                              // 움직이지않는다.
        {
            AckermannAngleLeft = 0;
            AckermannAngleRight = 0;
        }

        foreach (ScrWheel w in wheels)
        {
            if(w.wheelFrontRight)
            {
                w.steerAngle = AckermannAngleRight;
            }
            if(w.wheelFrontLeft)
            {
                w.steerAngle = AckermannAngleLeft;
            }
        }

    }


    public void StopWheelCompletely_carcontroller()
    {
        AckermannAngleRight = 0;
        AckermannAngleLeft = 0;
    }

}
