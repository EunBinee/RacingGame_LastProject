using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;
using Random = UnityEngine.Random;

public class CarDriverAgent : Agent
{
    [SerializeField] private TrackCheckPoints trackCheckPoints;
    [SerializeField] private Transform spawnPosition;

    private ScrCarController carController;
    [SerializeField] private ScrWheel[] Wheels;

    void Start()
    {
        carController = GetComponent<ScrCarController>();
    }


    void Update()
    {
        trackCheckPoints.OnBicycleCorrectCheckPoint += TrackCheck_OnPlayerWrongCheckPoint;
        trackCheckPoints.OnBicycleWrongCheckPoint += TrackCheck_OnPlayerCorrectCheckPoint;
    }

    private void TrackCheck_OnPlayerWrongCheckPoint(object sender, System.EventArgs e)
    {
        //�߸��� �������� ������ UI�� �����ش�.
        AddReward(-1f);
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //�� ���� ������ UI�� �����.
        AddReward(1f);
    }

    public override void OnEpisodeBegin()
    {  
        //���Ǽҵ尡 �����Ҷ�
        transform.position = spawnPosition.position + new Vector3(Random.Range(-3, 2), 0, Random.Range(-4.5f, 4.5f));
        transform.forward = spawnPosition.forward;
        trackCheckPoints.ResetCheckPoint(transform);
        
        carController.StopWheelCompletely_carcontroller();
        foreach (var wheel in Wheels)
        {
            wheel.StopWheelCompletely();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //��ֹ�
        Vector3 CheckPointForward = trackCheckPoints.GetNextCheckPoint(transform).transform.forward;
        float directionDot = Vector3.Dot(transform.forward, CheckPointForward);  // ���� ��ü�� �հ� üũ����Ʈ�� ���� ������ ���ؼ� ������ ���Ѵ�.
        sensor.AddObservation(directionDot); 
        //ai�� üũ ����Ʈ �������� ���� ����� �����ϴ�.
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float forwardAmount = 0;    //Vertical
        float turnAmount = 0;          //Horizontal

        switch(actions.DiscreteActions[0])
        {
            case 0:
                forwardAmount = 0;
                break;
            case 1:
                forwardAmount = 1; //��
                break;
            case 2:
                forwardAmount = -1; //�Ʒ�
                break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0:
                turnAmount = 0;
                break;
            case 1:
                turnAmount = 1; //������
                break;
            case 2:
                turnAmount = -1; //����
                break;
        }
        
        carController.steerInput = turnAmount ;
        foreach(var wheel in Wheels)
        {
            wheel.speedInput = forwardAmount;
        }

    }
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        if (Input.GetAxisRaw("Vertical") > 0) //��
            forwardAction = 1;
        if (Input.GetAxisRaw("Vertical") < 0) //�Ʒ�
            forwardAction = 2;

        int turnAction = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)  //������
            turnAction = 1;
        if (Input.GetAxisRaw("Horizontal") < 0) //����
            turnAction = 2;

        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;
        discreteAction[0] = forwardAction;
        discreteAction[1] = turnAction;
    }

    private void OnCollisionEnter(Collision collision)
{
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            //���� �ε�������
            AddReward(-0.5f);
          //  EndEpisode();
        }
        if (collision.gameObject.TryGetComponent<Goal>(out Goal goal))
        {
            //���� �ε�������
            AddReward(1.0f);
            EndEpisode();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            //���� �ε�������
            AddReward(-0.1f);
            EndEpisode();
        }
    }

}

