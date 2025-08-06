using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMenager : MonoBehaviour
{
    public TextMeshProUGUI character;
    public TextMeshProUGUI DialogueText;
    private BarmanTrigger trigger;
    public Animator animator;
    public GameObject dialougebox;
    public float textSpeed = 0.05f;
    public LevelLoader loader;
    public Image image;

    public Queue<string> sentences;
    public bool dialog = false;

    void Start()
    {
        sentences = new Queue<string>();
        dialog = false;
        trigger = FindObjectOfType<BarmanTrigger>();
        loader = FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && dialog == true)
        {
            DisplayNextScene();

        }

    }
    public void StartDialogue(dialogue dialogue, bool czyDrugi, Sprite sprite)
    {
        dialog = true;
        dialougebox.SetActive(true);
        image.sprite = sprite;
        animator.SetBool("IsOpen", true);
        character.text = dialogue.name;
        sentences.Clear();
        if (!czyDrugi || dialogue.sentences2.Length== 0)
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            trigger.czyDrugi = true;
            DisplayNextScene();
        }
        else if(czyDrugi)
        {
            foreach (string sentence in dialogue.sentences2)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextScene();
        }

    }
    public void DisplayNextScene()
    {
        if (sentences.Count == 0)
        {
            Endthisshit();
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
    void Endthisshit()
    {

        animator.SetBool("IsOpen", false);
        dialougebox.SetActive(false);
        dialog = false;
        trigger.aktywuj();
        loader.LoadNextLevel(1);
        FindObjectOfType<PlayerMovement>().EnableControls();
        Cursor.visible = false;
        
    }

}
