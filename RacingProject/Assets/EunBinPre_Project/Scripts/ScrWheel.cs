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
    public bool wheelFrontRight;    //��� �� �������� üũ
    public bool wheelFrontLeft;
    public bool wheelRearRight;
    public bool wheelRearLeft;

    [Header("Suspension")]
    public float restLength;            //springTravel���� �ȵ�.. �þ���� �������� ����
    public float springTravel;         //�������� ���Ʒ��� �󸶳� ��鸱 �� �ִ���
    public float springStiffness;     //�������� �þ����� ����� ���� ���� ��
                                                    //Stiffness : ��ü�� ������ �����ϴ� ����
    public float damperStiffness;   //������ ���װ�
                                                    //Damper��? ������������ ����ϴ� ��ġ. �������� �������� �����ϰ� �����Ѵ�.

    private float minLength;          //�������� �ּ� ����
    private float maxLength;         //�������� �ִ� ����
    private float lastLength;          //���� ������(������)�� �������� ����
    private float springLength;      //���� �������� ���� (��� ������Ʈ �Ǹ� �ٲ��.)
    private float springVelocity;     //�������� �����̴� �ӵ�
    private float springForce;        //�������� ��
    private float damperForce;      //Damper�� ��

    private Vector3 suspensionForce;   //springForce�� �� + ���� ������ ���� | �������� ���� ���� ��

    [Header("Wheel")]
    public float wheelRadius;       //������ ������. ������ �����޽��� ������ ������ �ʴ´�.
    private Vector3 wheelVelocityLs;  //������ �ӵ�( Ls�� LocalSpace)
    private float fX;
    private float fY;

    public float steerAngle;         //������ ���� (CarController���� �޾ƿ�)
    public float wheelAngle;
    public float sterrTime;

    [Header("Inputs")]
    public float speedInput;

    void Start()
    {
        rb = transform.root.GetComponent<Rigidbody>();  //�ڽ��ʿ��� �θ� ã�� ��. transform.root�� �ϸ� �θ� ã���ش�.

        minLength   = restLength - springTravel;                //�������� �ּҰ��� �ִ밪.
        maxLength  = restLength + springTravel;

    }

    void Update()
    {
        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, sterrTime * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);   //������ ������ �ٲ۴�.

        Debug.DrawRay(transform.position, -transform.up * (springLength + wheelRadius), Color.yellow);
    }

    void FixedUpdate() //�������� FixedUpdate���� �����Ѵ�.
    {

        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLength+wheelRadius ))
        {
            
            lastLength           = springLength;
            springLength       = hit.distance - wheelRadius;  //���� �������� ����
            springLength       = Mathf.Clamp(springLength, minLength, maxLength);
            //�������Ǳ��̰� minLength���� �۾����� minLength ����, maxLength���� Ŀ���� maxLength ���� �־��ش�.
            springVelocity     = (lastLength - springLength) / Time.fixedDeltaTime;
           
            damperForce      = damperStiffness * springVelocity;
            springForce         = springStiffness * (restLength - springLength);
            suspensionForce = (springForce + damperForce) * transform.up;

            wheelVelocityLs = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
            //rigidbody.GetPointVelocity                  =>�� ������ �ӵ�
            //transform.InverseTransformDirection =>������ǥ�� �� ��ũ��Ʈ�� ���� ��ü�� ������ǥ�� �ٲپ��ش�.
            if (isAI)
            {

            }
            else
                speedInput = Input.GetAxis("Vertical");

            fX = speedInput * springForce * 0.5f;
            fY = wheelVelocityLs.x * springForce;   //wheelVeloctyLs�� x�� ��������� ������ϴ�����.
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
