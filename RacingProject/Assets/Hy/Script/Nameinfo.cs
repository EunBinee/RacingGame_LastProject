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
        ainames[0] = "Àººó"; ainames[1] = "¼ö¿¬"; ainames[2] = "ÇÏ¿µ"; ainames[3] = "Ãá½ÄÀÌ"; ainames[4] = "Èç³É";
        ainames[5] = "³Í¹«"; ainames[6] = "ÇìÀ³"; ainames[7] = "Ä³µÛ";ainames[8] = "´©Èå"; ainames[9] = "È£½ÄÀÌ"; 
        ainames[10] = "º£¶ó"; ainames[11] = "²ã¹Ù·Î¿ì"; ainames[12] = "»Ç¿À"; ainames[13] = "°í¶ó´Ï"; ainames[14] = "¼¼¸ð";
        ainames[15] = "È÷¾Æ"; ainames[16] = "¼¼ÀÌ"; ainames[17] = "³ª³ª"; ainames[18] = "³ª¿ø"; ainames[19] = "Èñ¼º";
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
