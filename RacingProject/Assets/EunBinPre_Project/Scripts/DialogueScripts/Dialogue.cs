using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    [Tooltip("CheckPointNumber")]
    public int checkPointNumber;


    [Tooltip("대사 내용")]
    public List<List<string>> contexts;

}

