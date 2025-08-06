using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BarmanTrigger : MonoBehaviour, IData
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
    public dialogue newdialogue;

    private GameData gameData;
    public void StartBar(GameData data)
    {
        //Debug.Log(data.checkpoint.TryGetValue("Newton", out bool czyDrugi));
        if (!(data.checkpoint.TryGetValue("Newton", out bool czyDrugi1)))
        {
            NewDialogueConverter("Domek Newtona znajduje się w prawej dolnej części mapy i jest koloru szarego");
        }
        else if(!(data.checkpoint.TryGetValue("Einstein", out bool czyDrugi2)))
        {
            NewDialogueConverter("Domek Einsteina znajduje się zaraz po drugiej stronie mostu");
        }
        else if(!(data.checkpoint.TryGetValue("Tesla", out bool czyDrugi3)))
        {
            NewDialogueConverter("Domek Tesli jest ostatnim domkiem po prawej górnej stronie mapy");
        }
        else if(!(data.checkpoint.TryGetValue("Hawking", out bool czyDrugi4)))
        {

        }
        else
            TriggerDialogue(dialogue);
    }
    public void LoadData(GameData data)
    {
        this.czyDrugi = data.checkpoint.TryGetValue(id, out czyDrugi);
        gameData = data;
    }
    public void SaveData(ref GameData data)
    {
        if (data.checkpoint.ContainsKey(id))
        {
            data.checkpoint.Remove(id);
        }
        data.checkpoint.Add(id, czyDrugi);
    }
    private void NewDialogueConverter(string hint)
    {
            newdialogue.sentences = new string[dialogue.sentences.Length + 1];
            newdialogue.sentences2 = new string[dialogue.sentences2.Length + 1];
            Debug.Log(dialogue.sentences.Length);
            dialogue.sentences.CopyTo(newdialogue.sentences, 0);
            dialogue.sentences2.CopyTo(newdialogue.sentences2, 0);
            newdialogue.name = dialogue.name;
            Debug.Log(newdialogue.sentences[dialogue.sentences.Length]);
            newdialogue.sentences[dialogue.sentences.Length] = hint;
            newdialogue.sentences2[dialogue.sentences2.Length] = hint;
            TriggerDialogue(newdialogue);
    }
    public void TriggerDialogue(dialogue dialogue)
    {
        //Debug.Log("Barman trigger = "+ sprite);
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
        if ((npc.transform.position - player.transform.position).sqrMagnitude < .25f && (!FindObjectOfType<DialogueMenager>().dialog))
        {
            interactSign.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartBar(gameData);
                player.GetComponent<PlayerMovement>().DisableControls();
            }
        }
        else
        {
            interactSign.SetActive(false);
        }
    }


}
