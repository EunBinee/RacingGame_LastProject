using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrWheel : MonoBehaviour
{
    public bool isAI;

    Rigidbody rb;

    [Header("IsBicycle")]
    public bool bicycleFWheel;

    [Header("IsCar")]
    public bool wheelFrontRight;    //어느 쪽 바퀴인지 체크
    public bool wheelFrontLeft;
    public bool wheelRearRight;
    public bool wheelRearLeft;

    [Header("Suspension")]
    public float restLength;            //springTravel값이 안들어간.. 늘어나기전 스프링의 길이
    public float springTravel;         //스프링이 위아래로 얼마나 흔들릴 수 있는지
    public float springStiffness;     //스프링이 늘어지고 압축될 때의 저항 값
                                                    //Stiffness : 물체가 변형에 저항하는 정도
    public float damperStiffness;   //댐퍼의 저항값
                                                    //Damper란? 진동에너지를 흡수하는 장치. 진동같은 움직임을 제어하고 차단한다.

    private float minLength;          //스프링의 최소 길이
    private float maxLength;         //스프링의 최대 길이
    private float lastLength;          //이전 프레임(마지막)의 스프링의 길이
    private float springLength;      //현재 스프링의 길이 (계속 업데이트 되며 바뀐다.)
    private float springVelocity;     //스프링이 움직이는 속도
    private float springForce;        //스프링의 힘
    private float damperForce;      //Damper의 힘

    private Vector3 suspensionForce;   //springForce의 힘 + 힘이 가해질 방향 | 스프링의 최종 힘의 값

    [Header("Wheel")]
    public float wheelRadius;       //바퀴의 반지름. 바퀴는 정적메쉬기 때문에 변하지 않는다.
    private Vector3 wheelVelocityLs;  //바퀴의 속도( Ls는 LocalSpace)
    private float fX;
    private float fY;

    public float steerAngle;         //바퀴의 각도 (CarController에서 받아옴)
    public float wheelAngle;
    public float sterrTime;

    [Header("Inputs")]
    public float speedInput;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();  //자식쪽에서 부모를 찾는 법. transform.root를 하면 부모를 찾아준다.

        minLength   = restLength - springTravel;                //스프링의 최소값과 최대값.
        maxLength  = restLength + springTravel;

    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, sterrTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);   //바퀴의 방향을 바꾼다.

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.yellow);
    }

    void FixedUpdate() //물리값은 FixedUpdate에서 진행한다.
    {

        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength+wheelRadius ))
        {
            
            lastLength           = springLength;
            springLength       = hit.distance - wheelRadius;  //현재 스프링의 길이
            springLength       = Mathf.Clamp(springLength, minLength, maxLength);
            //스프링의길이가 minLength보다 작아지면 minLength 값을, maxLength보다 커지면 maxLength 값을 넣어준다.
            springVelocity     = (lastLength - springLength) / Time.fixedDeltaTime;
           
            damperForce      = damperStiffness * springVelocity;
            springForce         = springStiffness * (restLength - springLength);
            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelocityLs = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            //rigidbody.GetPointVelocity                  =>한 지점의 속도
            //transform.InverseTransformDirection =>월드좌표를 이 스크립트를 가진 객체의 로컬좌표로 바꾸어준다.
            if (isAI)
            {

            }
            else
                speedInput = Input.GetAxis("Vertical");

            fX = speedInput * springForce * 0.5f;
            fY = wheelVelocityLs.x * springForce;   //wheelVeloctyLs의 x는 어느쪽으로 꺽어야하는지다.
            rb.AddForceAtPosition(suspensionForce + (fX * transform.forward) + (fY * -transform.right), hit.point);
        }
        else
        {
            springLength = maxLength;
        }


    }


    public void StopWheelCompletely()
    {
        rb.velocity = Vector3.zero;
        wheelVelocityLs = Vector3.zero;
        steerAngle = 0;
        wheelAngle = 0;
        suspensionForce = Vector3.zero;
    }
}
