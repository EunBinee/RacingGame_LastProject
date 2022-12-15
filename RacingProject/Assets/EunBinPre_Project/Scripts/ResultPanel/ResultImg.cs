using UnityEngine;
using UnityEngine.UI;
public class ResultImg : MonoBehaviour
{

    public enum Img
    {
        PlayerFinish,                       //�÷��̾ 1������ ���� ���
        PlayerPass_rankIn,              //ai�� ���� �������� �÷��̾ ��ŷ���� ���� ���
        PlayerPass_rankInNot,       //ai�� ���� ���԰�, �÷��̾ ��ŷ�� ���� ��� ()
        PlayerNotPass                   // ai�� ���� ���԰�, �÷��̾ �ƿ� ����� ���� ��Ȳ
    }

    //Image 
    [SerializeField] Sprite[] resultImg;
    [SerializeField] Image resultImgPanel;


    public Img curImg = Img.PlayerFinish;

    //��� â--------------------------------------------------------------------------------------
    [SerializeField] GameObject resultPanelGameObject;

    void Start()
    {
       // resultImgPanel.sprite = resultImg[0];
    }
    public void ChangeImg(Img imgString)
    {
        curImg = imgString;

        switch (imgString)
        {
            case Img.PlayerFinish:
                Debug.Log("PlayerFinish");
                resultImgPanel.sprite = resultImg[0];
                Invoke("ShowResultPanel", 1f);
                SoundController.GetInstance().GetSound(SoundController.Actions.Goal);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = true;
                break;

            case Img.PlayerPass_rankIn:
                Debug.Log("PlayerPass_rankIn");
                resultImgPanel.sprite = resultImg[0];
                Invoke("ShowResultPanel", 1f);
                SoundController.GetInstance().GetSound(SoundController.Actions.Goal);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = true;
                break;

            case Img.PlayerPass_rankInNot:
                Debug.Log("PlayerPass_rankInNot");
                resultImgPanel.sprite = resultImg[1];
                Invoke("ShowResultPanel", 1f);
                SoundController.GetInstance().GetSound(SoundController.Actions.Goal);
                resultPanelGameObject.GetComponent<ResultPanel>().playerPass = true;
                break;

            case Img.PlayerNotPass:
                Debug.Log("PlayerNotPass");
                resultImgPanel.sprite = resultImg[2];
                Invoke("ShowResultPanel", 1f);
                SoundController.GetInstance().GetSound(SoundController.Actions.Goal);
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
