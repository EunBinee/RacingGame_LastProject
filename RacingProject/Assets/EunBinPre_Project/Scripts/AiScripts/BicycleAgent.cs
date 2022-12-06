using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;
using Random = UnityEngine.Random;
using SBPScripts;

public class BicycleAgent : Agent
{
    [SerializeField] private TrackCheckPoints trackCheckPoints;
    [SerializeField] private Transform spawnPosition;

    private BicycleController bicycleController;


    void Start()
    {
        bicycleController = GetComponent<BicycleController>();
        spawnPosition = GameObject.Find("SpawnPoint").transform;
    }
    void Update()
    {
        trackCheckPoints.OnBicycleCorrectCheckPoint += TrackCheck_OnPlayerCorrectCheckPoint;
        trackCheckPoints.OnBicycleWrongCheckPoint += TrackCheck_OnPlayerWrongCheckPoint;
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //�� ���� ������ ���� ��
        AddReward(1f);
    }
    private void TrackCheck_OnPlayerWrongCheckPoint(object sender, System.EventArgs e)
    {
        //�߸��� �������� ���� ���� ��
        AddReward(-1f);
    }


    public override void OnEpisodeBegin()
    {
        //���Ǽҵ尡 �����Ҷ�
        //1. Ai ���� ��ġ

        transform.position = spawnPosition.position + new Vector3(Random.Range(-3, 2), 0, Random.Range(-4.5f, 4.5f));

        //2. Ai forward ��ġ 
        transform.forward = spawnPosition.forward;

        //3. trackCheckPointstrackCheckPoints.ResetCheckPoint(transform); => NextCheckPoint ��ġ ����
        trackCheckPoints.ResetCheckPoint(transform);


        //�ʱ�ȭ �� �� �� �ʱ�ȭ ��Ű��
        bicycleController.AiSetting();
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        //��ֹ� üũ

        //1. üũ ����Ʈ
        Vector3 CheckPointForward = trackCheckPoints.GetNextCheckPoint(transform).transform.forward;
        float directionDot = Vector3.Dot(transform.forward, CheckPointForward);  // ���� ��ü�� �հ� üũ����Ʈ�� ���� ������ ���ؼ� ������ ���Ѵ�.
        sensor.AddObservation(directionDot);
        //ai�� üũ ����Ʈ �������� ���� ����� �����ϴ�.


        //2. �ν����� ��ġ�� ������.
        //-> üũ ����Ʈ�� ���� �ƶ����� ���� �� ��


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //ai ���� üũ 
        float forwardAmount = 0;    //Vertical
        float turnAmount = 0;          //Horizontal

        switch (actions.DiscreteActions[0])
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



        bicycleController.vertical_Input = forwardAmount;
        bicycleController.horizontal_Input = turnAmount;

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //���� �̵��� �� �ֵ���.
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
           // EndEpisode();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            //���� �ε�������
            AddReward(-0.1f);
            //EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Boost>(out Boost boost))
        {
            //�ν��Ϳ� ���� ��
            AddReward(1f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Boost>(out Boost boost))
        {
            //�ν��Ϳ� ��� ������ ��
            AddReward(0.1f);
        }
    }


}