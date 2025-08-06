using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class silnikScript : MonoBehaviour, IDropHandler
{
    int i = 1;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if(eventData.pointerDrag.GetComponent<DragDrop>().index == i)
            {
                eventData.pointerDrag.SetActive(false);
                GetComponent<silnikSwaper>().ZmianaBanana(i);
                i++;
            }
            else
            {
                eventData.pointerDrag.GetComponent<RectTransform>().position = eventData.pointerDrag.GetComponent<DragDrop>().defaultPosition;
            }
            
            
        }
    }
}

