using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class trigger2 : MonoBehaviour
{
    public GameObject Object;
    public Trigger trigger;
    private void OnMouseDown()
    {
        trigger.TriggerDialogue();
        deaktywuj();
    }
    public void deaktywuj()
    {
        Object.SetActive(false);
    }
    public void aktywuj()
    {
        Object.SetActive(true);
    }
}


