using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MiniMap : MonoBehaviour
{
    //미니맵 스크립트 입니다.

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject trackPath;

    public GameObject miniMapCam;

   public  GameObject[] bicycles;
    List<GameObject> bicyclesPos;


    public Material playerMaterial;
    public Material aiMaterial;


    void Start()
    {
        bicycles = GameObject.FindGameObjectsWithTag("Bicycle");

        GameObject bicyclePos_MiniMap = GameObject.Find("BicyclePos_MiniMap");
        Debug.Log("아");
        bicyclesPos = new List<GameObject>();

 

        for (int i = 0; i < bicycles.Length; i++) 
        {
            bicyclesPos.Add(bicyclePos_MiniMap.transform.GetChild(i).gameObject);
        }


        string playerName = PlayerPrefs.GetString("CurrentPlayerName");
        for (int i = 0; i < bicycles.Length; i++)
        {
            bicyclesPos[i].SetActive(true);
            if (bicycles[i].name == playerName)
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

        
        int pathNum = trackPath.transform.childCount;  //라인을 이을  점들의 개수
        lineRenderer.positionCount = pathNum + 1;

        for(int i = 0; i < pathNum; i++)
        {
            // lineRenderer.SetPosition(인덱스, vec3) : 라인렌더러의 시작위치를 정한다.
            
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
            if (bicycles[i].name == GameManager.GetInstance().playerName)
            {
                miniMapCam.transform.position = new Vector3(bicycles[i].transform.position.x, miniMapCam.transform.position.y, bicycles[i].transform.position.z);
            }
            bicyclesPos[i].transform.position = new Vector3(bicycles[i].transform.position.x, bicyclesPos[i].transform.position.y, bicycles[i].transform.position.z);
        }
    }
}
