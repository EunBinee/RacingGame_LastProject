using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nameinfo : MonoBehaviour
{
    List<string> AiNameList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(PlayerPrefs.GetString("CurrentPlayerName"));

       

        string[] ainames = new string[20];
        ainames[0] = "Àººó"; ainames[1] = "¼ö¿¬"; ainames[2] = "ÇÏ¿µ"; ainames[3] = "player3"; ainames[4] = "qwe";
        ainames[5] = "asd"; ainames[6] = "44"; ainames[7] = "55";ainames[8] = "66"; ainames[9] = "77"; 
        ainames[10] = "88"; ainames[11] = "99"; ainames[12] = "¶Ñ¶Ñ"; ainames[13] = "¹Ì¹Ì"; ainames[14] = "µfµf";
        ainames[15] = "gg"; ainames[16] = "¼ö°í"; ainames[17] = "³ª³ª"; ainames[18] = "½É½É"; ainames[19] = "¸ÓÇÏÁö";
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
