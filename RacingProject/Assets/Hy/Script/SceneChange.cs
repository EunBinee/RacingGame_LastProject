using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject menuPanel;

    public void GameStart()
    {
        //SceneManager.LoadScene("Tutorial");
        menuPanel.SetActive(true);
    }
    public void GoTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void GameMap()
    {
        //SceneManager.LoadScene("Bike");
        SceneManager.LoadScene("Stage");
    }
    public void GameMain()
    {
        SceneManager.LoadScene("Main");
    }
}
