using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimelineDialogue : MonoBehaviour
{
    public TextMeshProUGUI character;
    public TextMeshProUGUI DialogueText;
    public Sprite sprite;
    public Animator animator;
    public GameObject dialougebox;
    public Image image;
    public float textSpeed = 0.05f;
    public dialogue dialogue;

    public Queue<string> sentences;
    void Start()
    {
        sentences = new Queue<string>();
    }
    public void TriggerDialogue()
    {
        StartDialogue(dialogue);
    }

    public void StartDialogue(dialogue dialogue)
    {
        image.sprite = sprite;
        dialougebox.SetActive(true);
        character.text = dialogue.name;
        animator.SetBool("IsOpen", true);
        
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextScene();
    }
    public void DisplayNextScene()
    {
        if (sentences.Count == 0)
        {
            Endthisshit();
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
    }
}
