using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Timeline : MonoBehaviour, IData
{
    public bool czyTimeline;
    public GameObject most;
    public AudioSource audioSource;
    public PlayableDirector timeline;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TimelineStart()
    {
        if(czyTimeline)
        {
            timeline.Play();
            czyTimeline = false;
            most.SetActive(true);
        }
        else
        {
            most.SetActive(false);
            PlayBackgroundMusic();
        }
    }
    public void PlayBackgroundMusic()
    {
        audioSource.Play();
    }
    public void LoadData(GameData data)
    {
        this.czyTimeline = data.timeline;
        TimelineStart();

    }
    public void SaveData(ref GameData data)
    {
        data.timeline = this.czyTimeline;
    }
}
