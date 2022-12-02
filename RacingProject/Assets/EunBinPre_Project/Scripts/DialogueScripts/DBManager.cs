using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public static DBManager instance;
    [SerializeField] string csvFileName = "";
    Dictionary<int, Dialogue> dialogueDic; 
    public static bool isFinish = false;            //다른 스크립트에서 파일파싱이 다끝났는지 확인용

    private void Awake()
    {
        if( instance == null)
        {
            instance = this;
            dialogueDic = new Dictionary<int, Dialogue>();      //모든 다이아로그를 저장할 딕셔너리
            DialogueParser parser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = parser.Parse(csvFileName);

            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i, dialogues[i]);           //i는 체크 포인트 번호로 체크가 가능하다.
                isFinish = true;                                    //데이터 파싱 끝
            }
        }
    }


    public Dialogue GetDialogue(int checkPointNum)    
    {
        return dialogueDic[checkPointNum];
    }

}
