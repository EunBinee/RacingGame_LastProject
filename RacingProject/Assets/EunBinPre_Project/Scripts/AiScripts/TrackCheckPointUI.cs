using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCheckPointUI : MonoBehaviour
{
    [SerializeField] private TrackCheckPoints trackCheckPoints;
    [SerializeField] private GameObject warningUI;

    void Start()
    {
    }

    private void TrackCheck_OnPlayerWrongCheckPoint(object sender, System.EventArgs e)
    {
        //�߸��� �������� ������ UI�� �����ش�.
        //Show();
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //�� ���� ������ UI�� �����.
        Hide();
    }


    public void Show()
    {
        warningUI.SetActive(true);
    }
    public void Hide()
    {
        warningUI.SetActive(false);
    }

}
