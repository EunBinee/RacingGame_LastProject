using SBPScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player;
    private BicycleController ct;
    private GameObject cameraConstarint;
    private GameObject cameralookAt;
    public float speed = 0;
    public float defaltFOV = 5, desiredFOV = 0;
    [Range(0, 5)] public float smothTime = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        cameraConstarint = Player.transform.Find("cameraConstarint").gameObject;
        cameralookAt = Player.transform.Find("cameralookAt").gameObject;
        ct = Player.GetComponent<BicycleController>();
        defaltFOV = Camera.main.fieldOfView;
    }

    private void FixedUpdate()
    {
        follow();
        boostFOV();
    }
    private void follow()
    {
        if (speed <= 3)
            speed = Mathf.Lerp(speed, ct.currentSpeed / 2, Time.deltaTime);
        else
            speed = 3;

        gameObject.transform.position = Vector3.Lerp(transform.position, cameraConstarint.transform.position, Time.deltaTime * speed);
        gameObject.transform.LookAt(cameralookAt.gameObject.transform.position);
    }
    public void boostFOV()
    {
        if (ct.sprint)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, desiredFOV, Time.deltaTime * smothTime);
        }
        else if(ct.sprint==false)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, defaltFOV, Time.deltaTime * smothTime);
        }
    }
}
