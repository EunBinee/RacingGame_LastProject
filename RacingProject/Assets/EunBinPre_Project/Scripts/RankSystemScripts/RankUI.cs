using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    public Text[] rankUiText;

    public List<string> rankUiList;
    public List<string> rankName;

    void Start()
    {
        rankUiList = new List<string>();
        for(int i = 0; i < rankUiText.Length; i++)
        {
            rankUiList.Add(rankUiList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rankUiText.Length; i++)
        {
            rankUiText[i].text = rankUiList[i].ToString() + rankName[i];
        }
    }
}
