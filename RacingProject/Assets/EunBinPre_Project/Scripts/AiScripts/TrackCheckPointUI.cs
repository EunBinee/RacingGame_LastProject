using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPointUI : MonoBehaviour
{
    [SerializeField] private TrackCheckPoints trackCheckPoints;
    [SerializeField] private GameObject warningUI;

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
        warningUI.SetActive(true);
    }
    private void Hide()
    {
        warningUI.SetActive(false);
    }

}
