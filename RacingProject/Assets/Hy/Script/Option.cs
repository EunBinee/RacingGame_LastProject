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
    }
    public void OutOption()
    {
        OptionPanel.SetActive(false);
    }
    public void Continue() //���Ӱ���ϱ�
    {
        Time.timeScale = 1;
        OptionPanel.SetActive(false);
        cd.Timer = 0;
        cd.Count();
    }

    public void GameExit() //���ӳ�����
    {
        Application.Quit();
    }
    public void RestartScenesButton() //�� �����
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
