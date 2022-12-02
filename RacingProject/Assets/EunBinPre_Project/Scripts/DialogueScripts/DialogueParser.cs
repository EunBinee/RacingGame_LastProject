using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
        //csv 파일을 파싱하는 스크립트

    public Dialogue[] Parse(string csvFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);     //csv 파일 가지고 옴
        //TextAsset은 csv파일을 담는 그릇이라고 생각하면 편하다.

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;) 
        {
            //i가 1부터 시작하는 이유. data[0]에는 헤더값이 들어있기 때문

            //data를 , 로 나누어준다.
            string[] row = data[i].Split(new char[] { ',' });
            Dialogue dialogue = new Dialogue();

            dialogue.map = row[0];
            dialogue.checkPointNumber = int.Parse(row[1]);

            Debug.Log(row[0]);
            Debug.Log(row[1]);

            List<List<string>> contextList = new List<List<string>>();

            do
            {
                List<string> context = new List<string>();
                contextList.Add(new List<string>());
                do
                {
                    context.Add(row[3]);

                    if (++i < data.Length)
                    {
                        //만약 데이터가 끝나지않았다면
                        //
                        row = data[i].Split(new char[] { ',' });
                    }
                    else
                    {
                        contextList.Add(context);

                        break;
                    }


                } while (row[2] == "");
            } while (row[0] == ""); //만약에 빈 칸이면
            //dialogue.context


  
        }

        return dialogueList.ToArray();

    }


    private void Start()
    {
        Parse("Test001");
    }

}
