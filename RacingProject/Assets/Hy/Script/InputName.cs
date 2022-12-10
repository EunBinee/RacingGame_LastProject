using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour
{
    public GameObject InputImage;
    public InputField playerNameInput;
    public string playerName = "";
    public bool isname;


    private void Awake()
    {
        playerName = playerNameInput.GetComponent<InputField>().text;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputUserName();
        }

        if(isname) //닉넴 입력했으면
        {
            Debug.Log(playerName);
            //PlayerPrefs.GetString("CurrentPlayerName"); 저장된 닉넴 불러오기
        }
    }

    public void InputUserName()
    {
        playerName = playerNameInput.text;
        //playerName=System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(playerName));

        PlayerPrefs.SetString("CurrentPlayerName", playerName);
        Debug.Log(playerName);
        isname = true;
        SceneManager.LoadScene("Bike");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wheel")) //끝에 도달하면 닉네임 입력 창 띄움
        {
            InputImage.SetActive(true);
        }
    }
}
