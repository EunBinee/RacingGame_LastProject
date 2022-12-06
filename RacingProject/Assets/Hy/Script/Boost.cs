using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boost : MonoBehaviour
{
    private GameObject Player;
    private BicycleController ct;
    private int countBoost=0;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Bicycle");
        ct = Player.GetComponent<BicycleController>();
    }
    void Update()
    {
        Booster();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bicycle"))
        {
            countBoost += 1;
            //ct.sprint = true;
            //Invoke("delay", 3f);
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
        //CancelInvoke();
        //Debug.Log("del");
    }
    private void Booster()
    {
        if (countBoost > 0)
        {
            //for()
            //{
            //    ct.sprint = true;
            //    //Invoke("delay", 3f);
            //}
        }
        else
        {
            //CancelInvoke();
        }
    }
}
