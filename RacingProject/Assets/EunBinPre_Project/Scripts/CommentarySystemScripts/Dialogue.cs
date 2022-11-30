using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    [Tooltip("���� �� �̸�")]
    public string map;

    [Tooltip("id")]
    public int id;

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("��� ����")]
    public string[] context;
}

public class DialogueEvent
{
    [Tooltip("���� �� �̸�")]
    public string map;

    [Tooltip("id")]
    public int id;

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;

    [Tooltip("Rank")]
    public int Rank;

    [Tooltip("��� ����")]
    public string[] context;
}
