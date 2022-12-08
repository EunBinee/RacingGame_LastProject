using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{

    //UI text�κ�
    public Text[] rankUiText;


    //�÷��̾� ���
    public int playerRank = 1;

    //�÷��̾� ��� �̹���
    [SerializeField] Image rankImg;
    [SerializeField] Sprite curPlayerRankImage;
    public Sprite[] playerRankImageArray;


    private List<string> rankUiList;
    [HideInInspector]
    public List<string> rankName;

    void Start()
    {
        //�÷��̾� ��� �̹��� ����
        // curPlayerRankImage = playerRankImageArray[playerRank -1];
        //     rankImg.sprite = curPlayerRankImage;
        //rankImg.sprite= playerRankImageArray[playerRank - 1];

        rankUiList = new List<string>();
        rankName = new List<string>();
        for (int i = 0; i < rankUiText.Length; i++)
        {
            rankUiList.Add((i+1) + "�� : ");
            rankName.Add(" ");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� ��� �̹��� ����
        //  curPlayerRankImage = playerRankImageArray[playerRank-1];
        //   rankImg.sprite = curPlayerRankImage;

        rankImg.sprite = playerRankImageArray[playerRank-1];
        Debug.Log("�÷��̾�:" + playerRank); //1���̸� 1�� ��

        for (int i = 0; i < rankUiText.Length; i++)
        {
            rankUiText[i].text = rankUiList[i].ToString() + rankName[i];
        }
    }
}
