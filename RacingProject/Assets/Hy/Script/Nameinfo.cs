using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nameinfo : MonoBehaviour
{
    List<string> AiNameList = new List<string>();
    public string playerName = "";

    // Start is called before the first frame update
    void Start()
    {
        playerName = PlayerPrefs.GetString("CurrentPlayerName");

        string[] ainames = new string[20];
        ainames[0] = "����"; ainames[1] = "����"; ainames[2] = "�Ͽ�"; ainames[3] = "�����"; ainames[4] = "���";
        ainames[5] = "�͹�"; ainames[6] = "����"; ainames[7] = "ĳ��";ainames[8] = "����"; ainames[9] = "ȣ����"; 
        ainames[10] = "����"; ainames[11] = "��ٷο�"; ainames[12] = "�ǿ�"; ainames[13] = "����"; ainames[14] = "����";
        ainames[15] = "����"; ainames[16] = "����"; ainames[17] = "����"; ainames[18] = "����"; ainames[19] = "��";
        ainames = ShuffleArray(ainames);
        for (int i = 0; i < 9; i++)
        {
            Debug.Log(ainames[i]);
        }


    }


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
