using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class MiniMap : MonoBehaviour
{
    //�̴ϸ� ��ũ��Ʈ �Դϴ�.

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject trackPath;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        trackPath = this.gameObject;

        
        int pathNum = trackPath.transform.childCount;  //������ ����  ������ ����
        lineRenderer.positionCount = pathNum + 1;

        for(int i = 0; i < pathNum; i++)
        {
            // lineRenderer.SetPosition(�ε���, vec3) : ���η������� ������ġ�� ���Ѵ�.
            
            Vector3 pos = trackPath.transform.GetChild(i).position;
            lineRenderer.SetPosition(i, new Vector3(pos.x, 100, pos.z));
        }
        lineRenderer.SetPosition(pathNum, lineRenderer.GetPosition(0));
        lineRenderer.startWidth = 20;
        lineRenderer.endWidth = 20;
    }


    void Update()
    {
        
    }
}
