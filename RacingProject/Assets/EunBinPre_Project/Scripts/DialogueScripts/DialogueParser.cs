using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
        //csv ������ �Ľ��ϴ� ��ũ��Ʈ

    public Dialogue[] Parse(string csvFileName)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(csvFileName);     //csv ���� ������ ��
        //TextAsset�� csv������ ��� �׸��̶�� �����ϸ� ���ϴ�.

        string[] data = csvData.text.Split(new char[] { '\n' });

        for (int i = 1; i < data.Length;) 
        {
            //i�� 1���� �����ϴ� ����. data[0]���� ������� ����ֱ� ����

            //data�� , �� �������ش�.
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
                        //���� �����Ͱ� �������ʾҴٸ�
                        //
                        row = data[i].Split(new char[] { ',' });
                    }
                    else
                    {
                        contextList.Add(context);

                        break;
                    }


                } while (row[2] == "");
            } while (row[0] == ""); //���࿡ �� ĭ�̸�
            //dialogue.context


  
        }

        return dialogueList.ToArray();

    }


    private void Start()
    {
        Parse("Test001");
    }

}
