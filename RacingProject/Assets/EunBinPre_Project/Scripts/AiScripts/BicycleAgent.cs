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
    }
    void Update()
    {
        trackCheckPoints.OnBicycleCorrectCheckPoint += TrackCheck_OnPlayerCorrectCheckPoint;
        trackCheckPoints.OnBicycleWrongCheckPoint += TrackCheck_OnPlayerWrongCheckPoint;
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //잘 가고 있으면 상을 줌
        AddReward(1f);
    }
    private void TrackCheck_OnPlayerWrongCheckPoint(object sender, System.EventArgs e)
    {
        //잘못된 방향으로 가면 상을 뺌
        AddReward(-1f);
    }


    public override void OnEpisodeBegin()
    {
        //에피소드가 시작할때
        //1. Ai 로컬 위치

        //transform.position = spawnPosition.position + new Vector3(Random.Range(-3, 2), 0, Random.Range(-4.5f, 4.5f));

        //2. Ai forward 위치 
        //transform.forward = spawnPosition.forward;

        //3. trackCheckPointstrackCheckPoints.ResetCheckPoint(transform); => NextCheckPoint 위치 변경
       // trackCheckPoints.ResetCheckPoint(transform);
        

        //초기화 할 거 다 초기화 시키기
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        //장애물 체크

        //1. 체크 포인트
        Vector3 CheckPointForward = trackCheckPoints.GetNextCheckPoint(transform).transform.forward;
        float directionDot = Vector3.Dot(transform.forward, CheckPointForward);  // 현재 객체의 앞과 체크포인트의 앞의 내적을 구해서 각도를 구한다.
        sensor.AddObservation(directionDot);
        //ai는 체크 포인트 방향으로 가는 방법을 익힙니다.


        //2. 부스터의 위치를 익힌다.
        //-> 체크 포인트랑 같은 맥락으로 가면 될 듯
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
            //ai 방향 체크 
            

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        //직접 이동할 수 있도록.

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Boost>(out Boost boost))
        {
            //부스터에 들어갔을 때
            AddReward(1f);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<Boost>(out Boost boost))
        {
            //부스터에 계속 들어가있을 때
            AddReward(0.1f);
        }
    }


}
