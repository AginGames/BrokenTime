using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[System.Serializable]
public class dialogue
{
    public string name;

    [TextArea()]
    public string[] sentences;
    [TextArea()]
    public string[] sentences2;
}
