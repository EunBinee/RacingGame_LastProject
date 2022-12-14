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

    public GameObject menuPanel;

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

        if(isname) //�г� �Է�������
        {
            Debug.Log(playerName);
            //PlayerPrefs.GetString("CurrentPlayerName"); ����� �г� �ҷ�����
        }
    }

    public void InputUserName()
    {
        playerName = playerNameInput.text;
        //playerName=System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(playerName));

        PlayerPrefs.SetString("CurrentPlayerName", playerName);
        Debug.Log(playerName);
        isname = true;
        InputImage.SetActive(false); 
        menuPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wheel")) //���� �����ϸ� �г��� �Է� â ���
        {
            menuPanel.SetActive(true);
        }
    }
}
