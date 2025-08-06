using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeslaManager : MonoBehaviour, IData
{
    [SerializeField] private string id = "Tesla";
    public GameObject interactSign;
    public GameObject player;
    public GameObject npc;
    public Sprite sprite;
    public bool czyDrugi = false;
    public TextMeshProUGUI character;
    public TextMeshProUGUI DialogueText;
    public Animator animator;
    public GameObject dialougebox;
    public Image image;
    public GameObject taskCanvas;
    public float textSpeed = 0.05f;

    public dialogue dialogue;
    public dialogue2 EndDialogue;
    public Queue<string> sentences;
    private bool dialog = false;
    public bool game = false;

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
    void Start()
    {
        sentences = new Queue<string>();
        dialog = false;
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
        if ((npc.transform.position - player.transform.position).sqrMagnitude < .25f && (!dialog) && (!game))
        {
            interactSign.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartDialogue(dialogue, czyDrugi);
                player.GetComponent<PlayerMovement>().DisableControls();
            }
        }
        else
        {
            interactSign.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && dialog == true)
        {
            DisplayNextScene();

        }
    }
    public void StartDialogue(dialogue dialogue, bool czyDrugi)
    {
        dialog = true;
        image.sprite = sprite;
        dialougebox.SetActive(true);
        animator.SetBool("IsOpen", true);
        character.text = dialogue.name;
        sentences.Clear();
        if (!czyDrugi || dialogue.sentences2.Length == 0)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextScene();
        }
        else if (czyDrugi)
        {
            foreach (string sentence in dialogue.sentences2)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextScene();
        }

    }
    public void StartEndDialogue()
    {
        dialog = true;
        game = false;
        dialougebox.SetActive(true);
        animator.SetBool("IsOpen", true);
        character.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in EndDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextScene();

    }
    public void DisplayNextScene()
    {
        if (sentences.Count == 0)
        {
            if (!czyDrugi)
            {
                Debug.Log("CATCHER");
                AppleCather();
            }
            else if (czyDrugi)
            {
                Debug.Log("END");
                Endthisshit();
            }
            dialog = false;
            return;

        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void AppleCather()
    {
        game = true;
        animator.SetBool("IsOpen", false);
        dialougebox.SetActive(false);
        dialog = false;

        Cursor.visible = true;

        taskCanvas.SetActive(true);
    }
    public void Endthisshit()
    {
        taskCanvas.SetActive(false);

        animator.SetBool("IsOpen", false);
        dialougebox.SetActive(false);
        dialog = false;
        aktywuj();
        FindObjectOfType<LevelLoader>().LoadNextLevel(1);
        FindObjectOfType<PlayerMovement>().EnableControls();
        Cursor.visible = false;


    }
}
