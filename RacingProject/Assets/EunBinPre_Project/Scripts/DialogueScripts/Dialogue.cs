using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;


    [Tooltip("��� ����")]
    public List<List<string>> contexts;
//    public string[] context;
}

[System.Serializable]
public class DialogueEvent
{
    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("��� ����")]
    public string[] context;
}
