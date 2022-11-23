using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private GameObject Player;
    private BicycleController ct;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        ct = Player.GetComponent<BicycleController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ct.sprint = true;
            Invoke("delay", 4f);
        }

    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        ct.sprint = false;
    //    }
    //}

    private void delay()
    {
        ct.sprint = false;
        ct.currentSpeed -= 10;

    }
}
