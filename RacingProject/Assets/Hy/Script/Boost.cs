using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class Boost : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wheel"))
        {
            BicycleController otherBC = other.transform.parent.gameObject.GetComponent<BicycleController>();
            if (otherBC.isBoost)
            {
              //  Debug.Log(otherBC.name + "시간추가");
                otherBC.time = 0;
            }
            else
            {
                otherBC.isBoost = true;


              //  Debug.Log(otherBC.name + "의 부스터 시작!");
            }

        }
    }




}