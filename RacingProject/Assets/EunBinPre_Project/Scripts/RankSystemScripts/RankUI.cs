using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{

    //UI text부분
    public Text[] rankUiText;


    //플레이어 등수
    public int playerRank = 1;

    //플레이어 등수 이미지
    [SerializeField] Image rankImg;
    [SerializeField] Sprite curPlayerRankImage;
    public Sprite[] playerRankImageArray;


    private List<string> rankUiList;
    [HideInInspector]
    public List<string> rankName;

    void Start()
    {
        //플레이어 등수 이미지 변경
        // curPlayerRankImage = playerRankImageArray[playerRank -1];
        //     rankImg.sprite = curPlayerRankImage;
        //rankImg.sprite= playerRankImageArray[playerRank - 1];

        rankUiList = new List<string>();
        rankName = new List<string>();
        for (int i = 0; i < rankUiText.Length; i++)
        {
            rankUiList.Add((i+1) + "등 : ");
            rankName.Add(" ");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 등수 이미지 변경
        //  curPlayerRankImage = playerRankImageArray[playerRank-1];
        //   rankImg.sprite = curPlayerRankImage;

        rankImg.sprite = playerRankImageArray[playerRank-1];
        Debug.Log("플레이어:" + playerRank); //1등이면 1로 뜸

        for (int i = 0; i < rankUiText.Length; i++)
        {
            rankUiText[i].text = rankUiList[i].ToString() + rankName[i];
        }
    }
}
