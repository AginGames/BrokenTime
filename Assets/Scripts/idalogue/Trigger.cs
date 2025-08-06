using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour, IData
{
    [SerializeField] private string id;

    [ContextMenu("Generate unique id")]
    private void Generateid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public GameObject interactSign;
    public GameObject player;
    public GameObject npc;
    public Sprite sprite;
    public bool czyDrugi = true;
    public dialogue dialogue;
    public void LoadData(GameData data)
    {
        this.czyDrugi = data.checkpoint.TryGetValue(id, out czyDrugi);
    }
    public void SaveData(ref GameData data)
    {
        if (data.checkpoint.ContainsKey(id))
        {
            data.checkpoint.Remove(id);
        }
        data.checkpoint.Add(id, czyDrugi);
    }
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueMenager>().StartDialogue(dialogue, czyDrugi, sprite);
    }
    public void deaktywuj()
    {
        npc.SetActive(false);
    }
    public void aktywuj()
    {
        npc.SetActive(true);
    }
    void Update()
    {
        if ((npc.transform.position - player.transform.position).sqrMagnitude < .25f && (!FindObjectOfType<DialogueMenager>().dialog) )
        {
            interactSign.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                TriggerDialogue();
                player.GetComponent<PlayerMovement>().DisableControls();
            }
        }
        else
        {
            interactSign.SetActive(false);
        }
    }


}
 