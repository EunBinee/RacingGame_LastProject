using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
public class ResultImg : MonoBehaviour
{

    public enum Img
    {
        PlayerFinish,                       //플레이어가 1등으로 들어온 경우
        PlayerPass_rankIn,              //ai가 먼저 들어왔지만 플레이어가 랭킹으로 들어온 경우
        PlayerPass_rankInNot,       //ai가 먼저 들어왔고, 플레이어가 랭킹에 못든 경우 ()
        PlayerNotPass                   // ai가 먼저 들어왔고, 플레이어가 아예 통과를 못한 상황
    }

    //Image 
    [SerializeField] Sprite[] resultImg;
    [SerializeField] Image resultImgPanel;


    public Img curImg = Img.PlayerFinish;

    //결과 창--------------------------------------------------------------------------------------
    [SerializeField] GameObject resultPanelGameObject;

    void Start()
    {
        resultImgPanel.sprite = resultImg[0];
    }
    public void ChangeImg(Img imgString)
    {
        curImg = imgString;

        switch (imgString)
        {
            case Img.PlayerFinish:
                resultImgPanel.sprite = resultImg[0];
                Invoke("ShowResultPanel", 1f);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = true;
                break;

            case Img.PlayerPass_rankIn:
                resultImgPanel.sprite = resultImg[1];
                Invoke("ShowResultPanel", 1f);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = true;
                break;

            case Img.PlayerPass_rankInNot:
                resultImgPanel.sprite = resultImg[2];
                Invoke("ShowResultPanel", 1f);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = true;
                break;

            case Img.PlayerNotPass:
                resultImgPanel.sprite = resultImg[3];
                Invoke("ShowResultPanel", 1f);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = false;
                break;

            default:
                break;
        }
    }


    void ShowResultPanel()
    {
        resultPanelGameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
