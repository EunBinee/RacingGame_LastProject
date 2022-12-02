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
        Player = GameObject.FindGameObjectWithTag("Bicycle");
        ct = Player.GetComponent<BicycleController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bicycle"))
        {
            ct.sprint = true;
            Invoke("delay", 3f);
            //Debug.Log("sads");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bicycle"))
        {
            //ct.sprint = false;
            //Invoke("delay", 3f);
            //Debug.Log("sads");
        }
    }

    private void delay()
    {
        ct.sprint = false;
        ct.currentSpeed -= 10;
        CancelInvoke();
        //Debug.Log("del");

    }
}
