using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static ResultImg;

public class GameManager : MonoBehaviour
{
    //�̱���-------------------------------------------------------------------------------------------
    //��𼭵� ��������.~
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }
    //----------------------------------------------------------------------------------------------------
    //��ŷ
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] runners;   //���� �ٰ��ִ� ���ʵ� (�÷��̾� + Ai)
    [SerializeField]  List<RankingSystem> sortArray;
    [SerializeField] RankUI rankUI;
   public  int curPlayerRank;
    //---------------------------------------------------------------------------------------------------
    //������ UI
    public bool isfinish = false;
    public int finishLap = 2;                //3�̸� 2���� ���� ����
    public bool playerFinish = false;  //�÷��̾ �������� Ȯ��



    //--------------------------------------------------------------------------------------------------------
    //�ؼ�
    [SerializeField] GameObject explanation_Board;
    [SerializeField] Text explanation_Text;
    //�ִϸ��̼�
    Animator Explanation_Anim;

    //-------------------------------------------------------------------------------------------------------
    //������ ����
    List<string> AiNameList;
    public string playerName = "";  //�÷��̾� �̸�

    //-------------------------------------------------------------------------------------------------------
    //��� â
    [SerializeField] GameObject ResultImgPanel;
    [SerializeField] GameObject FinishCount;
    bool alreadyShow = false;




    private void Awake()
    {
        //���� Ai�̸�
        AiNameList = new List<string>();
        string[] ainames = new string[20];
        ainames[0] = "�Ϳ��� ���̾�"; ainames[1] = "���ڸ԰� ��� ����"; ainames[2] = "�򸮴� ���� �׿�"; ainames[3] = "player3"; ainames[4] = "qwe";
        ainames[5] = "asd"; ainames[6] = "44"; ainames[7] = "55"; ainames[8] = "66"; ainames[9] = "77";
        ainames[10] = "88"; ainames[11] = "99"; ainames[12] = "�Ѷ�"; ainames[13] = "�̹�"; ainames[14] = "�f�f";
        ainames[15] = "gg"; ainames[16] = "����"; ainames[17] = "����"; ainames[18] = "�ɽ�"; ainames[19] = "������";
        ainames = ShuffleArray(ainames);
        for (int i = 0; i < 9; i++)
        {
            AiNameList.Add(ainames[i]);
        }

        playerName = PlayerPrefs.GetString("CurrentPlayerName");

        //�÷��̾� �̸� ����
        player.name = playerName;


        //Ai�̸� ����
        runners = GameObject.FindGameObjectsWithTag("Bicycle");

        int j = 0;
        for(int i =  0;i<runners.Length; i++)
        {
            if (runners[i].name != playerName)
            {
                runners[i].name = AiNameList[j];
                j++;
            }
        }

    }


    void Start()
    {
        instance = this;

        rankUI = GameObject.FindWithTag("RankUI").GetComponent<RankUI>();
        explanation_Board = GameObject.Find("explanation_Board");
        //explanation_Text = GameObject.Find("explanation_Text").GetComponent<Text>();
        Explanation_Anim = explanation_Board.GetComponent<Animator>();


        //���
        sortArray = new List<RankingSystem>();
        foreach (var runner in runners)
        {
            RankingSystem runnerRank = runner.GetComponent<RankingSystem>();
            sortArray.Add(runnerRank);
        }

        curPlayerRank = player.GetComponent<RankingSystem>().rank;
        rankUI.playerRank = curPlayerRank;


    }


    void Update()
    {
        RankCalculation();

        curPlayerRank = player.GetComponent<RankingSystem>().rank;
        rankUI.playerRank = curPlayerRank;

        //----------------------------------------------------------------------------------------------------------------------------------
        //������ üũ�� ������ ���� ����
        CheckLap();
    }
   public void  RankCalculation()
    {
        sortArray = sortArray.OrderBy(x => x.counter).ToList();
        //counter�� ���� �������� ����
        int rank_ = sortArray.Count;
        for (int i = 0; i < sortArray.Count; i++)
        {
            //�տ� �������� �޼����̴�.
            sortArray[i].rank = rank_;
            rankUI.rankName[rank_ - 1] = sortArray[i].name;
            rank_--;
        }
    }
    //--------------------------------------------------------------------------------------------
    //�ؼ� �ý���
    public void GetDialogue(int CheckPointNumber)
    {
        Dialogue dialogue = DBManager.GetInstance().GetDialogue(CheckPointNumber);

        RankingSystem playerRank = player.GetComponent<RankingSystem>();

        int rank=0;

        if (playerRank.rank == 1)
            rank = playerRank.rank;
        else if (playerRank.rank == 2)
            rank = playerRank.rank;
        else if (playerRank.rank == 3)
            rank = playerRank.rank;
        else if (playerRank.rank >= 4 && playerRank.rank <= 6)
            rank = 4;
        else if(playerRank.rank >= 7 && playerRank.rank <= 10)
            rank = 5;



        explanation_Board.SetActive(true);
        Explanation_Anim.SetTrigger("StartEx");
        StartCoroutine(StartExplanation(dialogue, rank));
        

    }


    IEnumerator StartExplanation(Dialogue dialogue, int rank)
    {
        int i = 0;
        while(true)
        {
            
            explanation_Text.text = dialogue.contexts[rank-1][i];
            
            i++;
            if (dialogue.contexts[rank-1].Count > i )
            {
                yield return new WaitForSeconds(1f);
            }
            else
                break;
        }

        yield return new WaitForSeconds(1f);
        Explanation_Anim.SetTrigger("EndEx");
        Invoke("ExplanationSetActiveFalse", 1.2f);
    }

    void ExplanationSetActiveFalse()
    {
        explanation_Board.SetActive(false);
    }
    //----------------------------------------------------------------------------------
    //finishLap-1 ���� ���� ��������!
    void CheckLap()
    {
        if (isfinish)
        {
            //���� isFinish�� true�� �ȴٸ� ������ ������ ���̴�.
            FinishGame();
        }
    }
    void FinishGame()
    {
        //1. �÷��̾ ������� ��� �ٷ� ��
        //2. Ai�� ������� ��� 10�ʸ� ��ٸ���.    
        //�׸��� 10�ʰ� �ѱ�� �ٷ� ��� ������ ������.
        //�ð��ʰ� �귯���� �� �÷��̾ ����ϸ� �ٷ� ������ ������.
        if(!alreadyShow)
        {
            if (!playerFinish)
            {
                //ai�� ����ؼ� ���� ���-----
                Debug.Log("ai���!! 10�ʸ� ��ٸ��ϴ�!");
                FinishCount.SetActive(true);

                FinishCount.GetComponent<FinishCount>().StartCount();

                alreadyShow = true;
            }
            else
            {
                //player�� ����ؼ� ���� ��� ------
                Debug.Log("��!");



                ResultImgPanel.SetActive(true);
                ResultImgPanel.GetComponent<ResultImg>().ChangeImg(Img.PlayerFinish);

                alreadyShow = true;

            }
        }



    }


    //----------------------------------------------------------------------------------
    //���� Ai�̸�
    private T[] ShuffleArray<T>(T[] ainames)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < ainames.Length; ++i)
        {
            random1 = Random.Range(0, ainames.Length);
            random2 = Random.Range(0, ainames.Length);

            temp = ainames[random1];
            ainames[random1] = ainames[random2];
            ainames[random2] = temp;
        }

        return ainames;
    }

}
