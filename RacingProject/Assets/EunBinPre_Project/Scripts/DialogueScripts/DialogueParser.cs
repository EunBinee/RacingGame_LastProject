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

        bool endData = false;
        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;)
        {
            //i가 1부터 시작하는 이유. data[0]에는 헤더값이 들어있기 때문

            //data를 , 로 나누어준다.
            string[] row = data[i].Split(new char[] { ',' });
            Dialogue dialogue = new Dialogue();

            dialogue.checkPointNumber = int.Parse(row[0]);


            List<List<string>> contextList = new List<List<string>>();

            do
            {
                if(!endData)
                {
                    List<string> contexts = new List<string>();  //랭킬별 대사

                    do
                    {
                        contexts.Add(row[2]);

                        if (++i < data.Length)
                        {
                            //아직 값이 남음.
                            //다시 쪼갈라줌
                            row = data[i].Split(new char[] { ',' });
                        }
                        else
                        {
                            endData = true;
                            break;
                        }
                    } while (row[1].ToString() == "");   //랭킹으로 나눈다.


                    contextList.Add(contexts);
                }
                else
                {
                    break;
                }
            } while (row[0].ToString() == "");    //체크 포인트로 나눈다.


            dialogue.contexts = contextList;
            dialogueList.Add(dialogue);
        }


        return dialogueList.ToArray();

    }


}
