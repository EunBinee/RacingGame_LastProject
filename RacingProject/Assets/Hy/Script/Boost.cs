using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private GameObject Player;
    private BicycleController ct;

    [SerializeField] private bool isBoost = false;

    [SerializeField] private float time = 0;
    [SerializeField] private float maxTime = 3;         //5초동안 부스터
    private float fixTime = 3;      //고정된 시간

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Bicycle");
        ct = Player.GetComponent<BicycleController>();
    }


    private void Update()
    {
        if (isBoost)
        {
            if (time > maxTime)
            {
                Debug.Log("시간 끝");
                time = 0;
                maxTime = fixTime;
                ct.sprint = false;
                isBoost = false;
            }
            else
            {
                time += Time.deltaTime;
                ct.sprint = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wheel"))
        {
            if (isBoost)
            {
                Debug.Log("시간추가");
                time = 0;
            }
            else
            {
                Debug.Log("달린다!");
                isBoost = true;
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Wheel"))
        {

        }
    }

}