using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("현재 맵 이름")]
    public string map;

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("대사 내용")]
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

    [Tooltip("대사 내용")]
    public string[] context;
}
