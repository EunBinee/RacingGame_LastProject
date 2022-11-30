using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    [Tooltip("현재 맵 이름")]
    public string map;

    [Tooltip("id")]
    public int id;

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("대사 내용")]
    public string[] context;
}

public class DialogueEvent
{
    [Tooltip("현재 맵 이름")]
    public string map;

    [Tooltip("id")]
    public int id;

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("대사 내용")]
    public string[] context;
}
