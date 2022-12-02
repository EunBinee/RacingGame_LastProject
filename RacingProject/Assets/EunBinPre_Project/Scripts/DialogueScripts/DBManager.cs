using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;
    [SerializeField] string csvFileName = "";
    Dictionary<int, Dialogue> dialogueDic; 
    public static bool isFinish = false;            //�ٸ� ��ũ��Ʈ���� �����Ľ��� �ٳ������� Ȯ�ο�

    private void Awake()
    {
        if( instance == null)
        {
            instance = this;
            dialogueDic = new Dictionary<int, Dialogue>();      //��� ���̾Ʒα׸� ������ ��ųʸ�
            DialogueParser parser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = parser.Parse(csvFileName);

            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i, dialogues[i]);           //i�� üũ ����Ʈ ��ȣ�� üũ�� �����ϴ�.
                isFinish = true;                                    //������ �Ľ� ��
            }
        }
    }


    public Dialogue GetDialogue(int checkPointNum)    
    {
        return dialogueDic[checkPointNum];
    }

}
