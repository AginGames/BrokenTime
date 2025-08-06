using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomekJonczora : MonoBehaviour
{
    public GameObject miejsce;
    public GameObject popup;
    public GameObject player; 
    private LevelLoader levelLoader;
    public int lvl;
    private bool active = false;

    public void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
    }
    public void Update()
    {
        if ((miejsce.transform.position - player.transform.position).sqrMagnitude < 0.1f)
        {
            setActive();
            active = true;
            if(Input.GetKeyDown(KeyCode.E))
            {
                levelLoader.LoadNextLevel(lvl);
                Cursor.visible = true;
            }

        }
        else
        {
            setFalse();
            active = false;
        }
    }
    public void setActive()
    {
        if(!active)
        {
            popup.SetActive(true);
        }
        
    }
    public void setFalse()
    {
        if(active)
        {
            popup.SetActive(false);
        }
    }
}
