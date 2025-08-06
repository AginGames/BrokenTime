using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Playables;

public class TimeMachineLoader : MonoBehaviour, IData
{
    public PlayableDirector timeline;
    public GameObject miejsce;
    public GameObject popup;
    public GameObject open;
    public GameObject player;
    private bool active = false;
    private bool openActive = false;
    public GameObject Newton;
    public GameObject Hawking;
    public GameObject Tesla;
    public GameObject Einstein;
    public string NewtonId = "Newton";
    public string HawkingId = "Hawking";
    public string TeslaId = "Tesla";
    public string EinsteinId = "Einstein";
    public SpriteRenderer SpriteRenderer;
    public Sprite pusty;
    public Sprite tm1;
    public Sprite tm2;
    public Sprite tm3;
    public Sprite tm4;
    public Sprite pelny;
    private bool end =false;
    public void LoadData(GameData data)
    {
        bool NewtonActive = data.checkpoint.TryGetValue(NewtonId, out bool czyDrugi);
        bool HawkingActive = data.checkpoint.TryGetValue(HawkingId, out bool czyDrugi2);
        bool TeslaActive = data.checkpoint.TryGetValue(TeslaId, out bool czyDrugi3);
        bool EinsteinActive = data.checkpoint.TryGetValue(EinsteinId, out bool czyDrugi4);
        int ileNaukowcow = 0;
        Newton.SetActive(NewtonActive);
        //Debug.Log(data.checkpoint.TryGetValue(NewtonId, out bool czyDrugi1));

        Hawking.SetActive(HawkingActive);
        //Debug.Log(data.checkpoint.TryGetValue(HawkingId, out bool czyDrugi3));

        Tesla.SetActive(TeslaActive);
        //Debug.Log(data.checkpoint.TryGetValue(TeslaId, out bool czyDrugi5));

        Einstein.SetActive(EinsteinActive);
        //Debug.Log(data.checkpoint.TryGetValue(EinsteinId, out bool czyDrugi7));

        ileNaukowcow += (NewtonActive ? 1 : 0);
        ileNaukowcow += (HawkingActive ? 1 : 0);
        ileNaukowcow += (TeslaActive ? 1 : 0);
        ileNaukowcow += (EinsteinActive ? 1 : 0);

        SpriteRenderer.sprite = TimeMachineUpdater(ileNaukowcow);
    }
    public Sprite TimeMachineUpdater(int index)
    {
        switch(index)
        {
            case 0:
                return pusty;
            case 1:
                return tm1;
            case 2:
                return tm2;
            case 3:
                return tm3;
            case 4:
                end = true;
                return tm4;
            case 5:
                return pelny;
            default:
                return pusty;
        }
    }
    public void SaveData(ref GameData data)
    {

    }
    public void Update()
    {
        if ((miejsce.transform.position - player.transform.position).sqrMagnitude < 1f)
        {
            setOpenActive();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (end)
                {
                    //GameObject.FindGameObjectWithTag("levelloader").GetComponent<LevelLoader>().LoadNextLevel(7);
                    timeline.Play();
                }
                else
                {
                    //Debug.Log("kliknieto E");
                    if (active)
                    {
                        setFalse();
                    }
                    else
                    {
                        setActive();
                    }
                }
            }

        }
        else
        {
            setOpenFalse();
            setFalse();
            active = false;
        }
    }
    public void setActive()
    {
        if (!active)
        {
            popup.SetActive(true);
            active = true;
        }

    }
    public void setFalse()
    {
        if (active)
        {
            popup.SetActive(false);
            active = false;
        }
    }
    public void setOpenActive()
    {
        if (!openActive)
        {
            open.SetActive(true);
            openActive = true;
        }

    }
    public void setOpenFalse()
    {
        if (openActive)
        {
            open.SetActive(false);
            openActive = false;
        }
    }
}
