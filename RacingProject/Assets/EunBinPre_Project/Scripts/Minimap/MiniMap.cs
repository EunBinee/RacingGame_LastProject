using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MiniMap : MonoBehaviour
{
    //�̴ϸ� ��ũ��Ʈ �Դϴ�.

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject trackPath;

    [SerializeField] private GameObject localPlayer;
    public GameObject miniMapCam;

    GameObject[] bicycles;
    List<GameObject> bicyclesPos;


    public Material playerMaterial;
    public Material aiMaterial;


    void Start()
    {
        bicycles = GameObject.FindGameObjectsWithTag("Bicycle");

        GameObject bicyclePos_MiniMap = GameObject.Find("BicyclePos_MiniMap").gameObject;
        bicyclesPos = new List<GameObject>();
        for (int i = 0; i < bicycles.Length; i++) 
        {
            bicyclesPos.Add(bicyclePos_MiniMap.transform.GetChild(i).gameObject);         

        }

        for (int i = 0; i < bicycles.Length; i++)
        {
            bicyclesPos[i].SetActive(true);
            if (bicycles[i].name == "Player")
            {
                bicyclesPos[i].GetComponent<MeshRenderer>().material = playerMaterial;
            }
            else
            {
                bicyclesPos[i].GetComponent<MeshRenderer>().material = aiMaterial;
            }
        }


        lineRenderer = GetComponent<LineRenderer>();
        trackPath = this.gameObject;

        
        int pathNum = trackPath.transform.childCount;  //������ ����  ������ ����
        lineRenderer.positionCount = pathNum + 1;

        for(int i = 0; i < pathNum; i++)
        {
            // lineRenderer.SetPosition(�ε���, vec3) : ���η������� ������ġ�� ���Ѵ�.
            
            Vector3 pos = trackPath.transform.GetChild(i).position;
            lineRenderer.SetPosition(i, new Vector3(pos.x, 20, pos.z));
        }
        lineRenderer.SetPosition(pathNum, lineRenderer.GetPosition(0));
        lineRenderer.startWidth = 20;
        lineRenderer.endWidth = 20;
    }


    void Update()
    {
        for (int i = 0; i < bicycles.Length; i++)
        {
            if (bicycles[i].name == "Player")
            {
                miniMapCam.transform.position = new Vector3(localPlayer.transform.position.x, miniMapCam.transform.position.y, localPlayer.transform.position.z);
            }
            bicyclesPos[i].transform.position = new Vector3(bicycles[i].transform.position.x, bicyclesPos[i].transform.position.y, bicycles[i].transform.position.z);
        }
    }
}
