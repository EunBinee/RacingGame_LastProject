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
        //잘못된 방향으로 갔을때 UI를 보여준다.
        //Show();
    }
    private void TrackCheck_OnPlayerCorrectCheckPoint(object sender, System.EventArgs e)
    {
        //잘 가고 있으면 UI를 숨긴다.
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
