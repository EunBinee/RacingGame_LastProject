using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPointUI : MonoBehaviour
{
    [SerializeField] private TrackCheckPoints trackCheckPoints;

    void Start()
    {
        trackCheckPoints.OnBicycleCorrectCheckPoint += TrackCheck_OnPlayerCorrectCheckPoint;
        trackCheckPoints.OnBicycleWrongCheckPoint += TrackCheck_OnPlayerWrongCheckPoint;
    }

    private void TrackCheck_OnPlayerWrongCheckPoint(object sender, System.EventArgs e)
    {
        //�߸��� �������� ������ UI�� �����ش�.
        Show();
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //�� ���� ������ UI�� �����.
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
