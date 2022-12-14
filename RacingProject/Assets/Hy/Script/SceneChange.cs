using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject imgBan;
    public bool mapOpen=false;

    private void Start()
    {
        mapOpen = false; 
    }
    public void GameStart()
    {
        //SceneManager.LoadScene("Tutorial");
        if (mapOpen)
        {
            menuPanel.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(true);
            imgBan.SetActive(false);
            Debug.Log("sad");
        }
       
    }
    public void GoTutorial()
    {
        mapOpen=true;
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
