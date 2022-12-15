using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject menuPanel;


    private void Start()
    {
      
    }
    private void Update()
    {
     
    }
    public void GameStart()
    {
        //SceneManager.LoadScene("Tutorial");
        //menuPanel.SetActive(true);
        GetComponent<InputName>().InputImage.SetActive(true);
        SoundController.GetInstance().GetSound(SoundController.Actions.ButtonCilck);

    }
    public void GoTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }
    public void GameMap()
    {
        //SceneManager.LoadScene("Bike");
        SceneManager.LoadScene("Map01");
        Time.timeScale = 1;

    }
    public void SingleMap()
    {
        SceneManager.LoadScene("Stage Single1");
        Time.timeScale = 1;
    }
    public void GameMain()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;


    }
   
}
