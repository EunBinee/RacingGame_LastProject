using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public GameObject OptionPanel;

    public CountDown cd;
    // Start is called before the first frame update
    void Start()
    {
        OptionPanel.transform.SetAsLastSibling();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape)) //esc������ �ɼ� 
        {

            Time.timeScale = 0; //���� �Ͻ�����
            OptionPanel.SetActive(true);

        }

        
    }
    // Update is called once per frame
    public void GoOption() //�ɼ�ȭ��
    {
            //Time.timeScale = 0; //���� �Ͻ�����
            OptionPanel.SetActive(true);
        SoundController.GetInstance().GetSound(SoundController.Actions.ButtonCilck);
    }
    public void OutOption()
    {
        OptionPanel.SetActive(false);
        Time.timeScale = 1;
        SoundController.GetInstance().GetSound(SoundController.Actions.ButtonCilck);
    }
    public void Continue() //���Ӱ���ϱ�
    {
        Time.timeScale = 1;
        SoundController.GetInstance().GetSound(SoundController.Actions.ButtonCilck);
        OptionPanel.SetActive(false);
        cd.Timer = 0;
        cd.Count();
    }

    public void GameExit() //���ӳ�����
    {
        SoundController.GetInstance().GetSound(SoundController.Actions.ButtonCilck);
        Application.Quit();
    }
    public void RestartScenesButton() //�� �����
    {
        SoundController.GetInstance().GetSound(SoundController.Actions.ButtonCilck);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }


}
