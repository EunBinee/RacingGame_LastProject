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
        if(Input.GetKey(KeyCode.Escape)) //esc누르면 옵션 
        {
            Time.timeScale = 0; //게임 일시정지
            OptionPanel.SetActive(true);
        }

        
    }
    // Update is called once per frame
    public void GoOption() //옵션화면
    {
            //Time.timeScale = 0; //게임 일시정지
            OptionPanel.SetActive(true);
    }
    public void OutOption()
    {
        OptionPanel.SetActive(false);
    }
    public void Continue() //게임계속하기
    {
        Time.timeScale = 1;
        OptionPanel.SetActive(false);
        cd.Timer = 0;
        cd.Count();
    }

    public void GameExit() //게임끝내기
    {
        Application.Quit();
    }
    public void RestartScenesButton() //씬 재시작
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
