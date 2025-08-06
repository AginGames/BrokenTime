using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class questionmodel
{
    [TextArea()]
    public string Question;
    [TextArea()]
    public string[] Answer;
    public int correctAnswer;
}