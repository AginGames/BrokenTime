using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject popup;
    public GameObject player;

    void Update()
    {
        if((popup.transform.position - player.transform.position).sqrMagnitude < .5f)
        {
            popup.SetActive(true);
        }
        else
        {
            popup.SetActive(false);
        }
    }
}
