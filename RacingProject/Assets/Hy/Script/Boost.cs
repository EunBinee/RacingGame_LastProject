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
    [SerializeField] private float maxTime = 3;         //5�ʵ��� �ν���
    private float fixTime = 3;      //������ �ð�

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
                Debug.Log("�ð� ��");
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
                Debug.Log("�ð��߰�");
                time = 0;
            }
            else
            {
                Debug.Log("�޸���!");
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