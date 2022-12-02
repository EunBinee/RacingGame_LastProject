using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputName : MonoBehaviour
{
    public GameObject InputImage;
    public InputField playerNameInput;
    private string playerName = "";

    private void Awake()
    {
        playerName = playerNameInput.GetComponent<InputField>().text;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName.Length > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            InputUserName();
        }
    }

    public void InputUserName()
    {
        playerName = playerNameInput.text;
        //UserInfo.GetInstance().SetNickName(playerName);
        Debug.Log(playerName);
        //SceneManager.LoadScene("Bike");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bicycle"))
        {
            InputImage.SetActive(true);
        }
    }
}
