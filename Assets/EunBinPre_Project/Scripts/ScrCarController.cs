using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrCarController : MonoBehaviour
{
    public bool isAI;

    public ScrWheel[] wheels;   //������

    [Header("Car Specs")]
    public float wheelBase;       // in meter
    public float rearTrack;         // in meter
    public float turnRadius;       // in meter

    [Header("Inputs")]
    public float steerInput;        // �¿� Ű �Է�

    [SerializeField] private float AckermannAngleLeft;       // �� ���� ������ ����
    [SerializeField] private float AckermannAngleRight;     // �� ������ ������ ����

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
        else                              // ���������ʴ´�.
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
