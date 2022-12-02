using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("���� �� �̸�")]
    public string map;

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("��� ����")]
    public List<List<string>> contexts;
//    public string[] context;
}

[System.Serializable]
public class DialogueEvent
{
    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("��� ����")]
    public string[] context;
}
