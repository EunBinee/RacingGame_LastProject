using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    public Text curPlayerRank;
    public Text[] rankUiText;

    private List<string> rankUiList;
    [HideInInspector]
    public List<string> rankName;

    void Start()
    {
        rankUiList = new List<string>();
        rankName = new List<string>();
        for (int i = 0; i < rankUiText.Length; i++)
        {
            rankUiList.Add((i+1) + "µî : ");
            rankName.Add(" ");
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
