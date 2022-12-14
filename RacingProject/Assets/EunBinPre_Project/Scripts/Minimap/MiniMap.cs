using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MiniMap : MonoBehaviour
{
    //�̴ϸ� ��ũ��Ʈ �Դϴ�.

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
        Debug.Log("��");
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
            if (bicycles[i].name == GameManager.GetInstance().playerName)
            {
                miniMapCam.transform.position = new Vector3(bicycles[i].transform.position.x, miniMapCam.transform.position.y, bicycles[i].transform.position.z);
            }
            bicyclesPos[i].transform.position = new Vector3(bicycles[i].transform.position.x, bicyclesPos[i].transform.position.y, bicycles[i].transform.position.z);
        }
    }
}
