using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Player : MonoBehaviour, IData
{
    public int level;
    public float time;
    void Update()
    {
        this.time += Time.deltaTime;
    }
    public void LoadData(GameData data)
    {
        this.time = data.time;
        this.level = data.level;
    }
    public void SaveData(ref GameData data)
    {
        data.time = this.time;
        data.level = this.level;
    }

}
