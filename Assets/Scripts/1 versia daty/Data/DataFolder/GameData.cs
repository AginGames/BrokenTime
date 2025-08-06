using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public int level;
    public Vector2 playerPosition;
    public float time;
    public bool timeline;

    public SerializableDictionary<string, bool> checkpoint;
    //public SerializableDictionary<string, int> DialogueCheck;

    public GameData()
    {
        this.time = 0;
        this.level = 0;
        this.timeline = true;
        playerPosition = new Vector2(10.56f, -6.751f);
        checkpoint = new SerializableDictionary<string, bool>();
    }
}
