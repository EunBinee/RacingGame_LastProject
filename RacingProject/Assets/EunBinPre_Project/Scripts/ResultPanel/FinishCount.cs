using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ResultImg;

public class FinishCount : MonoBehaviour
{
    [SerializeField] Text countText;

    [SerializeField] GameObject ResultImgPanel;

    void Start()
    {
       // StartCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCount()
    {
        StartCoroutine(StartCountTime()); 
    }

    IEnumerator StartCountTime()
    {
        bool PlayerFinish = false;
        yield return new WaitForSeconds(2.0f);

        int i = 10;
        while(i > 0)
        {

            if (GameManager.GetInstance().playerFinish)
            {
                PlayerFinish = true;
                break;
            }
            else
            {
                countText.text = i.ToString();
                i--;
            }


            yield return new WaitForSeconds(1.0f);
        }

        if( PlayerFinish)
        {
            //카운트 다운때, 플레이어가 들어온 경우
            if (GameManager.GetInstance().curPlayerRank >=1&& GameManager.GetInstance().curPlayerRank <= 3)
            {
                //1등에서 3등일때
                
                ResultImgPanel.SetActive(true);
                ResultImgPanel.GetComponent<ResultImg>().ChangeImg(Img.PlayerPass_rankIn);
                this.gameObject.SetActive(false);
            }
            else
            {
                ResultImgPanel.SetActive(true);
                ResultImgPanel.GetComponent<ResultImg>().ChangeImg(Img.PlayerPass_rankInNot);
                this.gameObject.SetActive(false);
            }

        }
        else
        {
            ResultImgPanel.SetActive(true);
            ResultImgPanel.GetComponent<ResultImg>().ChangeImg(Img.PlayerNotPass);
            this.gameObject.SetActive(false);
        }



    }

}
