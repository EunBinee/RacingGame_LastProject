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
        //잘못된 방향으로 갔을때 UI를 보여준다.
        AddReward(-1f);
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //잘 가고 있으면 UI를 숨긴다.
        AddReward(1f);
    }

    public override void OnEpisodeBegin()
    {  
        //에피소드가 시작할때
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
        //장애물
        Vector3 CheckPointForward = trackCheckPoints.GetNextCheckPoint(transform).transform.forward;
        float directionDot = Vector3.Dot(transform.forward, CheckPointForward);  // 현재 객체의 앞과 체크포인트의 앞의 내적을 구해서 각도를 구한다.
        sensor.AddObservation(directionDot); 
        //ai는 체크 포인트 방향으로 가는 방법을 익힙니다.
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
                forwardAmount = 1; //위
                break;
            case 2:
                forwardAmount = -1; //아래
                break;
        }
        switch (actions.DiscreteActions[1])
        {
            case 0:
                turnAmount = 0;
                break;
            case 1:
                turnAmount = 1; //오른쪽
                break;
            case 2:
                turnAmount = -1; //왼쪽
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
        if (Input.GetAxisRaw("Vertical") > 0) //위
            forwardAction = 1;
        if (Input.GetAxisRaw("Vertical") < 0) //아래
            forwardAction = 2;

        int turnAction = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)  //오른쪽
            turnAction = 1;
        if (Input.GetAxisRaw("Horizontal") < 0) //왼쪽
            turnAction = 2;

        ActionSegment<int> discreteAction = actionsOut.DiscreteActions;
        discreteAction[0] = forwardAction;
        discreteAction[1] = turnAction;
    }

    private void OnCollisionEnter(Collision collision)
{
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            //벽에 부딪혔을때
            AddReward(-0.5f);
          //  EndEpisode();
        }
        if (collision.gameObject.TryGetComponent<Goal>(out Goal goal))
        {
            //벽에 부딪혔을때
            AddReward(1.0f);
            EndEpisode();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Wall>(out Wall wall))
        {
            //벽에 부딪혔을때
            AddReward(-0.1f);
            EndEpisode();
        }
    }

}

